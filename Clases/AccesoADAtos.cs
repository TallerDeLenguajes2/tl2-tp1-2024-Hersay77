using EspacioCadeteria;
using EspacioCadete;

namespace EspacioAccesoADatos
{
    interface AccesoADatos
    {
        List<Cadete> CargarArchivoCadetes(string ruta);
        Cadeteria CargarArchivoCadeteria(string ruta, List<Cadete> ListaCadetes);
    }
}
