namespace EspacioHelperLogic
{
    class HelperLogic
    {
        public static bool Existe(string ruta) //comrpueba existencia de archivo
        {
            return File.Exists(ruta);
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