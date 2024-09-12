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
                    completado = cadeteria.AltaPedido(nro);
                    if (completado)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("PEDIDO AGREGADO A LA LISTA GENERAL DE PEDIDOS");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.WriteLine("ERROR NO SE PUDDO AGREGAR EL PEDIDO");
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
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.WriteLine("NO HAY PEDIDOS PARA ASIGNARLES CADETES");
                    }
                    break;
                case 3:
                    cadeteria.CambiarEstado();
                    break;
                case 4:
                    cadeteria.ReasignarPedido();
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






