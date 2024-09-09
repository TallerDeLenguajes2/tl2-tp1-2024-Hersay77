# TRABAJO PRACTICO 1
## Sanchez Luis Hernan

### ● ¿Cuál de estas relaciones considera que se realiza por composición y cuál por agregación?
    Pedido/Cliente  - Composicion - El cliente forma parte de un pedido por lo que depende de la existencia de este
    Pedido/Cadete - Agregacion - El pedido puede reasignarse a otro Cadete por lo que es indepediente del mismo
    Cadete/Cadeteria - Agregacion - La lista de Cadeterias sigue existiendo sin uno o mas de los cadetes, por lo que es independiente
### ● ¿Qué métodos considera que debería tener la clase Cadetería y la clase Cadete?
    Cadeteria: 
        metodo constructor de Cadeteria
        metodos
            AltaDePedido
            AsignarPedido
            CambiarEstado
            ReasingarPedido
            MostrarInforme

    Cadete: 
        metodo Jornal a Cobrar
        metodo Constructor de Cadete
        metodo mostrar datos del Cadete

### ● Teniendo en cuenta los principios de abstracción y ocultamiento, que atributos, propiedades y métodos deberían ser públicos y cuáles privados.
    Todos los atributos son privadso. Y todos de solo lectura a excepcion del estado de un pedido que sera cambiado desde fuera de la clase

### ● ¿Cómo diseñaría los constructores de cada una de las clases?
    en el caso de cadeteria, cadete, Cliente uso un constructor que recibe cada dato y asigna sus valores a los campos de los mismos por ejemplo: 
    public Cliente(string nombre, string direccion, long telefono, string datosReferenciaDireccion) //constructor
        {
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            DatosReferenciaDireccion = datosReferenciaDireccion;
        }

    En el caso de Pedido hice un constructor mas elaborado ya que este a su vez llama a los metodos de construir cliente

### ● ¿Se le ocurre otra forma que podría haberse realizado el diseño de clases?
    Pondria la clase cliente como Agregacion pára poder tener un listado aparte de clientes nuevos y clientes habituales 
