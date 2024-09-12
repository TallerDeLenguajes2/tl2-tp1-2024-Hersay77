using EspacioPedido;
using EspacioCadete;

namespace EspacioMetodosHelper
{
    public class MetodosHelper
    {
        public static Pedido SelectPedidos(List<Pedido> ListaPedidos)
        {
            Console.WriteLine("SELECCIONE UN PEDIDO: ");
            foreach (var pedido in ListaPedidos)
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
                    pedidoEncontrado = ListaPedidos.Find(Pedido => Pedido.Nro == nroABuscar);
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

        public static Pedido CrearPedido(int Nro)
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

        public static int ControlEstado(){
            string entrada;
            bool entradacorrecta;
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
            return estado;
        }
    }
}