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
                nro++; //aumento 
            break;
            case 2:

            break;
            case 3:
            break;
            case 4:
            break;
            case 5:
            break;
            default:
            break;
        }

    } while (opcion >= 1 && opcion <=5);
}
else
{
    Console.WriteLine("Error no se puede inciiar el programa falta: Cadetes.csv o Cadeteria.csv");
}



