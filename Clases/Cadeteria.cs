using System.Data.Common;
using System.Text;
using System.IO;

namespace EspacioCadeteria
{
    public class Cadeteria
    {
        public string Nombre {get;set;}
        public long Telefono {get;set;}
        public List<Cadete> ListaCadetes {get; set;}

        public Cadeteria(string Nombre, long Telefono, List<Cadete> ListaCadetes) //constructor
        {
            this.Nombre = Nombre;
            this.Telefono = Telefono;
            this.ListaCadetes = ListaCadetes;
        }

        public static Cadeteria CargarCSVCadeteria(string ArchivoCadeteria, List<Cadete> ListaCadetes)
        {
            string[] lineas = File.ReadAllLines(ArchivoCadeteria); //leo archivo csv cadeteria

            string[] datos = lineas[0].Split(','); //creo arreglo con cada dato para la cadeteria

            Cadeteria cadeteria = new Cadeteria(datos[0], Convert.ToInt64(datos[1]), ListaCadetes);

            return cadeteria;
        }
    }

    public class Cadete
    {
        public int Id {get; set;}
        public string Nombre {get; set;}
        public string Direccion {get; set;}
        public long Telefono {get;set;} 
        public List<Pedido> ListaPedidos {get;set;}

        public float JornalACobrar() 
        {
            float jornal = 0;

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

        public static List<Cadete> CargarCSVCadetes(string ArchivoCadetes)
        {
            List<Cadete> ListaCadetes = new List<Cadete>(); //nueva lista de cadetes vacia

            string[] lineas = File.ReadAllLines(ArchivoCadetes); //leo archivo csv en un arreglo

            for (int i = 0; i < lineas.Length; i++) //recorre arreglo
            {
                string linea = lineas[i]; //selecciono una linea del archivo csv
                string[] datos = linea.Split(','); //creo arreglo con cada dato para cada cadete segun el formato

                Cadete cadete = new Cadete(Convert.ToInt32(datos[0]), datos[1], datos[2], Convert.ToInt64(datos[3]));

                ListaCadetes.Add(cadete); //agrego cadete a la lista
            }
            return ListaCadetes;
        }
    }

    public class Pedido
    {
        public int Nro {get; set;}
        public string Obs {get;set;}
        public Cliente Cliente {get; set;}
        public string Estado {get; set;}
        
        public string VerDireccionCliente() //metodo muestra direccion del cliente
        {
            return $"La direccion del cliente es: {Cliente.Direccion}";
        }
        public void VerDatosCliente() //metodo muestra datos del cliente
        {
            Console.WriteLine($"Nombre del Cliente: {Cliente.Nombre}");
            Console.WriteLine($"Direccion del Cliente: {Cliente.Direccion}");
            Console.WriteLine($"Telefono del Cliente: {Cliente.Telefono}");
            Console.WriteLine($"Datos de Referencia de Direccion: {Cliente.DatosReferenciaDireccion}");
        }      

        public Pedido(int nro, string obs, Cliente cliente, string estado) //constructor
        {
            Nro = nro;
            Obs = obs;
            Cliente = cliente;
            Estado = estado;
        }
    }

    public class Cliente 
    {
        public string Nombre {get; set;}
        public string Direccion {get; set;}
        public int Telefono {get; set;}
        public string DatosReferenciaDireccion {get;set;}

        public Cliente(string nombre, string direccion, int telefono, string datosReferenciaDireccion) //constructor
        {
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            DatosReferenciaDireccion = datosReferenciaDireccion;
        }
    }


}