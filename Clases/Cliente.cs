namespace EspacioCliente
{
    public class Cliente
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public long Telefono { get; set; }
        public string DatosReferenciaDireccion { get; set; }

        public Cliente(string Nombre, string Direccion, long Telefono, string DatosReferenciaDireccion) //constructor
        {
            this.Nombre = Nombre;
            this.Direccion = Direccion;
            this.Telefono = Telefono;
            this.DatosReferenciaDireccion = DatosReferenciaDireccion;
        }
    }
}