using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;

namespace winproySerialPort
{
    class Archivo
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Nombre { get; set; }
        public string Directorio { get; set; }
        public string Extension { get; set; }
        public bool Activo { get; private set; }
        

        public Archivo(string path)
        {
            // Se asigna el path y el tipo de archivo a sus atributos correspondientes
            Path = path;

            // Se asignan los demás atributos del archivo en función del path ingresado
            InicializarMetadatos();

            // El archivo está activo
            ActivarArchivo();
        }

        private void InicializarMetadatos()
        {
            var fullName = Path.Substring(Path.LastIndexOf('\\') + 1);

            Extension = fullName.Substring(fullName.LastIndexOf('.') + 1);
            Nombre = fullName.Substring(0, fullName.Length - Extension.Length - 1);
            Directorio = Path.Substring(0, Path.Length - fullName.Length);
        }
    
        // Funciones para activar o desactivar el archivo
        public void DesactivarArchivo()
        {
            Activo = false;
        }
        public void ActivarArchivo()
        {
            Activo = true;
        }
        public void ToggleActivarArchivo()
        {
            Activo = !Activo;
        }
        //podriamos mantener a los Streams dentro de la clase, pero también podríamos hacerlo fuera
    }
}
