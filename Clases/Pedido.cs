using EspacioCliente;
using EspacioCadete;

namespace EspacioPedido
{
    public class Pedido
    {
        public int Nro { get; set; }
        public string Obs { get; set; }
        public Cliente Cliente { get; set; }
        public int Estado { get; set; }

        public static string VerDireccionCliente(Cliente cliente) //metodo muestra direccion del cliente
        {
            return $"\tLa direccion del cliente es: {cliente.Direccion}";
        }
        public static void VerDatosCliente(Cliente cliente) //metodo muestra datos del cliente
        {
            Console.WriteLine("\tDatos del Cliente: ");
            Console.WriteLine($"\t\tNombre del Cliente: {cliente.Nombre}");
            VerDireccionCliente(cliente);
            Console.WriteLine($"\t\tTelefono del Cliente: {cliente.Telefono}");
            Console.WriteLine($"\t\tDatos de Referencia de Direccion: {cliente.DatosReferenciaDireccion}");
        }

        public Pedido(int nro)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("######### DANDO DE ALTA UN PEDIDO #########\n");
            Console.ForegroundColor = ConsoleColor.White;

            string obs, nombre, direccion, entrada, datosReferenciaDireccion;
            long telefono;
            int estado;
            bool bandera;
            /*ingreso nro automatico*/
            Console.WriteLine("Ingrese Pedido y observaciones: ");
            obs = Console.ReadLine();
            Console.WriteLine("Ingrese Nombre Cliente: ");
            nombre = Console.ReadLine();
            Console.WriteLine("Ingrese Direccion del Cliente: ");
            direccion = Console.ReadLine();
            Console.WriteLine("Ingrese Telefono Cliente: ");
            do
            {
                entrada = Console.ReadLine();
                bandera = long.TryParse(entrada, out telefono);
                if (!bandera)
                {
                    Console.WriteLine("Ingreso Incorrecto. Vuelva a ingresar un numero");
                }
            } while (!bandera);
            Console.WriteLine("Ingrese Datos de Referencia de la Direccion: ");
            datosReferenciaDireccion = Console.ReadLine();
            Console.WriteLine("Ingrese Estado del pedido: 1 = Entregado y 0 = No Entregado");
            do
            {
                entrada = Console.ReadLine();
                bandera = int.TryParse(entrada, out estado);
                if (!bandera || (estado != 1 && estado != 0))
                {
                    Console.WriteLine("Ingreso Incorrecto. Vuelva a ingresar un numero");
                }
            } while (!bandera || (estado != 1 && estado != 0));

            //construyendo pedido
            Nro = nro;
            Obs = obs;
            Cliente = new Cliente(nombre, direccion, telefono, datosReferenciaDireccion);
            Estado = estado;
            Console.ResetColor();
        }

        public static void MostrarPedido(Pedido pedido)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"\tPedido Nr: {pedido.Nro} | Obs: {pedido.Obs} | Estado = {pedido.Estado} (Enregado = 1 - No Entregado = 0)");
            Pedido.VerDatosCliente(pedido.Cliente);
            Console.ResetColor();
        }

    }
}