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
        public static Pedido AltaPedido(int Nro, string Obs, string Nombre, string Direccion, long Telefono, string DatosReferenciaDireccion, int Estado)
        {
            Pedido NuevoPedido = new Pedido(Nro, Obs, Nombre, Direccion, Telefono, DatosReferenciaDireccion, Estado);
            return NuevoPedido;
        }
        public bool AsignarCadeteAPedido(int idCadete, int idPedido)
        {
            var pedidoEncontrado = listaPedidos.Find(pedido => pedido.Nro == idPedido);
            var cadeteEncontrado = listaCadetes.Find(cadete => cadete.Id == idCadete);
            if (pedidoEncontrado != null && cadeteEncontrado != null)
            {
                pedidoEncontrado.Cadete = cadeteEncontrado; 
                return true;
            }
            else
            {
                return false;
            }           
        }
        public bool CambiarEstado()
        {
            Pedido PedidoEncontrado = MetodosHelper.SelectPedidos(ListaPedidos);
            if (PedidoEncontrado != null)
            {
                PedidoEncontrado.Estado = MetodosHelper.ControlEstado();
                return true;
            }
            return false;
        }
        public bool ReasignarPedido()
        {
            List<Pedido> PedidosAsignados = ListaPedidos.Where(pedido => pedido.Cadete != null).ToList(); //armo lista de pedidos con cadetes asignados
            if (PedidosAsignados.Count != 0)
            {
                Pedido PedidoEncontrado = MetodosHelper.SelectPedidos(PedidosAsignados);
                if (PedidoEncontrado != null)
                    {
                        Cadete CadeteEncontrado = MetodosHelper.SelectCadete(ListaCadetes);
                        if (CadeteEncontrado != null)
                        {
                            AsignarCadeteAPedido(CadeteEncontrado.Id, PedidoEncontrado.Nro);
                            return true;
                        }
                    }
            }
            return false;
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

    }
}

