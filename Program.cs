using EspacioCadeteria;
using EspacioCadete;
using EspacioGUI;
using EspacioAccesoADatos;
using EspacioPedido;
using EspacioAccesoACSV;
using EspacioAccesoAJson;
using EspacioMetodosHelper;

string ArchivoCadetes = "Cadetes";
string ArchivoCadeteria = "Cadeteria";
string entrada;
int opcion;
AccesoADatos Acceso = null;
List<Cadete> ListaCadetes = null;
Cadeteria cadeteria = null;
do
{
    GUI.MenuAcceso();
    entrada = Console.ReadLine();
    if (!int.TryParse(entrada, out opcion))
    {
        Console.WriteLine("Por favor ingrese un número válido.");
        continue; // Volver a mostrar el menú si la conversión falla
    } 
    switch (opcion)
    {
        case 1:
            Acceso = new AccesoACSV();
        break;
        case 2:
            Acceso = new AccesoAJSON();
        break;
        default:
            Console.WriteLine("INGRESO INCORRECTO");
        break;
    }
} while (opcion != 1 && opcion !=2);

    if (Acceso.Existe(ArchivoCadetes) && Acceso.Existe(ArchivoCadeteria))
    {
        ListaCadetes = Acceso.CargarArchivoCadetes(ArchivoCadetes); //Cargo Lista Cadetes
        cadeteria = Acceso.CargarArchivoCadeteria(ArchivoCadeteria, ListaCadetes); //Cargo datos de Cadeteria en una instancia cadeteria
        
        int numeroPedido = 1;
        bool completado;
        do
        {
            GUI.Menu(cadeteria);
            entrada = Console.ReadLine();
            int.TryParse(entrada, out opcion);

            switch (opcion)
            {
                case 1:
                    string[] informacionPedido = MetodosHelper.CrearPedido();
                    completado = cadeteria.AltaPedido(numeroPedido,informacionPedido[0], informacionPedido[1], informacionPedido[2], informacionPedido[3], informacionPedido[4], informacionPedido[5]);
                    if (completado)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("PEDIDO AGREGADO A LA LISTA GENERAL DE PEDIDOS");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR NO SE PUDDO AGREGAR EL PEDIDO");
                        Console.ResetColor();
                    }
                    numeroPedido++; //aumento numero de pedido
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("####### ASIGNANDO CADETE A PEDIDO #########");
                    Console.ResetColor();
                    List<Pedido> PedidosNoAsignados = cadeteria.ListaPedidos.Where(pedido => pedido.Cadete == null).ToList();
                    completado =  cadeteria.AsignarCadeteAPedido((MetodosHelper.SelectCadete(ListaCadetes)),(MetodosHelper.SelectPedidos(PedidosNoAsignados)));
                    if (completado)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("PEDIDO ASIGNADO");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("NO HAY PEDIDOS PARA ASIGNARLES CADETES");
                        Console.ResetColor();
                    }
                    break;
                case 3:
                    int nroPedido = (MetodosHelper.SelectPedidos(cadeteria.ListaPedidos));
                    int estado = MetodosHelper.ControlEstado();
                    completado = cadeteria.CambiarEstado(nroPedido, estado);
                    if (completado)
                    {   
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("SE CAMBIO EL ESTADO DEL PEDIDO");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("NO SE PUDO CAMBIAR EL ESTADO AL PEDIDO");
                        Console.ResetColor();
                    }
                    break;
                case 4:
                    Console.WriteLine("### REASIGNANDO PEDIDO: ###");
                    List<Pedido> PedidosAsignados = cadeteria.ListaPedidos.Where(pedido => pedido.Cadete != null).ToList();
                    if (PedidosAsignados.Count != 0)
                    {
                        completado = cadeteria.AsignarCadeteAPedido(MetodosHelper.SelectCadete(cadeteria.ListaCadetes),MetodosHelper.SelectPedidos(PedidosAsignados));
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("SE REASIGNO EL PEDIDO");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("AUN NO SE ASIGNARON PEDIDOS A NINGUN CADETE");
                        Console.ResetColor();
                    }                  
                    break;
                case 5:
                    if (cadeteria.ListaPedidos != null || cadeteria.ListaPedidos.Count !=0) //al usar csv se crea lista null, al usar json lista vacia
                    {
                        MetodosHelper.MostrarInforme(cadeteria);
                    }
                    else
                    {
                        Console.WriteLine("NO SE REALIZARON PEDIDOS ESATA JORNADA");
                    }
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("OPCION NO VALIDA - VUELVA A INGRESAR");
                    break;
            }
        } while (opcion !=5);
    }
    else
    {
        Console.WriteLine("Error de programa: no se puede inciar porque faltan Archivos .CSV o .JSON");
    }






