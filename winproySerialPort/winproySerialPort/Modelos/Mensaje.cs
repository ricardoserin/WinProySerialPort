using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winproySerialPort
{
    class Mensaje
    {
        public byte[] Contenido { get; set; }
        public int TamanoMensaje { get; set; }

        public Mensaje(string contenido)
        {
            Contenido = Encoding.UTF8.GetBytes(contenido);
            TamanoMensaje = Contenido.Length;
        }
    }
}
