using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using winproySerialPort.Modelos;

namespace winproySerialPort
{
    class TramasEnvio
    {
        public ETipoTrama TipoDatos { get; set; }
        public int IdEnvio { get; set; }
        public int IndiceTrama { get; set; }
        public bool Enviado { get; set; }
        public string DisplayMessage { get; set; }
        public string NombreEmisor { get; set; }

        private readonly int NumeroDeTramas;
        private readonly Trama[] Tramas;
        private readonly int TamanoTrama;
        private readonly int TamanoCabecera;
        private readonly int TamanoDatos;
        public TramasEnvio(ArchivoEnvio archivoEnvio, int idEnvio, string nombreEmisor = "", int tamanoTrama = 1024, int tamanoCabecera = 5)
        {
            TamanoTrama = tamanoTrama;
            TamanoCabecera = tamanoCabecera;
            TamanoDatos = TamanoTrama - TamanoCabecera;
            DisplayMessage = archivoEnvio.ArchivoLeido.Nombre;
            TipoDatos = ETipoTrama.Archivo;
            NombreEmisor = nombreEmisor;
            IdEnvio = idEnvio;

            var indice = 1;

            var tamano = archivoEnvio.Tamano;
            NumeroDeTramas = (int) Math.Ceiling((double) tamano / tamanoTrama);
            Tramas = new Trama[NumeroDeTramas + 1];

            var metadatos = new string[] {
                NombreEmisor,
                tamano.ToString(),
                archivoEnvio.ArchivoLeido.Nombre,
                archivoEnvio.ArchivoLeido.Extension
            };

            Tramas[0] = GenerarTramaCabecera(TipoDatos, metadatos);

            while (indice < NumeroDeTramas)
            {
                var bytesLeidos = archivoEnvio.Lector.ReadBytes(TamanoDatos);
                Tramas[indice] = GenerarTrama(bytesLeidos);
                indice++;
            }
        }

        public TramasEnvio(Mensaje mensaje, int idEnvio, string nombreEmisor, int tamanoTrama = 1024, int tamanoCabecera = 5)
        {
            TamanoTrama = tamanoTrama;
            TamanoCabecera = tamanoCabecera;
            TamanoDatos = TamanoTrama - TamanoCabecera;
            DisplayMessage = mensaje.Texto;
            TipoDatos = ETipoTrama.Mensaje;
            NombreEmisor = nombreEmisor;
            IdEnvio = idEnvio;

            var indice = 1;

            var contenido = mensaje.Contenido;
            var tamano = contenido.Length;
            NumeroDeTramas = (int)Math.Ceiling((double)tamano / tamanoTrama);
            Tramas = new Trama[NumeroDeTramas + 1];
            var metadatos = new string[] {
                NombreEmisor,
                tamano.ToString()
            };
            Tramas[0] = GenerarTramaCabecera(TipoDatos, metadatos);

            while (indice < NumeroDeTramas)
            {
                var bytesLeidos = contenido;
                Tramas[indice] = GenerarTrama(bytesLeidos);
                indice++;
            }
        }

        /// <summary>
        /// Lee la siguiente trama y la devuelve, si llegó al final de la trama cambia el estado
        /// del objeto y envia la última trama. Si ya ha terminado de enviar vuelve a enviar la
        /// última trama
        /// </summary>
        /// <returns></returns>
        public Trama SiguienteTrama()
        {
            if (!Enviado)
            {
                IndiceTrama++;
                if (IndiceTrama == NumeroDeTramas) Enviado = true;
            }
            return Tramas[IndiceTrama - 1];
        }

        public double Progreso()
        {
            double progreso = (double)IndiceTrama / NumeroDeTramas;
            return progreso;
        }
        private byte[] GenerarCabeceraInformacion()
        {
            var headerPrefix = (TipoDatos == ETipoTrama.Archivo)
                ? "AI"
                : "MI";
            var stringHeader = headerPrefix + IndiceTrama.ToString("D3");
            var header = Encoding.UTF8.GetBytes(stringHeader);
            return header;
        }
        private Trama GenerarTrama(byte[] bytesLeidos)
        {
            var header = GenerarCabeceraInformacion();
            var bytes = header.Concat(bytesLeidos).ToArray();
            if (bytesLeidos.Length < 1019)
            {
                var tamanoRelleno = 1019 - bytesLeidos.Length;
                var relleno = new byte[tamanoRelleno];
                for (short i = 0; i < tamanoRelleno; i++)
                {
                    relleno[i] = 64;
                }
                bytes = bytes.Concat(relleno).ToArray();
            }
            var trama = new Trama(bytes);
            return trama;
        }

        private Trama GenerarTramaCabecera(ETipoTrama tipo, string[] metadatos)
        {
            var cabecera = (tipo == ETipoTrama.Archivo)
                ? "AC" + IdEnvio.ToString("D3")
                : "MC" + IdEnvio.ToString("D3");
            var cuerpo = metadatos.Aggregate("", (acum, dato) => acum + dato + ";");
            var bytes = Encoding.UTF8.GetBytes(cabecera).Concat(Encoding.UTF8.GetBytes(cuerpo)).ToArray();
            var trama = new Trama(bytes);
            return trama;
        }
    }
}
