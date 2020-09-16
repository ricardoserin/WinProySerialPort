using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Configuration;

namespace winproySerialPort
{
    static class Puerto
    {
        public static SerialPort puerto { get; private set; }
        public static string NombrePuerto { get; set; }
        public static bool BufferDeSalidaVacio { 
            get
            {
                return (puerto.BytesToWrite == 0);
            }
        }
        public static int BytesPorSalir
        {
            get
            {
                return (BufferDeSalidaVacio) ? 0 : puerto.BytesToWrite;
            }
        }
        public static int TamanoDeTrama 
        { 
            get
            {
                return puerto.ReceivedBytesThreshold;
            }
            set
            {
                puerto.ReceivedBytesThreshold = value;
            }
        }
        public static event SerialDataReceivedEventHandler DataRecibida
        {
            add
            {
                puerto.DataReceived += new SerialDataReceivedEventHandler(value);
            }
            remove
            {
                puerto.DataReceived -= value;
            }
        }
        public static bool Inicializar(string nombrePuerto, int baudios = 57600, int tamanoDeTrama = 0)
        {
            if (puerto != null)
            {
                return true;
            } else
            {
                try
                {
                    NombrePuerto = nombrePuerto;
                    puerto = new SerialPort(NombrePuerto, baudios, Parity.Even, 8, StopBits.Two);
                    TamanoDeTrama = (tamanoDeTrama > 0) ? tamanoDeTrama : int.Parse(ConfigurationManager.AppSettings["tamanoDeTrama"]);
                    puerto.Open();
                    return true;
                } catch
                {
                    return false;
                }
            }
        }

        public static void Escribir(string mensaje)
        {
            puerto.Write(mensaje);
        }

        public static void Escribir(Trama trama)
        {
            puerto.Write(trama.Contenido, 0, trama.TamanoDeTrama);
        }
        /// <summary>
        /// Lee los datos en el buffer de entrada y los coloca en la trama
        /// </summary>
        /// <param name="trama">Trama en la que se colocarán los datos, esta debe tener un tamaño establecido</param>
        /// <param name="tamanoTrama">Tamaño de la trama, sobreescribe el tamaño de la trama.
        /// Si no se entrega y la trama no tiene un tamaño fijo se asigna por defecto 1024</param>
        public static void Leer(Trama trama, int tamanoTrama = 0)
        {
            // return puerto.ReadExisting();
            // Si se entrega por parámetro un tamaño de trama, se sobreescribe el tamaño de trama brindada
            if (tamanoTrama > 0) trama.TamanoDeTrama = tamanoTrama;
            // Si no se entrega trama, y la trama no tiene un tamaño asignado, se crea asigna por defecto un tamaño de 1024
            else if (trama.TamanoDeTrama <= 0) trama.TamanoDeTrama = 1024;
            if (puerto.BytesToRead >= trama.TamanoDeTrama)
                puerto.Read(trama.Contenido, 0, trama.TamanoDeTrama); // Se escribe el buffer en el contenido de la trama            }
        }
    }
}
