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
        public string Texto { get; set; }
        
        public Mensaje(string texto)
        {
            Texto = texto;
            Contenido = Encoding.UTF8.GetBytes(texto);
            TamanoMensaje = Contenido.Length;
        }
        public TramasEnvio Disassemble()
        {
            var tramas = new TramasEnvio(this);
            return tramas;
        }
    }
}
