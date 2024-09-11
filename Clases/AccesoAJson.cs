using EspacioAccesoADatos;
using EspacioCadeteria;
using EspacioCadete;
using System.Text.Json;

namespace EspacioAccesoAJson
{
    public class AccesoAJSON : AccesoADatos
    {
        public bool Existe(string archivo)
        {
            string ruta = "JSON/"+archivo+".json";
            return File.Exists(ruta);
        }
        public List<Cadete> CargarArchivoCadetes(string ArchivoCadetes)
        {
            ArchivoCadetes = "JSON/"+ArchivoCadetes+".json";
            string cadetesJson;
            using (var archivoOpen = new FileStream(ArchivoCadetes, FileMode.Open))
            {
                using (var strReader = new StreamReader(archivoOpen))
                {
                    cadetesJson = strReader.ReadToEnd();
                    archivoOpen.Close();
                }
            }
            List<Cadete> ListaCadetes = JsonSerializer.Deserialize<List<Cadete>>(cadetesJson);
            return ListaCadetes;   
        }
        public Cadeteria CargarArchivoCadeteria(string ArchivoCadeteria, List<Cadete> ListaCadetes){
            ArchivoCadeteria = "JSON/"+ArchivoCadeteria+".json"; 
            string cadeteriaJson;
            using (var archivoOpen = new FileStream(ArchivoCadeteria, FileMode.Open))
            {
                using (var strReader = new StreamReader(archivoOpen))
                {
                    cadeteriaJson = strReader.ReadToEnd();
                    archivoOpen.Close();
                }
            }
            Cadeteria cadeteria = JsonSerializer.Deserialize<Cadeteria>(cadeteriaJson);
            cadeteria.ListaCadetes = ListaCadetes;
            return cadeteria;    
        }
    }
}