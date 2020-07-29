using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;
using System.Threading;


namespace winproySerialPort
{
    class classTransRecep
    {
        public delegate void HandlerTxRx(object oo, string mensRec);
        public event HandlerTxRx LlegoMensaje;

        Thread procesoEnvio;
        private SerialPort puerto;
        private string mensajeEnviar;
        private string mensRecibido;

        byte[] TramaEnvio;
        byte[] TramaCabeceraEnvio;
        byte[] TramaRelleno;

        public classTransRecep()
        {
            TramaEnvio = new byte[1024];
            TramaCabeceraEnvio = new byte[5];
            TramaRelleno = new byte[1024];

            for (int i=0; i<1024; i++)
            {
                TramaRelleno[i]= 64;
            }
        }

        public void Inicializa(string NombrePuerto)
        {
            try
            {
                puerto = new SerialPort(NombrePuerto, 57600, Parity.Even, 8, StopBits.Two);
                puerto.ReceivedBytesThreshold = 1024;
                puerto.DataReceived += new SerialDataReceivedEventHandler(puerto_DataReceived);
                puerto.Open();
            }
            catch(UnauthorizedAccessException e)
            {
                MessageBox.Show("El puerto COM1 está siendo utilizado, ahora se utilizará el puerto al COM2");
                puerto = new SerialPort("COM2", 57600, Parity.Even, 8, StopBits.Two);
                puerto.ReceivedBytesThreshold = 1024;
                puerto.DataReceived += new SerialDataReceivedEventHandler(puerto_DataReceived);
                puerto.Open();
            }
            finally
            {
                
            }
                


            MessageBox.Show("Apertura del puerto " + puerto.PortName);
        }

        private void puerto_DataReceived(object o, SerialDataReceivedEventArgs a)
        {
            //MessageBox.Show("Se disparó el evento de recepción.");
            mensRecibido = puerto.ReadExisting();
            OnLlegoMensaje();
            //MessageBox.Show(mensRecibido);
        }

        protected virtual void OnLlegoMensaje()
        {
            if (LlegoMensaje != null)
            {
                LlegoMensaje(this, mensRecibido);
            }
        }

        public void Enviar(string mens)
        {
            mensajeEnviar = mens;
            string longReal = mensajeEnviar.Length.ToString();
            string longMensString = "";
            int contDigit=4;

            TramaEnvio = ASCIIEncoding.UTF8.GetBytes(mensajeEnviar);
            //TramaCabeceraEnvio va a contener la longitud del mensaje
            for(int i=1; i<=(5-longReal.Length); i++)
            {
                longMensString += "0";
            }
            longMensString += longReal.ToString();
            /*if (longReal.Length == 1)
                longMensString = "0000" + longReal.ToString();
            if (longReal.Length == 2)
                longMensString = "000" + longReal.ToString();
            if (longReal.Length == 3)
                longMensString = "00" + longReal.ToString();
            if (longReal.Length == 4)
                longMensString = "0" + longReal.ToString();
            */
            

            TramaCabeceraEnvio = ASCIIEncoding.UTF8.GetBytes(longMensString);
            procesoEnvio = new Thread(Enviando);
            procesoEnvio.Start();


        }

        private void Enviando()
        {
            // puerto.Write(mensajeEnviar);
            puerto.Write(TramaCabeceraEnvio, 0, 5);
            puerto.Write(TramaEnvio, 0, TramaEnvio.Length);
            puerto.Write(TramaRelleno, 0, 1024-(TramaEnvio.Length + 5));
            //puerto.Write(TramaRelleno, 0, 1019 - TramaEnvio.Length);
           MessageBox.Show("mensaje terminado de enviar");
        }

        public void Recibir()
        {
            mensRecibido = puerto.ReadExisting();
            MessageBox.Show(mensRecibido);
        }


    }
}
