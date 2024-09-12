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
        public bool AltaPedido(int Nro, string Obs, string Nombre, string Direccion, string Telefono, string DatosReferenciaDireccion, string Estado)
        {
            Pedido NuevoPedido = new Pedido(Nro, Obs, Nombre, Direccion, long.Parse(Telefono), DatosReferenciaDireccion, int.Parse(Estado));
            if (NuevoPedido != null)
            {
                ListaPedidos.Add(NuevoPedido);
                return true;
            }
            return false;
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
        public bool CambiarEstado(int nroPedido, int estado)
        {
            Pedido PedidoEncontrado = ListaPedidos.Find(pedido => pedido.Nro == nroPedido);
            if (PedidoEncontrado != null)
            {
                PedidoEncontrado.Estado = estado;
                return true;
            }
            return false;
        }  
        public float[] InformeCadete(int IdCadete)
        {
            float montoACobrarCadete = JornalACobrar(IdCadete);
            int pedidosCompletadosPorElCadete = (int)montoACobrarCadete/500;
            float[] informe = {pedidosCompletadosPorElCadete, montoACobrarCadete};
            return informe;
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

