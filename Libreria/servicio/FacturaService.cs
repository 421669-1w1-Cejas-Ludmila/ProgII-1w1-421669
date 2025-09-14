using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Libreria.Datos;
using Libreria.Dominio;

namespace Libreria.Servicio
{
    public class FacturaService
    {
        private readonly FacturaRepository _repo;
        private readonly string _connectionString;

        public FacturaService(string connectionString)
        {
            _connectionString = connectionString;
            _repo = new FacturaRepository(connectionString);
        }

        public List<Factura> ObtenerFacturas()
        {
            return _repo.GetAll();
        }

        public Factura ObtenerFacturaPorId(int id)
        {
            return _repo.GetById(id);
        }

        public bool CrearFactura(Factura factura )
        {
            return _repo.Save(factura);
        }
    }
}
