using EspacioPedido;

namespace EspacioCadete
{
    public class Cadete
    {
        private int id;
        private string nombre;
        private string direccion;
        private long telefono;

        public int Id { get => id;}
        public string Nombre { get => nombre;  }
        public string Direccion { get => direccion; }
        public long Telefono { get => telefono; }


        public Cadete(int id, string nombre, string direccion, long telefono) //constructor
        {
            this.id = id;
            this.nombre = nombre;
            this.direccion = direccion;
            this.telefono = telefono;
        }

        public static void MostrarCadete(Cadete cadete)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Id: {cadete.Id} | Nombre: {cadete.Nombre} | Direccion: {cadete.Direccion} | Telefono: {cadete.Telefono}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Pedidos Asignados: ");
            Console.ResetColor();
        }
    }
}