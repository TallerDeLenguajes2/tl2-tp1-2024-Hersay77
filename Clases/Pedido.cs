using EspacioCliente;

namespace EspacioPedido
{
    public class Pedido
    {
        public int Nro { get;}
        public string Obs { get;}
        public Cliente Cliente { get;}
        public int Estado { get; set;}

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

        
        public static void MostrarPedido(Pedido pedido)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"\tPedido Nr: {pedido.Nro} | Obs: {pedido.Obs} | Estado = {pedido.Estado} (Enregado = 1 - No Entregado = 0)");
            Pedido.VerDatosCliente(pedido.Cliente);
            Console.ResetColor();
        }

        public Pedido(int Nro, string Obs, string Nombre, string Direccion, long Telefono, string DatosReferenciaDireccion,int Estado)
        {
            this.Nro = Nro;
            this.Obs = Obs;
            this.Cliente = new Cliente(Nombre, Direccion, Telefono, DatosReferenciaDireccion);
            this.Estado = Estado;
        }

    }
}