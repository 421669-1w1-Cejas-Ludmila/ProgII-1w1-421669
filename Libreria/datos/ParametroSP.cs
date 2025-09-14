using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Dominio
{
    public class ParametroSP
    {
        public string Name { get; set; }
        public object Valor { get; set; }

        public ParametroSP() { }
        public ParametroSP(string name, object valor)
        {
            this.Name = name;
            this.Valor = valor;
        }
    }
}
