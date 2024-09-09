using EspacioPedido;

namespace EspacioCadete
{
    public class Cadete
    {
        private int id;
        private string nombre;
        private string direccion;

        private long telefono;

        private List<Pedido> listaPedidos;

        public int Id { get => id;}
        public string Nombre { get => nombre;  }
        public string Direccion { get => direccion; }
        public long Telefono { get => telefono; }
        public List<Pedido> ListaPedidos { get => listaPedidos; }

        public float JornalACobrar(Cadete cadete)
        {
            int totalPedidosCompletados = cadete.ListaPedidos.Count(pedido => pedido.Estado == 1);
            float jornal = totalPedidosCompletados * 500;
            return jornal;
        }

        public Cadete(int id, string nombre, string direccion, long telefono) //constructor
        {
            this.id = id;
            this.nombre = nombre;
            this.direccion = direccion;
            this.telefono = telefono;
            this.listaPedidos = new List<Pedido>();
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