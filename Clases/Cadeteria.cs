using EspacioCadete;
using EspacioPedido;

namespace EspacioCadeteria
{
    public class Cadeteria
    {
        public string Nombre { get; set; }
        public long Telefono { get; set; }
        public List<Cadete> ListaCadetes { get; set; }

        public Cadeteria(string Nombre, long Telefono, List<Cadete> ListaCadetes) //constructor
        {
            this.Nombre = Nombre;
            this.Telefono = Telefono;
            this.ListaCadetes = ListaCadetes;
        }

        public Cadeteria() //Constructor "Vacio"
        {
            this.ListaCadetes = new List<Cadete>();
        }

        public static Pedido AltaPedido(int Nro)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("######### DANDO DE ALTA UN PEDIDO #########\n");
            Console.ForegroundColor = ConsoleColor.White;

            string Obs, Nombre, Direccion, Entrada, DatosReferenciaDireccion;
            long Telefono;
            int Estado;
            bool Bandera;
            /*ingreso nro automatico*/
            Console.WriteLine("Ingrese Pedido y observaciones: ");
            Obs = Console.ReadLine();
            Console.WriteLine("Ingrese Nombre Cliente: ");
            Nombre = Console.ReadLine();
            Console.WriteLine("Ingrese Direccion del Cliente: ");
            Direccion = Console.ReadLine();
            Console.WriteLine("Ingrese Telefono Cliente: ");
            do
            {
                Entrada = Console.ReadLine();
                Bandera = long.TryParse(Entrada, out Telefono);
                if (!Bandera)
                {
                    Console.WriteLine("Ingreso Incorrecto. Vuelva a ingresar un numero");
                }
            } while (!Bandera);
            Console.WriteLine("Ingrese Datos de Referencia de la Direccion: ");
            DatosReferenciaDireccion = Console.ReadLine();
            Console.WriteLine("Ingrese Estado del pedido: 1 = Entregado y 0 = No Entregado");
            do
            {
                Entrada = Console.ReadLine();
                Bandera = int.TryParse(Entrada, out Estado);
                if (!Bandera || (Estado != 1 && Estado != 0))
                {
                    Console.WriteLine("Ingreso Incorrecto. Vuelva a ingresar un numero");
                }
            } while (!Bandera || (Estado != 1 && Estado != 0));

            //construyendo pedido
            Pedido Pedido = new Pedido(Nro, Obs, Nombre, Direccion, Telefono, DatosReferenciaDireccion, Estado);
            Console.ResetColor();
            return Pedido;
        }

        public static void AsignarPedido(List<Pedido> ListaGeneralPedidos, List<Cadete> ListaCadetes)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("####### ASIGNANDO PEDIDO A CADETE #########");
            Console.ResetColor();
            Console.WriteLine("SELECCIONE UN PEDIDO: ");
            foreach (var pedido in ListaGeneralPedidos)
            {
                Pedido.MostrarPedido(pedido);
            }
            int nroABuscar;
            string entrada;
            bool entradacorrecta;
            Pedido pedidoEncontrado;
            do
            {
                Console.WriteLine("INGRESE NUMERO DE PEDIDO: ");
                entrada = Console.ReadLine();
                entradacorrecta = int.TryParse(entrada, out nroABuscar);
                pedidoEncontrado = ListaGeneralPedidos.Find(Pedido => Pedido.Nro == nroABuscar);
                if (entradacorrecta && pedidoEncontrado != null)
                {
                    Cadete cadeteEncontrado;
                    do
                    {
                        Console.WriteLine("SELECCIONE UN CADETE");
                        foreach (var cadete in ListaCadetes)
                        {
                            Cadete.MostrarCadete(cadete);
                        }
                        Console.WriteLine("INGRESE EL ID DEL CADETE: ");
                        entrada = Console.ReadLine();
                        entradacorrecta = int.TryParse(entrada, out nroABuscar);
                        cadeteEncontrado = ListaCadetes.Find(Cadete => Cadete.Id == nroABuscar);
                        if (entradacorrecta && cadeteEncontrado != null)
                        {
                            cadeteEncontrado.ListaPedidos.Add(pedidoEncontrado); //agrego el pedido a la lista del cadete
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("PEDIDO ASIGNADO A CADETE");
                            Console.ResetColor();
                            ListaGeneralPedidos.Remove(pedidoEncontrado);
                        }
                        else
                        {
                            Console.WriteLine("NO SE ENCONTRO EL CADETE - INGRESO INCORRECTO");
                        }
                    } while (!entradacorrecta || cadeteEncontrado == null);

                }
                else
                {
                    Console.WriteLine("NO SE ENCONTRO EL PEDIDO - INGRESO INCORRECTO");
                }
            } while (!entradacorrecta || pedidoEncontrado == null);
        }

        public static void CambiarEstado(List<Cadete> ListaCadete)
        {
            Console.WriteLine("### CAMBIANDO ESTADO A UN PEDIDO: ###");

            List<Cadete> CadetesConPedidos = ListaCadete.Where(c => c.ListaPedidos != null && c.ListaPedidos.Any()).ToList(); //Busco en la lista de cadetes si alguno tiene un pedido asignado - los que tengan al menos un pedido seguardan en la lista de cadetes con pedidos

            if (CadetesConPedidos.Count != 0)
            {
                Pedido pedidoEncontrado = null;
                do
                {
                    Console.WriteLine("LISTA DE PEDIDOS ASIGNADOS: ");
                    foreach (var cadete in CadetesConPedidos)
                    {
                        foreach (var pedido in cadete.ListaPedidos)
                        {
                            Pedido.MostrarPedido(pedido);
                        }
                    }
                    int nroABuscar;
                    string entrada;
                    bool entradacorrecta;

                    Console.WriteLine("SELECCIONE UN PEDIDO: ");
                    entrada = Console.ReadLine();
                    entradacorrecta = int.TryParse(entrada, out nroABuscar);

                    pedidoEncontrado = CadetesConPedidos.SelectMany(cadete => cadete.ListaPedidos).FirstOrDefault(pedido => pedido.Nro == nroABuscar); // Encuentra el primer pedido que coincida

                    if (pedidoEncontrado != null)
                    {
                        Console.WriteLine("PEDIDO ENCONTRADO: ");
                        Pedido.MostrarPedido(pedidoEncontrado);
                        int estado;
                        do
                        {
                            Console.WriteLine("INGRESE EL NUEVO ESTADO DEL PEDIDO: 1 = Entregado y 0 = No Entregado");
                            entrada = Console.ReadLine();
                            entradacorrecta = int.TryParse(entrada, out estado);
                            if (!entradacorrecta || (estado != 1 && estado != 0))
                            {
                                Console.WriteLine("Ingreso Incorrecto. Vuelva a ingresar un numero");
                            }
                        } while (!entradacorrecta || (estado != 1 && estado != 0));
                        pedidoEncontrado.Estado = estado;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("SE CAMBIO EL ESTADO DEL PEDIDO");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"NO SE ECNONTRO EL PEDIDO CON EL NUMERO {nroABuscar}.");
                    }
                } while (pedidoEncontrado == null); 
            }
            else
            {
                Console.WriteLine("NO HAY PEDIDOS ASIGNADOS PARA CAMBIAR SU ESTADO");
            }
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
                if (entradacorrecta && cadeteEncontrado != null)
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
                            Cadeteria.AsignarPedido(ListaGeneralPedidos, ListaCadetes);
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
}

