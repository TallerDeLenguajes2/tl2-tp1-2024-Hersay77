using EspacioCliente;

namespace EspacioPedido
{
    public class Pedido
    {
        private int nro;
        private string obs;
        private Cliente cliente;
        private int estado;

        public int Nro { get => nro;}
        public string Obs { get => obs; }
        public Cliente Cliente { get => cliente; }
        public int Estado { get => estado; set => estado = value; }

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
            Console.WriteLine($"\tPedido Nr: {pedido.nro} | Obs: {pedido.obs} | Estado = {pedido.estado} (Enregado = 1 - No Entregado = 0)");
            Pedido.VerDatosCliente(pedido.cliente);
            Console.ResetColor();
        }

        public Pedido(int Nro, string Obs, string Nombre, string Direccion, long Telefono, string DatosReferenciaDireccion,int Estado)
        {
            this.nro = Nro;
            this.obs = Obs;
            this.cliente = new Cliente(Nombre, Direccion, Telefono, DatosReferenciaDireccion);
            this.estado = Estado;
        }

    }
}