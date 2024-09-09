using EspacioPedido;

namespace EspacioCadete
{
    public class Cadete
    {
        public int Id { get; }
        public string Nombre { get; }
        public string Direccion { get;}
        public long Telefono { get; }
        public List<Pedido> ListaPedidos { get;}

        public float JornalACobrar(Cadete cadete)
        {
            int totalPedidosCompletados = cadete.ListaPedidos.Count(pedido => pedido.Estado == 1);
            float jornal = totalPedidosCompletados * 500;
            return jornal;
        }

        public Cadete(int id, string nombre, string direccion, long telefono) //constructor
        {
            Id = id;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            ListaPedidos = new List<Pedido>();
        }

        public static void MostrarCadete(Cadete cadete)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Id: {cadete.Id} | Nombre: {cadete.Nombre} | Direccion: {cadete.Direccion} | Telefono: {cadete.Telefono}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Pedidos Asignados: ");
            if (cadete.ListaPedidos.Count > 0)
            {
                foreach (var pedido in cadete.ListaPedidos)
                {
                    Pedido.MostrarPedido(pedido);
                }
            }
            else
            {
                Console.WriteLine("Aun no se asignaron pedidos a este cadete");
            }
            Console.ResetColor();
        }
    }
}