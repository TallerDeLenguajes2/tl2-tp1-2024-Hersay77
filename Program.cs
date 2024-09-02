using EspacioCadeteria;
using EspacioGUI;
using EspacioLogicHelp;

string ArchivoCadetes = "CSV/Cadetes.csv";
string ArchivoCadeteria = "CSV/Cadeteria.csv";

if (LogicHelp.Existe(ArchivoCadetes) && LogicHelp.Existe(ArchivoCadeteria))
{
    List<Cadete> ListaCadetes = Cadete.CargarCSVCadetes(ArchivoCadetes);
    Cadeteria cadeteria = Cadeteria.CargarCSVCadeteria(ArchivoCadeteria, ListaCadetes);
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
                Console.WriteLine("PEDIDO AGREGADO A LA LISTA GENERAL DE PEDIDOS");
                nro++; //aumento 
            break;
            case 2:
                Pedido.AsignarPedido(ListaGeneralPedidos, ListaCadetes);
            break;
            case 3:
                Pedido.CambiarEstado(ListaGeneralPedidos);
            break;
            case 4:
                Pedido.ReasignarPedido(ListaGeneralPedidos, ListaCadetes);
            break;
            case 5:
                Console.WriteLine("SALIENDO...");
            break;
            default:
            break;
        }

    } while (opcion >= 1 && opcion <=4);
}
else
{
    Console.WriteLine("Error no se puede inciiar el programa falta: Cadetes.csv o Cadeteria.csv");
}



