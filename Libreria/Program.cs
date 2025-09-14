using Libreria.Datos;
using Libreria.Dominio;
using Libreria.Servicio;
using System;
using System.Collections.Generic;

namespace Libreria
{

    internal class Program
    {
        static void Main(string[] args)
        {
            string connStr = @"Data Source=DESKTOP-IRDSJ6T\SQLEXPRESS;Initial Catalog=proyectolibreria;Integrated Security=True;TrustServerCertificate=True";
            FacturaService service = new FacturaService(connStr);

            Console.WriteLine("¿Desea crear una nueva factura? (Si/No)");
            string respuesta = Console.ReadLine().ToUpper();

            if (respuesta == "SI")
            {
                Console.WriteLine("=== Crear Factura ===");

                Console.Write("Ingrese Id Cliente: ");
                int idCliente = int.Parse(Console.ReadLine());

                Console.Write("Ingrese Forma de Pago: ");
                string formaPago = Console.ReadLine();

                Factura factura = new Factura
                {
                    IdCliente = idCliente,
                    FormaPago = formaPago,
                    Fecha = DateTime.Now,
                    Detalles = new List<DetalleFactura>()
                };

                Console.Write("¿Cuántos artículos desea cargar?: ");
                int cantidadArticulos = int.Parse(Console.ReadLine());

                for (int i = 0; i < cantidadArticulos; i++)
                {
                    Console.WriteLine($"\nArtículo {i + 1}:");
                    Console.Write("  Id Artículo: ");
                    int idArticulo = int.Parse(Console.ReadLine());

                    Console.Write("  Cantidad: ");
                    int cantidad = int.Parse(Console.ReadLine());

                    factura.Detalles.Add(new DetalleFactura
                    {
                        idArticulo = idArticulo,
                        Cantidad = cantidad
                    });
                }
             
                if (service.CrearFactura(factura))
                {
                    Console.WriteLine($"\nFactura creada con éxito. Nro generado: {factura.NroFactura}");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("\nError al crear la factura.");
                }
            }
            else
            {
                // Obtener todas las facturas - GetAll
                Console.WriteLine("Obtener todos las facturas - GetAll");              
                List<Factura> lp = service.ObtenerFacturas();
               
                if (lp.Count > 0)
                    foreach (Factura p in lp)
                    {
                        Console.WriteLine(p);
                        Console.WriteLine();
                    }
                else
                    Console.WriteLine("No hay productos");


                Console.WriteLine("\nObtener una factura por id - GetById");
                Factura factura1 = service.ObtenerFacturaPorId(1);

                if (factura1 != null)
                    Console.WriteLine(factura1);
                else
                    Console.WriteLine("No hay factura con ese id");

            }
        }
    }

}






