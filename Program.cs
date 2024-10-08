﻿using EspacioCadeteria;
using EspacioCadete;
using EspacioGUI;
using EspacioAccesoADatos;
using EspacioPedido;

string ArchivoCadetes = "CSV/Cadetes.csv";
string ArchivoCadeteria = "CSV/Cadeteria.csv";

if (AccesoADatos.Existe(ArchivoCadetes) && AccesoADatos.Existe(ArchivoCadeteria))
{
    List<Cadete> ListaCadetes = AccesoADatos.CargarCSVCadetes(ArchivoCadetes); //Cargo Lista Cadetes
    Cadeteria cadeteria = AccesoADatos.CargarCSVCadeteria(ArchivoCadeteria, ListaCadetes); //Cargo datos de Cadeteria en una instancia cadeteria
    List<Pedido> ListaGeneralPedidos = new List<Pedido>(); //Creo lista general de pedidos vacia

    string entrada;
    int opcion, nro = 1;
    do
    {

        GUI.Menu(cadeteria);
        entrada = Console.ReadLine();
        int.TryParse(entrada, out opcion);

        switch (opcion)
        {
            case 1:
                Pedido NuevoPedido = Cadeteria.AltaPedido(nro);
                ListaGeneralPedidos.Add(NuevoPedido); //agrego pedido a la lista general
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("PEDIDO AGREGADO A LA LISTA GENERAL DE PEDIDOS");
                Console.ForegroundColor = ConsoleColor.White;
                nro++; //aumento numero de pedido
                break;
            case 2:
                if (ListaGeneralPedidos.Count != 0)
                {
                    Cadeteria.AsignarPedido(ListaGeneralPedidos, ListaCadetes);
                }
                else
                {
                    Console.WriteLine("NO HAY PEDIDOS PARA ASIGNAR");
                }
                break;
            case 3:
                Cadeteria.CambiarEstado(ListaCadetes);
                break;
            case 4:
                Cadeteria.ReasignarPedido(ListaGeneralPedidos, ListaCadetes);
                break;
            case 5:
                Console.WriteLine("FINAL DE JORNADA - MOSTRANDO INFORME");
                Cadeteria.MostrarInforme(ListaCadetes);
                Console.WriteLine("SALIENDO...");
                break;
            default:
                Console.Clear();
                Console.WriteLine("OPCION NO VALIDA - VUELVA A INGRESAR");
                break;
        }

    } while (opcion !=5);
}
else
{
    Console.WriteLine("Error no se puede inciar el programa falta: Cadetes.csv o Cadeteria.csv");
}

