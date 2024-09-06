using EspacioCadeteria;
using EspacioCadete;

namespace EspacioInforme
{
    public class Informe
    {
        public static void MostrarInforme(List<Cadete> ListaCadetes)
        {
            float totalEnvios = 0;
            Console.WriteLine("Informacion Cadetes:");
            foreach (var cadete in ListaCadetes)
            {
                int CantidadDePedidosCompletados = 0;
                foreach (var pedido in cadete.ListaPedidos)
                {
                    if (pedido.Estado == 1)
                    {
                        CantidadDePedidosCompletados++;
                    }
                }
                
                Console.WriteLine($"Nombre: {cadete.Nombre} || Pedidos completdaoos por el cadete: {CantidadDePedidosCompletados} || Total ganado: ${cadete.JornalACobrar(cadete)}");
                totalEnvios = totalEnvios + CantidadDePedidosCompletados;
            }

            //var cantidad = cadete.pedidos.Count(t => t.Estado == 1);

            float promedioEnviosPorCadete = totalEnvios/(float)ListaCadetes.Count;
            Console.WriteLine("\nINFORME GENERAL:");
            Console.WriteLine($"Total de envios: {totalEnvios}");
            Console.WriteLine($"Promedio de envios por cadete: {promedioEnviosPorCadete}");
        }
    }
}