using EspacioCadeteria;
using EspacioCadete;

namespace EspacioAccesoADatos
{
    public class AccesoADatos
    {
        public static Cadeteria CargarCSVCadeteria(string ArchivoCadeteria, List<Cadete> ListaCadetes)
        {
            string[] lineas = File.ReadAllLines(ArchivoCadeteria); //leo archivo csv cadeteria

            string[] datos = lineas[0].Split(','); //creo arreglo con cada dato para la cadeteria

            Cadeteria cadeteria = new Cadeteria(datos[0], Convert.ToInt64(datos[1]), ListaCadetes);

            return cadeteria;
        }

        public static List<Cadete> CargarCSVCadetes(string ArchivoCadetes)
        {
            List<Cadete> ListaCadetes = new List<Cadete>(); //nueva lista de cadetes vacia

            string[] lineas = File.ReadAllLines(ArchivoCadetes); //leo archivo csv en un arreglo

            for (int i = 0; i < lineas.Length; i++) //recorre arreglo
            {
                string linea = lineas[i]; //selecciono una linea del archivo csv
                string[] datos = linea.Split(','); //creo arreglo con cada dato para cada cadete segun el formato

                Cadete cadete = new Cadete(Convert.ToInt32(datos[0]), datos[1], datos[2], Convert.ToInt64(datos[3]));

                ListaCadetes.Add(cadete); //agrego cadete a la lista
            }
            return ListaCadetes;
        }

    }
}