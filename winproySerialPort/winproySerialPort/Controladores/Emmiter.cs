using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.Threading;

namespace winproySerialPort
{
    class Emmiter
    {
        public Mensaje MensajeEmisor { get; set; }
        public int TamanoDeTramas { get; set; }
        public int TamanoDeCabeceras { get; set; }
        public string MensajeEnviar { get; set; }
        public bool EnvioTerminado { get; set; }
        public int IndiceTrama { get; set; }
        private string CabeceraTramas;
        private int NumeroDeTramas;
        private ArchivoLectura archivoEnviar;
        private ArchivoEscritura archivoRecibir;
        private byte[] ARREGLO = new byte[1024];
        private Trama[] Tramas;
        public delegate void EmmitHandler(object obj, string mensaje);
        private event EmmitHandler messageEmmited;
        public event EmmitHandler MessageEmmited
        {
            add
            {
                messageEmmited += new EmmitHandler(value);
            }
            remove
            {
                messageEmmited -= value;
            }
        }

        public Emmiter(string nombrePuerto = "COM1", int tamanoDeTramas = 1024, int tamanoDeCabeceras = 5)
        {
            Puerto.Inicializar(nombrePuerto);
            TamanoDeTramas = tamanoDeTramas;
            TamanoDeCabeceras = tamanoDeCabeceras;
        }
        public void Inicializar(string mensaje)
        {
            MensajeEmisor = new Mensaje(mensaje);
            EnvioTerminado = false;
            IndiceTrama = 0;
            CabeceraTramas = generarCabecera();
            GenerarTramas();
        }

        public void Enviar()
        {
            Puerto.Escribir(MensajeEnviar);
        }

        public void Transmitir()
        {
            // archivoEnviar = new ArchivoLectura(path);
            // archivoEnviar.Leer(ARREGLO);
            while(!EnvioTerminado)
            {
                Puerto.Escribir(Tramas[IndiceTrama]);
                SiguienteTrama();
            }
            OnEnvioTerminado();
        }
        protected virtual void OnEnvioTerminado()
        {
            messageEmmited?.Invoke(this, Encoding.UTF8.GetString(MensajeEmisor.Contenido));
        }
        public void Recibir(string path)
        {
            archivoRecibir = new ArchivoEscritura(path);
            archivoRecibir.Escribir(ARREGLO);
        }
        private int CalcularNumeroDeTramas()
        {
            double numeroDeTramas = Convert.ToDouble(MensajeEmisor.TamanoMensaje) / (TamanoDeTramas - TamanoDeCabeceras);
            return (int) Math.Ceiling(numeroDeTramas);
        }
        private string generarCabecera()
        {
            string formato = "D" + (TamanoDeCabeceras - 1);
            string cabecera = "M" + MensajeEmisor.Contenido.Length.ToString(formato);
            return cabecera;
        }
        private void GenerarTramas()
        {
            var restante = MensajeEmisor.TamanoMensaje;
            NumeroDeTramas = CalcularNumeroDeTramas();
            Tramas = new Trama[NumeroDeTramas];
            for (int i = 0; i < NumeroDeTramas; i++)
            {
                Tramas[i] = new Trama(TamanoDeTramas, TamanoDeCabeceras);
                var maximo = (restante < Tramas[i].TamanoDeCuerpo) ? restante : Tramas[i].TamanoDeCuerpo;
                var temp = new byte[maximo];
                for (int j = 0; j < maximo; j++)
                {
                    temp[j] = MensajeEmisor.Contenido[i * Tramas[i].TamanoDeCuerpo + j];
                }
                Tramas[i].GenerarCabecera(CabeceraTramas);
                Tramas[i].GenerarCuerpo(temp);
                Tramas[i].GenerarTramaEnvio();
                restante -= Tramas[i].TamanoDeCuerpo;
            }
        }
        public void SiguienteTrama()
        {
            if (!EnvioTerminado)
            {
                IndiceTrama++;
                if (IndiceTrama >= NumeroDeTramas)
                {
                    EnvioTerminado = true;
                }
            }
        }
    }
}
