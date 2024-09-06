using EspacioInforme;
using EspacioCadeteria;
using EspacioCadete;
using EspacioGUI;
using EspacioLogicHelp;
using EspacioAccesoADatos;
using EspacioPedido;

string ArchivoCadetes = "CSV/Cadetes.csv";
string ArchivoCadeteria = "CSV/Cadeteria.csv";

if (LogicHelp.Existe(ArchivoCadetes) && LogicHelp.Existe(ArchivoCadeteria))
{
    List<Cadete> ListaCadetes = AccesoADatos.CargarCSVCadetes(ArchivoCadetes);
    Cadeteria cadeteria = AccesoADatos.CargarCSVCadeteria(ArchivoCadeteria, ListaCadetes);
    List<Pedido> ListaGeneralPedidos = new List<Pedido>(); //Creo lista general de pedidos vacia

    string entrada;
    int opcion, nro = 1;
    do
    {
        do
        {
            GUI.Menu();
            entrada = Console.ReadLine();

        } while (!int.TryParse(entrada, out opcion) || !LogicHelp.ControlMenuP(opcion));

        switch (opcion)
        {
            case 1:
                var NuevoPedido = new Pedido(nro); //llamo al constructor de pedido
                ListaGeneralPedidos.Add(NuevoPedido); //agrego pedido a la lista general
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("PEDIDO AGREGADO A LA LISTA GENERAL DE PEDIDOS");
                Console.ForegroundColor = ConsoleColor.White;
                nro++; //aumento numero de pedido
                break;
            case 2:
                Cadeteria.AsignarPedido(ListaGeneralPedidos, ListaCadetes);
                break;
            case 3:
                Cadeteria.CambiarEstado(ListaGeneralPedidos);
                break;
            case 4:
                Cacdeteria.ReasignarPedido(ListaGeneralPedidos, ListaCadetes);
                break;
            case 5:
                Console.WriteLine("FINAL DE JORNADA - MOSTRANDO INFORME");
                Informe.MostrarInforme(ListaCadetes);
                Console.WriteLine("SALIENDO...");
                break;
            default:
                break;
        }

    } while (opcion >= 1 && opcion <= 4);
}
else
{
    Console.WriteLine("Error no se puede inciar el programa falta: Cadetes.csv o Cadeteria.csv");
}

