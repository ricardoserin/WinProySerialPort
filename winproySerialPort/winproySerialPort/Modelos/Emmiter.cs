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
        public delegate void EmmitHandler(object obj, string mensaje);
        private event EmmitHandler messageEmmited;
        private event EmmitHandler transmissionStarted;
        public double ProgresoEnvio { get; set; }
        public bool Enviando { get; set; }

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

        public event EmmitHandler TransmissionStarted
        {
            add
            {
                transmissionStarted += new EmmitHandler(value);
            }
            remove
            {
                transmissionStarted -= value;
            }
        }

        public Emmiter(string nombrePuerto = "COM1", int tamanoDeTramas = 1024, int baudios = 57600)
        {
            Puerto.Inicializar(nombrePuerto, baudios, tamanoDeTramas);
            Enviando = false;
        }

        public void Transmitir(TramasEnvio tramas)
        {
            OnEnvioIniciado();
            while (!tramas.Enviado)
            {
                ProgresoEnvio = tramas.Progreso();
                Puerto.Escribir(tramas.SiguienteTrama());
            }
            OnEnvioTerminado(tramas.DisplayMessage);
        }
        protected virtual void OnEnvioTerminado(string mensaje)
        {
            Enviando = false;
            messageEmmited?.Invoke(this, mensaje);
        }
        protected virtual void OnEnvioIniciado()
        {
            Enviando = true;
            ProgresoEnvio = 0;
            transmissionStarted?.Invoke(this, "Inicio envío");
        }
    }
}
