using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsoDocs.Datos
{
    public class Asociados_Datos
    {
        int id = 0;
        string nombre, Direccion, Telefono, Correo;
        long cedula;
        DateTime nacimiento, ingreso;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion1 { get => Direccion; set => Direccion = value; }
        public string Telefono1 { get => Telefono; set => Telefono = value; }
        public string Correo1 { get => Correo; set => Correo = value; }
        public long Cedula { get => cedula; set => cedula = value; }
        public DateTime Nacimiento { get => nacimiento; set => nacimiento = value; }
        public DateTime Ingreso { get => ingreso; set => ingreso = value; }
    }
}
