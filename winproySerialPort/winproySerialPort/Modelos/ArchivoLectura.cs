using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;

namespace winproySerialPort
{
    class ArchivoLectura : Archivo
    {
        public BinaryReader Action { get; set; }
        public long Tamano { get; set; }
        public int TamanoTrama { get; set; }

        public ArchivoLectura(string path, int tamanoTrama = 1024) : base(path, FileType.ReaderFile)
        {
            TamanoTrama = tamanoTrama;
            InicializarFlujo();
        }

        private void InicializarFlujo()
        {
            FlujoArchivo = new FileStream(Path, FileMode.Open, FileAccess.Read);
            Tamano = FlujoArchivo.Length;
            Action = new BinaryReader(FlujoArchivo);
        }

        public void Leer(byte[] Arreglo)
        {
            if (Activo)
            {
                Action.Read(Arreglo, 0, 1024);
            }
            else
            {
                Action.Close();
                FlujoArchivo.Close();
            }
        }
    }
}
