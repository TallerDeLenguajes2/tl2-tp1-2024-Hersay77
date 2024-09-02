# TRABAJO PRACTICO 1
## Sanchez Luis Hernan

### ● ¿Cuál de estas relaciones considera que se realiza por composición y cuál por agregación?
    Pedido/Cliente  - Composicion - El cliente forma parte de un pedido por lo que depende de la existencia de este
    Pedido/Cadete - Agregacion - El pedido puede reasignarse a otro Cadete por lo que es indepediente del mismo
    Cadete/Cadeteria - Agregacion - La lista de Cadeterias sigue existiendo sin uno o mas de los cadetes, por lo que es independiente
### ● ¿Qué métodos considera que debería tener la clase Cadetería y la clase Cadete?
    Cadeteria: 
        metodo constructor de Cadeteria
        metodo que cargue el CVS con datos de la Cadeteria
    Cadete: 
        metodo Jornal a Cobrar
        metodo Constructor de Cadete
        metodo que cargue un CSV con datos de los Cadetes
        metodo mostrar datos del Cadete

### ● Teniendo en cuenta los principios de abstracción y ocultamiento, que atributos, propiedades y métodos deberían ser públicos y cuáles privados.
    Estableci como publicos todos aquellos metodos en las clases, los cuales trabajan con los atributos privados, los cuales son los campos qe son las caracteristicas de cada instancia

### ● ¿Cómo diseñaría los constructores de cada una de las clases?
    en el caso de cadeteria, cadete, Cliente uso un constructor que recibe cada dato y asigna sus valores a los campos de los mismos por ejemplo: 
    public Cliente(string nombre, string direccion, long telefono, string datosReferenciaDireccion) //constructor
        {
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            DatosReferenciaDireccion = datosReferenciaDireccion;
        }

    En el caso de Pedido hice un constructor mas elaborado ya que este a su vez llama a los meetodos de construir cliente

### ● ¿Se le ocurre otra forma que podría haberse realizado el diseño de clases?
    Podria ser que en la clase cadeteria dentro tenga un atributo lista de pedidos, lista de clientes y lista de cadetes
    y clases apartes cadete, clientes y pedidos ya que todas forman parte de la cadeteria
