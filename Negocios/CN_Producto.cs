﻿using Datos;
using Entidad_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Negocios
{
    public class CN_Producto
    {
        private CD_Producto OBJCapaDato = new CD_Producto();

        public List<Productos> Listar()
        {
            return OBJCapaDato.Listar();
        }

        public int Registrar(Productos obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del producto  no puede estar vacio";
            }

            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La Descripcion del producto no puede estar vacio";
            }
            else if (obj.IdMarca == 0)
            {
                Mensaje = "Debe seleccionar una marca";
            }
            else if (obj.IdCategoria == 0)
            {
                Mensaje = "Debe seleccionar una Categoria";
            }
            else if (obj.Precio == 0)
            {
                Mensaje = "Debe ingresar el precio del producto";
            }
            else if (obj.Stock == 0)
            {
                Mensaje = "Debe ingresar el stock del producto";
            }
            else if (obj.RutaImagen == "")
            {
                Mensaje = "Debe ingresar la imagen del producto";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return OBJCapaDato.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }

        }


        public bool Editar(Productos obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del producto  no puede estar vacio";
            }

            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La Descripcion del producto no puede estar vacio";
            }
            else if (obj.IdMarca == 0)
            {
                Mensaje = "Debe seleccionar una marca";
            }
            else if (obj.IdCategoria == 0)
            {
                Mensaje = "Debe seleccionar una Categoria";
            }
            else if (obj.Precio == 0)
            {
                Mensaje = "Debe ingresar el precio del producto";
            }
            else if (obj.Stock == 0)
            {
                Mensaje = "Debe ingresar el stock del producto";
            }
            else if (obj.RutaImagen == "")
            {
                Mensaje = "Debe ingresar la imagen del producto";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {

                return OBJCapaDato.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }


        public bool GuardarDatosImagen(Productos obj, out string Mensaje)
        {
            return OBJCapaDato.GuardarDatosImagen(obj, out Mensaje);
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return OBJCapaDato.Eliminar(id, out Mensaje);
        }
    }
}
