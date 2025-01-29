using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsoDocs.Datos
{
    public class Familiar_Datos
    {
        int id;
        string nombre, genero;
        DateTime nacimiento;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Genero { get => genero; set => genero = value; }
        public DateTime Nacimiento { get => nacimiento; set => nacimiento = value; }
    }
}
