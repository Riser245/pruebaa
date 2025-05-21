using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;

class InventarioApp
{
 

    struct Articulo
    {
        public string Nombre;
        public double Cantidad;
        public char Estado;

        public override string ToString()
        {
            return $"{Nombre}*{Cantidad:F2}*{Estado}";
        }
    }

    static void Main()
    {
        List<Articulo> articulos = new List<Articulo>();

        // Leer datos previos si el archivo existe
        if (File.Exists(archivo))
        {
            string[] lineas = File.ReadAllLines(archivo);
            foreach (string linea in lineas)
            {
                string[] partes = linea.Split('*');
                if (partes.Length == 3)
                {
                    articulos.Add(new Articulo
                    {
                        Nombre = partes[0],
                        Cantidad = double.Parse(partes[1], CultureInfo.InvariantCulture),
                        Estado = char.Parse(partes[2])
                    });
                }
            }
        }

        bool agregarMas = true;
        while (agregarMas)
        {
            Console.Write("Ingrese el nombre del artículo: ");
            string nombre = Console.ReadLine().Trim();

            Console.Write("Ingrese la cantidad (2 decimales): ");
            double cantidad = double.Parse(Console.ReadLine());

            Articulo nuevo = new Articulo
            {
                Nombre = nombre,
                Cantidad = Math.Round(cantidad, 2),
                Estado = 'P'
            };
            articulos.Add(nuevo);

            Console.Write("¿Desea agregar más artículos? (S/N): ");
            agregarMas = Console.ReadLine().Trim().ToUpper() == "S";
        }

        // Guardar en el archivo
        GuardarArchivo(articulos);

        Console.Write("¿Desea inventariar un producto? (S/N): ");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            MostrarArchivo(articulos);

            Console.Write("Ingrese el nombre del producto a inventariar: ");
            string nombreBuscar = Console.ReadLine().Trim().ToUpper();

            bool encontrado = false;
            for (int i = 0; i < articulos.Count; i++)
            {
                if (articulos[i].Nombre.ToUpper() == nombreBuscar && articulos[i].Estado == 'P')
                {
                    articulos[i] = new Articulo
                    {
                        Nombre = articulos[i].Nombre,
                        Cantidad = articulos[i].Cantidad,
                        Estado = 'E'
                    };
                    encontrado = true;
                    break;
                }
            }

            if (encontrado)
            {
                Console.WriteLine("Producto inventariado correctamente.");
            }
            else
            {
                Console.WriteLine("Producto no encontrado o ya está inventariado.");
            }

            GuardarArchivo(articulos);
            MostrarArchivo(articulos);
        }
    }
    static string archivo = "inventario.txt";

    static void GuardarArchivo(List<Articulo> articulos)
    {
        using (StreamWriter writer = new StreamWriter(archivo))
        {
            foreach (var art in articulos)
            {
                writer.WriteLine(art.ToString());
            }
        }
    }

    static void MostrarArchivo(List<Articulo> articulos)
    {
        Console.WriteLine("\nInventario actual:");
        Console.WriteLine("Articulo*Cantidad*Estado");
        foreach (var art in articulos)
        {
            Console.WriteLine(art.ToString());
        }
        Console.WriteLine();
    }
}