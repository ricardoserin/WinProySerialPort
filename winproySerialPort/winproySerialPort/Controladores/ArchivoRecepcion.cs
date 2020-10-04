using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace winproySerialPort
{
    class ArchivoRecepcion
    {
        public Archivo NuevoArchivo { get; set; }
        public BinaryWriter EscritorBinario { get; set; }
        public long Avance { get; set; }
        public int TamanoTrama { get; set; }
        public FileStream FlujoArchivo { get; set; }

        public ArchivoRecepcion(string path, int tamanoTrama = 1024)
        {
            NuevoArchivo = new Archivo(path);
            TamanoTrama = tamanoTrama;
            InicializarFlujo();
            Avance = 0;
        }

        private void InicializarFlujo()
        {
            FlujoArchivo = new FileStream(NuevoArchivo.Path, FileMode.Create, FileAccess.Write);
            EscritorBinario = new BinaryWriter(FlujoArchivo);
        }

        public void Escribir(byte[] Arreglo, int tamanoTrama = -1)
        {
            if (NuevoArchivo.Activo)
            {
                int trama = (tamanoTrama <= 0) ? TamanoTrama : tamanoTrama;
                EscritorBinario.Write(Arreglo, 0, trama);
            } else
            {
                EscritorBinario.Close();
                FlujoArchivo.Close();
            }
        }
        public void Cerrar()
        {
            EscritorBinario.Close();
            FlujoArchivo.Close();
        }
    }
}
