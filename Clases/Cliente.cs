namespace EspacioCliente
{
    public class Cliente
    {
        public string Nombre { get;}
        public string Direccion { get;}
        public long Telefono { get;}
        public string DatosReferenciaDireccion { get;}

        public Cliente(string Nombre, string Direccion, long Telefono, string DatosReferenciaDireccion) //constructor
        {
            this.Nombre = Nombre;
            this.Direccion = Direccion;
            this.Telefono = Telefono;
            this.DatosReferenciaDireccion = DatosReferenciaDireccion;
        }
    }
}