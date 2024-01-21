using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using System.IO;
using Firebase.Auth;
using Firebase.Storage;

namespace Negocios
{
    public class CN_Firebase
    {

        public async Task<string> SubirStorage(Stream archivo , string nombre)
        {
            string UrlImagen = string.Empty;

            string email = "anuelitoramos@gmail.com";
            string clave = "Pauloyas20";
            string ruta = "tadiadmin.appspot.com";
            string api_key = "AIzaSyAPaTIb0wB_qBPetZAQFF2obKhD09LlCpo";

            try
            {

                var auth = new FirebaseAuthProvider(new FirebaseConfig(api_key));
                var a = await auth.SignInWithEmailAndPasswordAsync(email, clave);

                var cancellation = new CancellationTokenSource();

                var task = new FirebaseStorage(
                    ruta,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child("IMAGENES")
                    .Child(nombre)
                    .PutAsync(archivo, cancellation.Token);


                UrlImagen = await task;
            }
            catch
            {
                UrlImagen = "";
            }


            return UrlImagen;
        }
    }
}

