using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winproySerialPort
{
    class classArchivoEnviando
    {
        public int Num { get; set; }
        public string Nombre { get; set; }
        public string Ruta { get; set; }
        public long Tamaño { get; set; }
        public long Avance { get; set; }
        public Boolean Activo { get; set; }
        //podriamos mantener a los Streams dentro de la clase, pero también podríamos hacerlo fuera

    }
}
