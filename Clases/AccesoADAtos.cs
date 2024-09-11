using EspacioCadeteria;
using EspacioCadete;

namespace EspacioAccesoADatos
{
    interface AccesoADatos
    {
        bool Existe(string nombre);
        List<Cadete> CargarArchivoCadetes(string ruta);
        Cadeteria CargarArchivoCadeteria(string ruta, List<Cadete> ListaCadetes);
    }
}
