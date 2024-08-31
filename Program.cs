using EspacioCadeteria;
using EspacioGUI;

string ArchivoCadetes = "CSV/Cadetes.csv";
string ArchivoCadeteria = "CSV/Cadeteria.csv";

List<Cadete> cadetes = Cadete.CargarCSVCadetes(ArchivoCadeteria);
Cadeteria cadeteria = Cadeteria.CargarCSVCadeteria(ArchivoCadetes, cadetes);


GUI.Menu();
Console.Read();
