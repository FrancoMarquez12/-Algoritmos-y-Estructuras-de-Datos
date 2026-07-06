using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace TiendaLoona
{
    // 1. Definición de la estructura adaptada
    public struct Album
    {
        public string Nombre;
        public int Cantidad;
        public double Precio;

        public Album(string nombre, int cantidad, double precio)
        {
            Nombre = nombre;
            Cantidad = cantidad;
            Precio = precio;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Lista global para almacenar los elementos
            List<Album> inventario = new List<Album>();
            bool continuar = true;

            while (continuar)
            {
                Console.WriteLine("\n--- SISTEMA DE GESTIÓN DE ÁLBUMES (REDEBUTS LOONA) ---");
                Console.WriteLine("1. Registrar nuevo álbum");
                Console.WriteLine("2. Mostrar todos los álbumes");
                Console.WriteLine("3. Calcular precio promedio");
                Console.WriteLine("4. Buscar álbum por nombre");
                Console.WriteLine("5. Mostrar álbumes que superan el precio promedio");
                Console.WriteLine("6. Mostrar álbum con precio máximo y mínimo");
                Console.WriteLine("7. Eliminar álbum por nombre");
                Console.WriteLine("8. Salir");
                Console.Write("Seleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out int opcion))
                {
                    Console.WriteLine("Por favor, ingrese un número válido.");
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        string Nombre;
                        int Cantidad = 0;
                        double Precio = 0;

                        Console.WriteLine("\n[1. REGISTRAR NUEVO ALBUM]");
                        Console.WriteLine("Ingrese el nombre de ese album.");
                        Nombre = Console.ReadLine();
                        Console.WriteLine("Ingrese la cantidad de copias de ese album.");
                        int.TryParse(Console.ReadLine(), out Cantidad);
                        Console.WriteLine("Ingrese el precio de ese album.");
                        double.TryParse(Console.ReadLine(), out Precio);

                        Album nuevoAlbum = new Album(Nombre, Cantidad, Precio);
                        inventario.Add(nuevoAlbum);
                        break;

                    case 2:
                        Console.WriteLine("\n[2. INVENTARIO COMPLETO]");
                        if (inventario.Count == 0)
                        {
                            Console.WriteLine("No hay albumes para ver.");
                        }
                        else
                        {
                            foreach (Album album in inventario)
                            {
                                Console.WriteLine($"{album.Nombre} | {album.Cantidad} | {album.Precio}");
                            }
                        }
                        break;

                    case 3:
                        Console.WriteLine("\n[3. PRECIO PROMEDIO GENERAL]");
                        if (inventario.Count == 0)
                        {
                            Console.WriteLine("No hay albumes registrados de los que calcular un promedio");
                        }
                        else
                        {
                            double sumaPrecios = 0;
                            foreach (Album album in inventario)
                            {
                                sumaPrecios += album.Precio;
                            }

                            double promedio = sumaPrecios / inventario.Count;
                            Console.WriteLine($"El promedio de los albumes es {promedio:F2}");
                        }
                        break;

                    case 4:
                        Console.WriteLine("\n[4. BUSCAR ALBUM POR NOMBRE]");
                        if (inventario.Count == 0)
                        {
                            Console.WriteLine("El inventario esta vacio.");
                        }
                        else
                        {
                            Console.Write("Ingrese el nombre del album a buscar: ");
                            string busqueda = Console.ReadLine();
                            bool encontrado = false;

                            foreach (Album album in inventario)
                            {
                                if (album.Nombre.Contains(busqueda, StringComparison.OrdinalIgnoreCase))
                                {
                                    Console.WriteLine($"- Encontrado: {album.Nombre} | Stock: {album.Cantidad} | Precio: {album.Precio}");
                                    encontrado = true;
                                }
                            }

                            if (!encontrado)
                            {
                                Console.WriteLine("No se encontro ningun album que coincida.");
                            }
                        }
                        break;

                    case 5:
                        Console.WriteLine("\n[5. ALBUMES QUE SUPERAN EL PROMEDIO]");
                        if (inventario.Count == 0)
                        {
                            Console.WriteLine("El inventario esta vacio.");
                        }
                        else
                        {
                            double sumaFiltrar = 0;
                            foreach (Album album in inventario)
                            {
                                sumaFiltrar += album.Precio;
                            }
                            double promActual = sumaFiltrar / inventario.Count;
                            Console.WriteLine($"El precio promedio actual es: {promActual:F2}\n");

                            bool haySuperiores = false;
                            foreach (Album album in inventario)
                            {
                                if (album.Precio > promActual)
                                {
                                    Console.WriteLine($"{album.Nombre} | Precio: {album.Precio} (Supera el promedio)");
                                    haySuperiores = true;
                                }
                            }

                            if (!haySuperiores)
                            {
                                Console.WriteLine("Ningun album supera el precio promedio.");
                            }
                        }
                        break;

                    case 6:
                        Console.WriteLine("\n[6. ALBUM MAS CARO Y MAS BARATO]");
                        if (inventario.Count == 0)
                        {
                            Console.WriteLine("El inventario esta vacio.");
                        }
                        else
                        {
                            Album masCaro = inventario[0];
                            Album masBarato = inventario[0];

                            for (int i = 1; i < inventario.Count; i++)
                            {
                                if (inventario[i].Precio > masCaro.Precio) masCaro = inventario[i];
                                if (inventario[i].Precio < masBarato.Precio) masBarato = inventario[i];
                            }

                            Console.WriteLine($"Album mas caro: {masCaro.Nombre} - Precio: {masCaro.Precio}");
                            Console.WriteLine($"Album mas barato: {masBarato.Nombre} - Precio: {masBarato.Precio}");
                        }
                        break;

                    case 7:
                        Console.WriteLine("\n[7. ELIMINAR ALBUM DEL REGISTRO]");
                        if (inventario.Count == 0)
                        {
                            Console.WriteLine("El inventario esta vacio.");
                        }
                        else
                        {
                            Console.Write("Ingrese el nombre exacto del album a eliminar: ");
                            string nombreEliminar = Console.ReadLine();

                            int indiceAEliminar = inventario.FindIndex(a => a.Nombre.Equals(nombreEliminar, StringComparison.OrdinalIgnoreCase));

                            if (indiceAEliminar != -1)
                            {
                                inventario.RemoveAt(indiceAEliminar);
                                Console.WriteLine($"El album '{nombreEliminar}' ha sido eliminado exitosamente.");
                            }
                            else
                            {
                                Console.WriteLine("No se encontro ningun album con ese nombre exacto.");
                            }
                        }
                        break;

                    case 8:
                        continuar = false;
                        break;

                    default:
                        Console.WriteLine("Opcion invalida. Intente de nuevo.");
                        break;
                }
            }
        }
    }
}