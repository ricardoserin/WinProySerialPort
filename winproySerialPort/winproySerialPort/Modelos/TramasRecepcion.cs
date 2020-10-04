using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using winproySerialPort.Modelos;

namespace winproySerialPort
{
    class TramasRecepcion
    {
        public int IdRecepcion { get; set; }
        public ETipoTrama TipoDatos { get; set; }
        public string[] MetaDatos { get; set; }
        public int IndiceTrama { get; set; }
        public bool Completado { get; set; }
        public string DisplayMessage { get; set; }
        public string Emisor { get; set; }

        public delegate void HandleReception(object o, string mensaje);
        public event HandleReception inicioRecepcionMensaje;
        public event HandleReception inicioRecepcionArchivo;
        public event HandleReception InicioRecepcionMensaje
        {
            add
            {
                inicioRecepcionMensaje += new HandleReception(value);
            }
            remove
            {
                inicioRecepcionMensaje -= value;
            }
        }
        public event HandleReception InicioRecepcionArchivo
        {
            add
            {
                inicioRecepcionArchivo += new HandleReception(value);
            }
            remove
            {
                inicioRecepcionArchivo -= value;
            }
        }

        private int NumeroDeTramas;
        private Trama[] Tramas;
        private readonly int TamanoTrama;

        public TramasRecepcion(Trama tramaInformacion)
        {
            IndiceTrama = 0;
            Completado = false;
            DecodificarTramaInformacion(tramaInformacion);
        }

        /// <summary>
        /// Lee la siguiente trama y la devuelve, si llegó al final de la trama cambia el estado
        /// del objeto y envia la última trama. Si ya ha terminado de enviar vuelve a enviar la
        /// última trama
        /// </summary>
        /// <returns></returns>
        public void RecibirTrama(Trama trama)
        {
            if (!Completado)
            {
                Tramas[IndiceTrama] = trama;
                IndiceTrama++;
                if (IndiceTrama == NumeroDeTramas)
                {
                    Completado = true;
                }
            }
        }

        public double Progreso()
        {
            double progreso = (double) IndiceTrama / NumeroDeTramas;
            return progreso;
        }

        private void DecodificarTramaInformacion(Trama tramaCabecera)
        {

            var cabecera = new Cabecera(tramaCabecera);

            if (cabecera.TipoContenido == ETipoContenido.Cabecera)
            {
                TipoDatos = cabecera.TipoTrama;
                IdRecepcion = cabecera.IdRecepcion;

                var cuerpoTrama = tramaCabecera.ObtenerCuerpoDesdeContenido();
                var cuerpoDecodificado = Encoding.UTF8.GetString(cuerpoTrama);
                MetaDatos = cuerpoDecodificado.Split(';');

                Emisor = MetaDatos[0];
                var tamano = long.Parse(MetaDatos[1]);

                NumeroDeTramas = (int) Math.Ceiling((double) tamano / TamanoTrama);
                Tramas = new Trama[NumeroDeTramas + 1];
            }
        }

        public void EnsamblarArchivo(string path)
        {
            if (Completado)
            {
                var nombre = MetaDatos[2];
                var extension = MetaDatos[3];
                path += $"/{nombre}.{extension}";
                var archivoRecibido = new ArchivoRecepcion(path);
                for (int i = 0; i < Tramas.Length; i++)
                {
                    var cuerpoValido = Tramas[i].ObtenerCuerpoValido();
                    archivoRecibido.Escribir(cuerpoValido);
                }
                archivoRecibido.Cerrar();
                DisplayMessage = archivoRecibido.NuevoArchivo.Path;
            }
        }

        public void EnsamblarMensaje()
        {
            DisplayMessage = "";
            if (Completado)
            {
                for (int i = 0; i < Tramas.Length; i++)
                {
                    DisplayMessage += Encoding.UTF8.GetString(Tramas[i].ObtenerCuerpoValido());
                }
            }
        }

        public void Ensamblar(string path = "D:/recepcion")
        {
            switch (TipoDatos)
            {
                case (ETipoTrama.Archivo):
                    EnsamblarArchivo(path);
                    break;
                case ETipoTrama.Mensaje:
                    EnsamblarMensaje();
                    break;
                default:
                    throw new Exception("Trama no reconocida");
            }
        }


    }
}
