using EspacioCadeteria;
using EspacioGUI;
using EspacioLogicHelp;

string ArchivoCadetes = "CSV/Cadetes.csv";
string ArchivoCadeteria = "CSV/Cadeteria.csv";

if (LogicHelp.Existe(ArchivoCadetes) && LogicHelp.Existe(ArchivoCadeteria))
{
    List<Cadete> ListaCadetes = Cadete.CargarCSVCadetes(ArchivoCadetes);
    Cadeteria cadeteria = Cadeteria.CargarCSVCadeteria(ArchivoCadeteria, ListaCadetes);

    GUI.Menu();
    Console.Read();










}
else
{
    Console.WriteLine("Error no se puede inciiar el programa falta: Cadetes.csv o Cadeteria.csv");
}



