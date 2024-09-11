using EspacioAccesoADatos;
using EspacioCadeteria;
using EspacioCadete;

namespace EspacioAccesoAJson
{
    public class AccesoAJson : AccesoADatos
    {
        public Cadeteria CargarArchivo(string ArchivoCadeteria, List<Cadete> ListaCadetes){
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