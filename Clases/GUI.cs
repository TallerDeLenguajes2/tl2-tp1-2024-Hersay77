namespace EspacioGUI
{
    public class GUI
    {
        public static void Menu()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            
            Console.WriteLine(
                    @"
                    ╔═════════════════════════════════════════════════════╗
                    ║           G E S T I O N  D E  P E D I D O S         ║
                    ╠═════════════════════════════════════════════════════╣
                    ║ 1. NUEVO PEDIDO                                     ║
                    ║ 2. ASIGNAR PEDIDO A CADETE                          ║
                    ║ 3. CAMBIAR ESTADO DE PEDIDO                         ║
                    ║ 4. REASIGNAR PEDIDO A CADETE                        ║
                    ╚═════════════════════════════════════════════════════╝
                    "
            );
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("INGRESE UNA OPCION: ");
            Console.ResetColor();
        }
    }
}