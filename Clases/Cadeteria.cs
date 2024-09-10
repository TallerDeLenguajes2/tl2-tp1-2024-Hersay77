using EspacioCadete;
using EspacioPedido;

namespace EspacioCadeteria
{
    public class Cadeteria
    {
        private string nombre;
        private long telefono;
        private List<Cadete> listaCadetes;
        private List<Pedido> listaPedidos; 

        public string Nombre { get => nombre; }
        public long Telefono { get => telefono; }
        public List<Cadete> ListaCadetes { get => listaCadetes;}
        public List<Pedido> ListaPedidos { get => listaPedidos;}

        public Cadeteria(string Nombre, long Telefono, List<Cadete> ListaCadetes) //constructor
        {
            this.nombre = Nombre;
            this.telefono = Telefono;
            this.listaCadetes = ListaCadetes;
            listaCadetes = ListaCadetes;
            listaPedidos = new List<Pedido>();
        }

        public Cadeteria(){

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
        public void AsignarPedido()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("####### ASIGNANDO PEDIDO A CADETE #########");
            Console.ResetColor();

            List<Pedido> PedidosNoAsignados = ListaPedidos.Where(pedido => pedido.Cadete == null).ToList(); //armo lista de pedidos NO asignados

            if (PedidosNoAsignados.Count != 0)
            {
                Console.WriteLine("SELECCIONE UN PEDIDO: ");
                foreach (var pedido in PedidosNoAsignados)
                {
                    Pedido.MostrarPedido(pedido);
                }
                int nroABuscar;
                string entrada;
                bool entradacorrecta;
                Pedido pedidoEncontrado = null;
                Cadete cadeteEncontrado = null;
                do
                {
                    Console.WriteLine("INGRESE NUMERO DE PEDIDO: ");
                    entrada = Console.ReadLine();
                    entradacorrecta = int.TryParse(entrada, out nroABuscar);
                    if (entradacorrecta)
                    {
                        pedidoEncontrado = PedidosNoAsignados.Find(Pedido => Pedido.Nro == nroABuscar);
                        if (pedidoEncontrado != null)
                        {
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
                                if (entradacorrecta)
                                {
                                    cadeteEncontrado = ListaCadetes.Find(Cadete => Cadete.Id == nroABuscar);
                                    if (cadeteEncontrado != null)
                                    {
                                        pedidoEncontrado.Cadete = cadeteEncontrado; //asigno cadete
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.WriteLine("PEDIDO ASIGNADO A CADETE");
                                        Console.ResetColor();
                                    }
                                    else
                                    {
                                        Console.WriteLine("NO SE ENCONTRO EL CADETE");
                                    }

                                }
                                else
                                {
                                    Console.WriteLine("INGRESO INCORRECTO");
                                }                            
                            } while (!entradacorrecta || cadeteEncontrado == null);

                        }
                        else
                        {
                            Console.WriteLine("NO SE ENCONTRO EL PEDIDO");
                        }
                    }
                    else
                    {
                        Console.WriteLine("INGRESO INCORRECTO");
                    }                
                } while (!entradacorrecta || pedidoEncontrado == null || cadeteEncontrado == null);
            }
            else
            {
                Console.WriteLine("NO HAY PEDIDOS PARA ASIGNAR");
            }




            
        }
        public void CambiarEstado()
        {
            Console.WriteLine("### CAMBIANDO ESTADO A UN PEDIDO: ###");
            int nroABuscar;
            string entrada;
            bool entradacorrecta;
            Pedido pedidoEncontrado;

            Console.WriteLine("SELECCIONE UN PEDIDO: ");
            foreach (var pedido in ListaPedidos)
            {
                Pedido.MostrarPedido(pedido);
            }
            entrada = Console.ReadLine();
            entradacorrecta = int.TryParse(entrada, out nroABuscar);

            if (entradacorrecta)
            {
                pedidoEncontrado = ListaPedidos.Find(pedido => pedido.Nro == nroABuscar); // Encuentra el primer pedido que coincida

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
                        Console.WriteLine("NO SE ENCONTRO EL PEDIDO");    
                    }
            }
            else
            {
                Console.WriteLine("ENTRADA INCORRECTA");    
            }

        }
        public void ReasignarPedido()
        {
            List<Pedido> PedidosAsignados = ListaPedidos.Where(pedido => pedido.Cadete != null).ToList(); //armo lista de pedidos con cadetes asignados
            
            if (PedidosAsignados.Count != 0)
            {
                Console.WriteLine("MOSTRANDO PEDIDOS ASIGNADOS Y SU CORRESPONDIENTE CADETE");
                foreach (var pedido in PedidosAsignados)
                {
                    Pedido.MostrarPedido(pedido);
                }

                int idCadete;
                int idPedido;
                string entrada;
                bool entradacorrecta;

                do
                {
                    Console.WriteLine("SELECCIONE EL NUMERO DE PEDIDO A REASINGAR");
                    entrada = Console.ReadLine();
                    entradacorrecta = int.TryParse(entrada, out idPedido);
                    if (entradacorrecta)
                    {
                        Console.WriteLine("SELECCIONE EL ID DEL CADETE");
                        Console.WriteLine("MOSTRANDO CADETES");
                        foreach (var cadete in ListaCadetes)
                        {
                            Cadete.MostrarCadete(cadete);
                        }
                        entrada = Console.ReadLine();
                        entradacorrecta = int.TryParse(entrada, out idCadete);
                        if (entradacorrecta)
                        {
                            bool asignacion = AsignarCadeteAPedido(idCadete, idPedido);
                            if (asignacion)
                            {
                                Console.WriteLine("CADETE ASIGNADO AL PEDIDO");
                            }
                            else
                            {
                                Console.WriteLine("NO SE PUDO ASIGNAR EL CADETE AL PEDIDO");
                            }
                        }
                        else
                        {
                            Console.WriteLine("ENTRADA INCORECTA");
                        }
                    }   
                    else
                    {
                        Console.WriteLine("ENTRADA INCORECTA");
                    }
                } while (!entradacorrecta);
            }
            else
            {
                Console.WriteLine("AUN NO SE ASIGNARON PEDIDOS A NINGUN CADETE");
            }
        }    
        public void MostrarInforme()
        {
            int totalEnviosCompletados = 0;
                
            Console.WriteLine("INFORME DE CADETES: ");
            foreach (var cadete in ListaCadetes)
            {
                float montoACobrarCadete = JornalACobrar(cadete.Id);
                int pedidosCompletadosPorElCadete = (int)montoACobrarCadete/500;
                if (montoACobrarCadete != 0)
                {
                    totalEnviosCompletados += pedidosCompletadosPorElCadete;
                }

                Console.WriteLine($"Nombre Cadete: {cadete.Nombre} | Pedidos Completados: {pedidosCompletadosPorElCadete} | Jornal a cobrar: {montoACobrarCadete}");
            }

            Console.WriteLine("INFORME GENERAL: ");
            float promedioEnviosPorCadete = (float)totalEnviosCompletados/ListaCadetes.Count;
            Console.WriteLine($"Total-Envios: {totalEnviosCompletados}"); 
            Console.WriteLine($"Promedio de envios completado por cadete: {promedioEnviosPorCadete}");
        }
        public float JornalACobrar(int id)
        {
            float jornal = 0;
            int pedidosCompletadosPorElCadete = ListaPedidos.Count(pedido => pedido.Cadete != null && pedido.Cadete.Id == id);

            if (pedidosCompletadosPorElCadete != 0)
            {
                jornal = pedidosCompletadosPorElCadete * 500;
            }
            
            return jornal;
        } 
        public bool AsignarCadeteAPedido(int idCadete, int idPedido)
        {
            Pedido pedidoEncontrado = ListaPedidos.Find(pedido =>pedido.Nro == idPedido);
            if (pedidoEncontrado != null)
            {
                Cadete cadeteParaAsginarPedido = ListaCadetes.Find(cadete => cadete.Id == idCadete);
                if (cadeteParaAsginarPedido != null)
                {
                    if (pedidoEncontrado.Cadete != cadeteParaAsginarPedido)
                    {
                        pedidoEncontrado.Cadete = cadeteParaAsginarPedido;
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("EL CADETE YA TIENE ASIGNADO ESTE PEDIDO");
                    }
                }
                else
                {
                    Console.WriteLine("NO SE EL CADETE");
                }
            }
            else
            {
                Console.WriteLine("NO SE ENCONTRO EL PEDIDO");
            }
            return false;         
        }
    }
}

