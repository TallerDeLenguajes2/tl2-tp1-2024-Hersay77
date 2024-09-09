using EspacioCadete;
using EspacioPedido;

namespace EspacioCadeteria
{
    public class Cadeteria
    {
        private string nombre;
        private long telefono;
        private List<Cadete> listaCadetes;

        public string Nombre { get => nombre; }
        public long Telefono { get => telefono; }
        public List<Cadete> ListaCadetes { get => listaCadetes;}

        public Cadeteria(string Nombre, long Telefono, List<Cadete> ListaCadetes) //constructor
        {
            this.nombre = Nombre;
            this.telefono = Telefono;
            this.listaCadetes = ListaCadetes;
        }

        public Cadeteria() //Constructor "Vacio"
        {
            this.listaCadetes = new List<Cadete>();
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

        public static void CambiarEstado(List<Cadete> ListaCadetes)
        {
            Console.WriteLine("### CAMBIANDO ESTADO A UN PEDIDO: ###");

            List<Cadete> CadetesConPedidos = ListaCadetes.Where(c => c.ListaPedidos != null && c.ListaPedidos.Any()).ToList(); //Busco en la lista de cadetes si alguno tiene un pedido asignado - los que tengan al menos un pedido seguardan en la lista de cadetes con pedidos

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
            List<Cadete> CadetesConPedidos = ListaCadetes.Where(c => c.ListaPedidos != null && c.ListaPedidos.Any()).ToList(); //Busco en la lista de cadetes si alguno tiene un pedido asignado - los que tengan al menos un pedido se guardan en la lista de cadetes con pedidos
            if (CadetesConPedidos.Count != 0)
            {
                Pedido pedidoEncontrado = null;
                Cadete cadeteEncontrado = null, cadeteParaAsginarPedido = null;
                string entrada;
                bool entradacorrecta;
                int nroABuscar;
                do
                {
                    Console.WriteLine("MOSTRANDO CADETES CON PEDIDOS: ");
                    foreach (var cadete in CadetesConPedidos)
                    {
                        Cadete.MostrarCadete(cadete);
                    }
                    Console.WriteLine("INGRESE EL ID DEL CADETE QUE TIENE EL PEDIDO A REASIGNAR: ");
                    entrada = Console.ReadLine();
                    entradacorrecta = int.TryParse(entrada, out nroABuscar);

                    if (entradacorrecta)
                    {
                        cadeteEncontrado = CadetesConPedidos.Find(Cadete => Cadete.Id == nroABuscar);
                        if (cadeteEncontrado != null)
                        {
                            Console.WriteLine("MOSTRANDO PEDIDOS DEL CADETE");
                            foreach (var pedido in cadeteEncontrado.ListaPedidos)
                            {
                                Pedido.MostrarPedido(pedido);
                            }
                            Console.WriteLine("SELECCIONE UN PEDIDO PARA REASIGNAR");
                            entrada = Console.ReadLine();
                            entradacorrecta = int.TryParse(entrada, out nroABuscar);
                            if (entradacorrecta)
                            {
                                pedidoEncontrado = cadeteEncontrado.ListaPedidos.Find(p => p.Nro == nroABuscar);
                                if (pedidoEncontrado != null)
                                {
                                    foreach (var cadete in ListaCadetes)
                                    {
                                        Cadete.MostrarCadete(cadete);
                                    }
                                    Console.WriteLine("SELECCIONE ID DEL CADETE PARA ASIGNAR PEDIDO: ");
                                    entrada = Console.ReadLine();
                                    entradacorrecta = int.TryParse(entrada, out nroABuscar);
                                    if (entradacorrecta)
                                    {
                                        cadeteParaAsginarPedido = ListaCadetes.Find(Cadete => Cadete.Id == nroABuscar);
                                        if (cadeteEncontrado != cadeteParaAsginarPedido)
                                        {
                                            cadeteParaAsginarPedido.ListaPedidos.Add(pedidoEncontrado);
                                            cadeteEncontrado.ListaPedidos.Remove(pedidoEncontrado);
                                            Console.WriteLine("PEDIDO REASIGNADO");
                                        }
                                        else
                                        {
                                            Console.WriteLine("EL CADETE YA TIENE ASIGNADO ESTE PEDIDO");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("INGRESO INCORRECTO VUELVA A INGRESAR");
                                    }

                                }
                                else
                                {
                                    Console.WriteLine("NO SE ENCONOTRO EL PEDIDO");
                                }
                            }
                            else
                            {
                                Console.WriteLine("INGRESO INCORRECTO VUELVA A INGRESAR");
                            }

                        }
                        else
                        {
                            Console.WriteLine("NO SE ECNONTRO EL CADETE CON ESE ID");
                        }
                    }
                    else
                    {
                        Console.WriteLine("INGRESO INCORRECTO VUELVA A INGRESAR");
                    }
                } while (cadeteEncontrado == null || pedidoEncontrado == null || cadeteParaAsginarPedido == null); 
            }
            else
            {
                Console.WriteLine("NO HAY PEDIDOS ASIGNADOS PARA REASIGNAR");
            }
        }
    
        public static void MostrarInforme(List<Cadete> ListaCadetes)
        {
            List<Cadete> CadetesConPedidos = ListaCadetes.FindAll(cadete => cadete.ListaPedidos.Any(pedido => pedido.Estado == 1)); //Guardo en una lista todos los cadetes que tengan pedido completado
            Console.WriteLine("\nINFORME GENERAL:");

            if (CadetesConPedidos != null)
            {
                int totalPedidosCompletados = CadetesConPedidos.Sum(cadete => cadete.ListaPedidos.Count(pedido => pedido.Estado == 1));
                float promedioEnviosPorCadete = totalPedidosCompletados/(float)CadetesConPedidos.Count;
                Console.WriteLine($"Total de envios: {totalPedidosCompletados}");
                Console.WriteLine($"Promedio de envios por cadete: {promedioEnviosPorCadete}");

                Console.WriteLine("INFORME POR CADETE: ");
                int totalEnvioCadete;
                foreach (var cadete in CadetesConPedidos)
                {       
                    totalEnvioCadete = cadete.ListaPedidos.Count(pedido => pedido.Estado == 1);
                    Console.WriteLine($"\tNombre: {cadete.Nombre} || Pedidos completdados por el cadete: {totalEnvioCadete } || Total ganado: ${cadete.JornalACobrar(cadete)}");
                }
            }
            else
            {
                Console.WriteLine("NO HAY CADETES CON PEDIDOS COMPLETADOS");
            }
        }
    }
}

