using EspacioCadeteria;

namespace EspacioGUI
{
    public class GUI
    {
        public static void Menu(Cadeteria cadeteria)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
           
            Console.WriteLine(
                    @$"
                    ╔═══════════════════════════════════════════════════════════════════════╗
                      CADETERIA: {cadeteria.Nombre} - TEL: {cadeteria.Telefono}             
                    ╠═══════════════════════════════════════════════════════════════════════╣
                    ║ 1. NUEVO PEDIDO                                                       ║
                    ║ 2. ASIGNAR CADETE A PEDIDO                                            ║
                    ║ 3. CAMBIAR ESTADO DE PEDIDO                                           ║
                    ║ 4. REASIGNAR CADETE A PEDIDO                                          ║
                    ║ 5. FINALIZAR JORNADA (MOSTRAR INFORME Y SALIR)                        ║
                    ╚═══════════════════════════════════════════════════════════════════════╝
                    "
            );
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("INGRESE UNA OPCION: ");
            Console.ResetColor();
        }

        public static void MenuAcceso()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(
                    @$"
                    ╔═══════════════════════════════════════════════════════════════════════╗
                    ║ MENU ACCESO: SELECCIONE CON QUE ARCHIVOS DESEA TRABAJAR               ║
                    ╠═══════════════════════════════════════════════════════════════════════╣
                    ║ 1. CSV                                                                ║
                    ║ 2. JSON                                                               ║
                    ╚═══════════════════════════════════════════════════════════════════════╝
                    "
            );
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("INGRESE UNA OPCION: ");
            Console.ResetColor();
        }
    }
}