using System.Collections.Generic;
using System.Xml.Linq;
using Libreria.Dominio;

namespace Libreria.Datos
{
    public interface IGenericRepository<T> where T : class
    {

        List<T> GetAll();
        T GetById(int id);

        bool Save(Factura factura);
    }
}
