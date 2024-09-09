namespace EspacioCliente
{
    public class Cliente
    {
        private string nombre;
        private string direccion;
        private long telefono;
        private string datosReferenciaDireccion;

        public string Nombre { get => nombre;  }
        public string Direccion { get => direccion; }
        public long Telefono { get => telefono; }
        public string DatosReferenciaDireccion { get => datosReferenciaDireccion;  }

        public Cliente(string Nombre, string Direccion, long Telefono, string DatosReferenciaDireccion) //constructor
        {
            this.nombre = Nombre;
            this.direccion = Direccion;
            this.telefono = Telefono;
            this.datosReferenciaDireccion = DatosReferenciaDireccion;
        }
    }
}