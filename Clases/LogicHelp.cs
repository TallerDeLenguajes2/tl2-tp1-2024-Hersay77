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
    }
}