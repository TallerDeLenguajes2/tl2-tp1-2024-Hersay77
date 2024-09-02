using System.Data.Common;
using System.Text;
using System.IO;
using System.Net;

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

        public float JornalACobrar(Cadete cadete) 
        {
            float jornal = 0;
            foreach (var pedido in cadete.ListaPedidos)
            {
                if (pedido.Estado == 1)
                {
                    jornal += 500;
                }
            }
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

    public class Pedido
    {
        public int Nro {get; set;}
        public string Obs {get;set;}
        public Cliente Cliente {get; set;}
        public int Estado {get; set;}
        
        public static string VerDireccionCliente(Cliente cliente) //metodo muestra direccion del cliente
        {
            return $"\tLa direccion del cliente es: {cliente.Direccion}";
        }
        public static void VerDatosCliente(Cliente cliente) //metodo muestra datos del cliente
        {
            Console.WriteLine("\tDatos del Cliente: ");
            Console.WriteLine($"\tNombre del Cliente: {cliente.Nombre}");
            VerDireccionCliente(cliente);
            Console.WriteLine($"\tTelefono del Cliente: {cliente.Telefono}");
            Console.WriteLine($"\tDatos de Referencia de Direccion: {cliente.DatosReferenciaDireccion}");
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

        public static void AsignarPedido(List<Pedido> ListaGeneralPedidos, List<Cadete> ListaCadetes)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("####### ASIGNANDO PEDIDO A CADETE #########");
            Console.ResetColor();
            Console.WriteLine("Selecione un pedido: ");
            foreach (var pedido in ListaGeneralPedidos)
            {
                MostrarPedido(pedido);
            }
            int nroABuscar;
            string entrada;
            bool entradacorrecta;
            Pedido pedidoEncontrado;
            do
            {
                Console.WriteLine("Ingrese numero del pedido: ");
                entrada = Console.ReadLine();
                entradacorrecta = int.TryParse(entrada, out nroABuscar);
                pedidoEncontrado = ListaGeneralPedidos.Find(Pedido => Pedido.Nro == nroABuscar);
                if (entradacorrecta && pedidoEncontrado != null )
                {
                    Cadete cadeteEncontrado;
                    do
                    {
                        Console.WriteLine("Seleccione un Cadete");
                        foreach (var cadete in ListaCadetes)
                        {
                            Cadete.MostrarCadete(cadete);
                        }
                        Console.WriteLine("Ingrese el Id del Cadete: ");
                        entrada = Console.ReadLine();
                        entradacorrecta = int.TryParse(entrada, out nroABuscar);
                        cadeteEncontrado = ListaCadetes.Find(Cadete => Cadete.Id == nroABuscar);
                        if (entradacorrecta && cadeteEncontrado != null)
                        {
                            cadeteEncontrado.ListaPedidos.Add(pedidoEncontrado); //agrego el pedido a la lista del cadete
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Pedido Asignado  a Cadete");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine("No se encontro el Cadete - puede haber ingresado un numero incorrecto");
                        }
                    } while (!entradacorrecta || cadeteEncontrado == null);

                }
                else
                {
                    Console.WriteLine("No se encontro el Pedido - puede haber ingresado un numero incorrecto");
                }
            } while (!entradacorrecta || pedidoEncontrado == null);
        }
    
        public static void CambiarEstado(List<Pedido> ListaGeneralPedidos)
        {
            Console.WriteLine("### CAMBIANDO ESTADO A UN PEDIDO: ###");
            Console.WriteLine("Selecione un pedido: ");
            foreach (var pedido in ListaGeneralPedidos)
            {
                MostrarPedido(pedido);
            }
            int nroABuscar;
            string entrada;
            bool entradacorrecta;
            Pedido pedidoEncontrado;
            do
            {
                Console.WriteLine("Ingrese numero del pedido: ");
                entrada = Console.ReadLine();
                entradacorrecta = int.TryParse(entrada, out nroABuscar);
                pedidoEncontrado = ListaGeneralPedidos.Find(Pedido => Pedido.Nro == nroABuscar);
                if (entradacorrecta && pedidoEncontrado != null )
                {
                    Console.WriteLine("Ingrese Estado del pedido: 1 = Entregado y 0 = No Entregado");
                    int estado;
                    do
                    {
                        entrada = Console.ReadLine();
                        entradacorrecta = int.TryParse(entrada, out estado);
                        if (!entradacorrecta || (estado != 1 && estado != 0))
                        {
                            Console.WriteLine("Ingreso Incorrecto. Vuelva a ingresar un numero");
                        }
                    } while (!entradacorrecta || (estado != 1 && estado != 0));
                    pedidoEncontrado.Estado = estado;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Estado cambiado");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("No se ecnontro el Pedido - puede haber ingresado un numero incorrecto");
                }
            } while (!entradacorrecta || pedidoEncontrado == null);
        }
    
        public static void ReasignarPedido(List<Pedido> ListaGeneralPedidos, List<Cadete> ListaCadetes)
        {
            Console.WriteLine("####### REASIGNANDO PEDIDO ########");
            Console.WriteLine("Seleccione el cadete al que se le asigno un pedido: ");
            foreach (var cadete in ListaCadetes)
            {
                Cadete.MostrarCadete(cadete);
            }
            int nroABuscar;
            string entrada;
            bool entradacorrecta;
            Cadete cadeteEncontrado;
            do
            {
                Console.WriteLine("Seleccione un id del cadete: ");
                entrada = Console.ReadLine();
                entradacorrecta = int.TryParse(entrada, out nroABuscar);
                cadeteEncontrado = ListaCadetes.Find(Cadete => Cadete.Id == nroABuscar);
                if (entradacorrecta && cadeteEncontrado != null )
                {
                    Pedido pedidoEncontrado;
                    do
                    {
                        Console.WriteLine("Seleccione un Pedido");
                        foreach (var pedido in cadeteEncontrado.ListaPedidos)
                        {
                            Pedido.MostrarPedido(pedido);
                        }
                        Console.WriteLine("Ingrese el Nro del Pedido: ");
                        entrada = Console.ReadLine();
                        entradacorrecta = int.TryParse(entrada, out nroABuscar);
                        pedidoEncontrado = cadeteEncontrado.ListaPedidos.Find(Pedido => Pedido.Nro == nroABuscar);
                        if (entradacorrecta && pedidoEncontrado != null)
                        {
                            cadeteEncontrado.ListaPedidos.Remove(pedidoEncontrado);
                            Pedido.AsignarPedido(ListaGeneralPedidos, ListaCadetes);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Pedido Reasignado");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine("No se encontro el pedido - puede haber ingresado un numero incorrecto");
                        }
                    } while (!entradacorrecta || cadeteEncontrado == null);

                }
                else
                {
                    Console.WriteLine("No se encontro el cadete - puede haber ingresado un numero incorrecto");
                }
            } while (!entradacorrecta || cadeteEncontrado == null);

        }
    
    }

    public class Cliente 
    {
        public string Nombre {get; set;}
        public string Direccion {get; set;}
        public long Telefono {get; set;}
        public string DatosReferenciaDireccion {get;set;}

        public Cliente(string nombre, string direccion, long telefono, string datosReferenciaDireccion) //constructor
        {
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            DatosReferenciaDireccion = datosReferenciaDireccion;
        }
    }

}