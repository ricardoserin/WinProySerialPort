using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace winproySerialPort
{
    class ArchivoEscritura : Archivo
    {
        public BinaryWriter Action { get; set; }
        public long Avance { get; set; }
        public int TamanoTrama { get; set; }

        public ArchivoEscritura(string path, int tamanoTrama = 1024) : base(path, FileType.WriterFile)
        {
            TamanoTrama = tamanoTrama;
            InicializarFlujo();
            Avance = 0;
        }

        private void InicializarFlujo()
        {
            FlujoArchivo = new FileStream(Path, FileMode.Create, FileAccess.Write);
            Action = new BinaryWriter(FlujoArchivo);
        }

        public void Escribir(byte[] Arreglo, int tamanoTrama = -1)
        {
            if (Activo)
            {
                int trama = (tamanoTrama <= 0) ? TamanoTrama : tamanoTrama;
                Action.Write(Arreglo, 0, trama);
            } else
            {
                Action.Close();
                FlujoArchivo.Close();
            }
        }
    }
}
