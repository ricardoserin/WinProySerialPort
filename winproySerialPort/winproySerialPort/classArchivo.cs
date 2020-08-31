using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winproySerialPort
{
    class classArchivo
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Nombre { get; set; }
        public string Directorio { get; set; }
        public string Extension { get; set; }
        public string Tipo { get; set; }
        public long Tamano { get; set; }
        public long Avance { get; set; }
        public Boolean Activo { get; set; }
        //podriamos mantener a los Streams dentro de la clase, pero también podríamos hacerlo fuera
    }
}
