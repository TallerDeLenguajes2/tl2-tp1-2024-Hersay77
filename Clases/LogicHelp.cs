namespace EspacioLogicHelp
{
    class LogicHelp
    {
        public static bool Existe(string ruta) //comrpueba existencia de archivo
        {
            if (File.Exists(ruta))
            {
                return true;
            }   
            else
            {
                return false;
            }
        }

        public static bool ControlMenuP(int opcion) //control entrada correcta
        {
            if (opcion >=1 && opcion <=5 )
            {
                return true;
            }
            else
            {
                Console.WriteLine("Entrada Incorrecta. Vuelva a Ingresar: ");
                return false;
            }
        }
    }
}