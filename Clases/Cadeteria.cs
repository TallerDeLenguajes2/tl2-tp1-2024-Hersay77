using EspacioCadete;
using EspacioMetodosHelper;
using EspacioPedido;

namespace EspacioCadeteria
{
    public class Cadeteria
    {
        private string nombre;
        private long telefono;
        private List<Cadete> listaCadetes;
        private List<Pedido> listaPedidos; 

        public string Nombre { get => nombre; set => nombre = value;}
        public long Telefono { get => telefono; set => telefono = value;}
        public List<Cadete> ListaCadetes { get => listaCadetes; set => listaCadetes = value;}
        public List<Pedido> ListaPedidos { get => listaPedidos; set => listaPedidos = value;}

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
                if (!Bandera) //devuelve 0 si no se logra la conversion
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
                var PedidoEncontrado = MetodosHelper.SelectPedidos(PedidosNoAsignados);
                if (PedidoEncontrado != null)
                {
                    var CadeteEncontrado = MetodosHelper.SelectCadete(ListaCadetes);
                    if (CadeteEncontrado != null)
                    {
                        AsignarCadeteAPedido(CadeteEncontrado.Id, PedidoEncontrado.Nro);
                    }
                }
            }
            else
            {
                Console.WriteLine("NO HAY PEDIDOS PARA ASIGNARLES CADETES");
            }
            
        }
        public void CambiarEstado()
        {
            Console.WriteLine("### CAMBIANDO ESTADO A UN PEDIDO: ###");
            string entrada;
            bool entradacorrecta;
            int estado;
            Pedido PedidoEncontrado = MetodosHelper.SelectPedidos(ListaPedidos);
            if (PedidoEncontrado != null)
            {
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
                PedidoEncontrado.Estado = estado;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("SE CAMBIO EL ESTADO DEL PEDIDO");
                Console.ResetColor();
            }
        }
        public void ReasignarPedido()
        {
            List<Pedido> PedidosAsignados = ListaPedidos.Where(pedido => pedido.Cadete != null).ToList(); //armo lista de pedidos con cadetes asignados
            if (PedidosAsignados.Count != 0)
            {
                Console.WriteLine("MOSTRANDO PEDIDOS ASIGNADOS Y SU CORRESPONDIENTE CADETE");
                Pedido PedidoEncontrado = MetodosHelper.SelectPedidos(PedidosAsignados);
                if (PedidoEncontrado != null)
                    {
                        Cadete CadeteEncontrado = MetodosHelper.SelectCadete(ListaCadetes);
                        if (CadeteEncontrado != null)
                        {
                        AsignarCadeteAPedido(CadeteEncontrado.Id, PedidoEncontrado.Nro);
                        }
                    }
            }
            else
            {
                Console.WriteLine("AUN NO SE ASIGNARON PEDIDOS A NINGUN CADETE");
            }
        }    
        public void MostrarInforme()
        {
            if (ListaPedidos != null || ListaPedidos.Count !=0) //al usar csv se crea lista null, al usar json lista vacia
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
                Console.WriteLine($"Total Envios: {totalEnviosCompletados}"); 
                Console.WriteLine($"Promedio de envios completado por cadete: {promedioEnviosPorCadete}");
            }
            else
            {
                Console.WriteLine("NO SE REALIZARON PEDIDOS ESATA JORNADA");
            }
        }
        public float JornalACobrar(int id)
        {
            float jornal = 0;
            int pedidosCompletadosPorElCadete = ListaPedidos.Count(pedido => pedido.Cadete != null && pedido.Cadete.Id == id && pedido.Estado == 1);

            if (pedidosCompletadosPorElCadete != 0)
            {
                jornal = pedidosCompletadosPorElCadete * 500;
            }
            
            return jornal;
        } 
        public void AsignarCadeteAPedido(int idCadete, int idPedido)
        {
            var pedidoEncontrado = listaPedidos.Find(pedido => pedido.Nro == idPedido);
            var cadeteEncontrado = listaCadetes.Find(cadete => cadete.Id == idCadete);

            pedidoEncontrado.Cadete = cadeteEncontrado;             
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("CADETE ASIGNADO AL PEDIDO");
            Console.ResetColor();
        }
    }
}

