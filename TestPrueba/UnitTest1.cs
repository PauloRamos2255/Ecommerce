using Datos;
using Entidad;

namespace TestPrueba
{
    [TestFixture]
    public class Tests
    {

        CD_Cliente cliente = new CD_Cliente();


        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            CD_Cliente cliente = new CD_Cliente();

            Cliente obj = new Cliente();
        
            List<Clientes> listaClientes = cliente.ListarCliente();
          
            Assert.IsNotNull(listaClientes); 
            Assert.IsInstanceOf<List<Clientes>>(listaClientes);  

            foreach (var clientes in listaClientes)
            {

                Assert.IsNotNull(clientes.Nombre, "El nombre no debe ser nulo o vacío");
              
                Assert.IsNotNull(clientes.Apellido, "El apellido no debe ser nulo");
            }
        }
    }
}