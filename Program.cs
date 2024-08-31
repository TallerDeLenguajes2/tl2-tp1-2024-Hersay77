using EspacioCadeteria;
using EspacioGUI;
using EspacioLogicHelp;

string ArchivoCadetes = "CSV/Cadetes.csv";
string ArchivoCadeteria = "CSV/Cadeteria.csv";

if (LogicHelp.Existe(ArchivoCadetes) && LogicHelp.Existe(ArchivoCadeteria))
{
    List<Cadete> ListaCadetes = Cadete.CargarCSVCadetes(ArchivoCadetes);
    Cadeteria cadeteria = Cadeteria.CargarCSVCadeteria(ArchivoCadeteria, ListaCadetes);

    string entrada;
    int opcion;
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



