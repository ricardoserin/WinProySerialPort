using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;

namespace winproySerialPort
{
    class ArchivoEnvio
    {
        public Archivo ArchivoLeido { get; set; }
        public BinaryReader Lector { get; set; }
        public long Tamano { get; set; }
        public FileStream Flujo { get; set; }


        public ArchivoEnvio(string path)
        {
            ArchivoLeido = new Archivo(path);
            InicializarFlujo();
        }

        private void InicializarFlujo()
        {
            try
            {
                Flujo = new FileStream(ArchivoLeido.Path, FileMode.Open, FileAccess.Read);
                Tamano = Flujo.Length;
                Lector = new BinaryReader(Flujo);
            }
            catch (ArgumentException aex)
            {

            }
            
        }

        public void Leer(byte[] Arreglo, int bytesALeer = 1024)
        {
            if (ArchivoLeido.Activo)
            {
                Lector.Read(Arreglo, 0, bytesALeer);
            }
            else
            {
                Lector.Close();
                Flujo.Close();
            }
        }

        public byte[] Leer(int bytesALeer = 1024)
        {
            if (ArchivoLeido.Activo)
            {
                return Lector.ReadBytes(bytesALeer);
            }
            Lector.Close();
            Flujo.Close();
            return null;
        }

        public TramasEnvio Disassemble(string emisor, int idEnvio)
        {
            var tramas = new TramasEnvio(this, idEnvio, emisor);
            return tramas;
        }
    }
}
