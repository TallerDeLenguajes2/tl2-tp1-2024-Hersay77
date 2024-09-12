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
        
        int nro = 1;
        bool completado;
        do
        {
            GUI.Menu(cadeteria);
            entrada = Console.ReadLine();
            int.TryParse(entrada, out opcion);

            switch (opcion)
            {
                case 1:
                    completado = MetodosHelper.CrearPedido(nro, cadeteria.ListaPedidos);
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
                    nro++; //aumento numero de pedido
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("####### ASIGNANDO PEDIDO A CADETE #########");
                    Console.ResetColor();
                    completado =  cadeteria.AsignarPedido();
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
                    completado = cadeteria.CambiarEstado();
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
                    completado = cadeteria.ReasignarPedido();
                    if (completado)
                    {   
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
                    Console.WriteLine("FINAL DE JORNADA - MOSTRANDO INFORME");
                    cadeteria.MostrarInforme();
                    Console.WriteLine("SALIENDO...");
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






