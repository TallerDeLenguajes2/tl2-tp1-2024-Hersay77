using EspacioPedido;
using EspacioCadete;

namespace EspacioMetodosHelper
{
    public class MetodosHelper
    {
        public static Pedido SelectPedidosNoAsignados(List<Pedido> ListaPedidos){

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
                            return pedidoEncontrado;
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
                } while (!entradacorrecta || pedidoEncontrado == null);
            }
            else
            {
                Console.WriteLine("NO HAY PEDIDOS NO ASIGNADOS");
            }
            return null;
        }
    
        public static Cadete SelectCadete(List<Cadete> ListaCadetes){
            string entrada;
            bool entradacorrecta;
            int nroABuscar;
            Cadete cadeteEncontrado = null;
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
                            return cadeteEncontrado;
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
                } while (!entradacorrecta);
                return null;
        }
    }
}