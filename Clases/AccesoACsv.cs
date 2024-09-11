using EspacioAccesoADatos;
using EspacioCadeteria;
using EspacioCadete;

namespace EspacioAccesoACSV
{
    public class AccesoACSV : AccesoADatos
    {
        public bool Existe(string archivo)
        {
            string ruta = "CSV/"+archivo+".csv";
            return File.Exists(ruta);
        }
        public List<Cadete> CargarArchivoCadetes(string ArchivoCadetes)
        {
            ArchivoCadetes = "CSV/"+ArchivoCadetes+".csv";
            List<Cadete> ListaCadetes = new List<Cadete>(); 
  
            using (var archivoOpen = new FileStream(ArchivoCadetes, FileMode.Open)) //se abre el archvo - using para liberar recursos despues del bloque
            {
                using (var strReader = new StreamReader(archivoOpen)) //Se lee el arrchivo completo
                {
                    string linea;
                    while ((linea = strReader.ReadLine()) != null)
                    {
                        string[] datos = linea.Split(',');  //creo arreglo con cada dato para cada cadete segun el formato
                        Cadete cadete = new Cadete(Convert.ToInt32(datos[0]), datos[1], datos[2], Convert.ToInt64(datos[3]));
                        ListaCadetes.Add(cadete); //agrego cadete a la lista
                    }
                }
            }
            return ListaCadetes;
        }
        public Cadeteria CargarArchivoCadeteria(string ArchivoCadeteria, List<Cadete> ListaCadetes)
        {
            ArchivoCadeteria = "CSV/"+ArchivoCadeteria+".csv";
            Cadeteria cadeteria = new Cadeteria();

            using (var archivoOpen = new FileStream(ArchivoCadeteria, FileMode.Open))
            {
                using (var strReader = new StreamReader(archivoOpen))
                {
                    string linea; 
                    while ((linea = strReader.ReadLine()) != null)
                    {
                        string[] datos = linea.Split(',');
                        cadeteria = new Cadeteria(datos[0], Convert.ToInt64(datos[1]), ListaCadetes);
                    }
                }
            }
            return cadeteria;           
        }
    }
}