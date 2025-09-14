using Libreria.Dominio;
using System.Data;
using System.Data.SqlClient;

namespace Libreria.Datos
{
    public class FacturaRepository : IGenericRepository<Factura>
    {
        private readonly string _connectionString;

        public FacturaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }


        public Factura? GetById(int id)
        {
            List<ParametroSP> param = new List<ParametroSP>()
            {
                new ParametroSP()
                {
                    Name = "@codigo",
                    Valor = id
                }
            };

            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_PRODUCTO_POR_CODIGO", param);

            if (dt != null && dt.Rows.Count > 0)
            {
                Factura p = new Factura()
                {
                    NroFactura = (int)dt.Rows[0]["nro_factura"],
                    Fecha = (DateTime)dt.Rows[0]["fecha"],
                    FormaPago = (string)dt.Rows[0]["FormaPago"],
                    Cliente = (string)dt.Rows[0]["Cliente"],
                    Detalles = new List<DetalleFactura>()

                };

                return p;
            }

            return null;
        }
     

        public List<Factura> GetAll()
        {
            List<Factura> facturas = new List<Factura>();
            var facturasDict = new Dictionary<int, Factura>();

            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_OBTENER_FACTURAS");

            foreach (DataRow row in dt.Rows)
            {
                int nroFactura = Convert.ToInt32(row["nro_factura"]);

                if (!facturasDict.ContainsKey(nroFactura))
                {
                    Factura f = new Factura
                    {
                        NroFactura = nroFactura,
                        Fecha = Convert.ToDateTime(row["fecha"]),
                        Cliente = row["Cliente"].ToString(),
                        FormaPago = row["FormaPago"].ToString(),
                        Detalles = new List<DetalleFactura>()
                    };

                    facturasDict[nroFactura] = f;
                    facturas.Add(f);
                }

                DetalleFactura df = new DetalleFactura
                {
                    IdFactura = nroFactura,
                    nombreArticulo = row["Articulo"].ToString(),
                    Cantidad = Convert.ToInt32(row["cantidad"]),
                    PrecioUnitario = Convert.ToDecimal(row["precio_unitario"])
                };

                facturasDict[nroFactura].Detalles.Add(df);
            }

            return facturas;
        }

        public bool Save(Factura factura)
        {
            if (factura == null)
                throw new ArgumentNullException(nameof(factura), "La factura no puede ser null");

            using (SqlConnection conexion = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("SP_INSERTAR_FACTURA", conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_cliente", factura.IdCliente);
                cmd.Parameters.AddWithValue("@id_forma_pago", factura.FormaPago);
                cmd.Parameters.AddWithValue("@fecha", factura.Fecha);

                DataTable dtDetalles = new DataTable();
                dtDetalles.Columns.Add("id_articulo", typeof(int));
                dtDetalles.Columns.Add("cantidad", typeof(int));

                foreach (var d in factura.Detalles)
                {
                    dtDetalles.Rows.Add(d.idArticulo, d.Cantidad);
                }

                SqlParameter paramDetalles = cmd.Parameters.AddWithValue("@DetallesDetalleFacturas", dtDetalles);
                paramDetalles.SqlDbType = SqlDbType.Structured;
                paramDetalles.TypeName = "dbo.DetalleFacturaType";

                conexion.Open();

                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    factura.NroFactura = Convert.ToInt32(result);
                    return true;
                }

                return false;
            }
        }
    }
}
