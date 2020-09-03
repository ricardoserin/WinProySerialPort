using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.Windows.Forms;
using System.Threading;


namespace winproySerialPort
{
    class classTransRecep
    {
        public delegate void HandlerTxRx(object oo, string mensRec);
        public event HandlerTxRx LlegoMensaje;
        public event HandlerTxRx EnviadoMensaje;

        //archivos
        private classArchivo archivoEnviar;
        private FileStream FlujoArchivoEnviar;
        private BinaryReader LeyendoArchivo;

        private classArchivo archivoRecibir;
        private FileStream FlujoArchivoRecibir;
        private BinaryWriter EscribiendoArchivo;

        Thread procesoEnvio;
        Thread procesoVerificaSalida;
        Thread procesoRecibirMensaje;

        Thread procesoEnvioArchivo;
        Thread procesoConstruyeArchivo;

        private SerialPort puerto; 
        public string nombrePuerto;
        public int baudiosPuerto;
        private string mensajeEnviar;
        private string mensRecibido;

        private Boolean BufferSalidaVacio;

        private byte[] TramaEnvio;
        private byte[] TramaCabeceraEnvio;
        private byte[] TramaRelleno;
        private byte[] TramaRecibida;

        private int LongRelleno;

        private int contArchivos;
        int tramasEnviadas = 0;
        public int tramasRecibidas = 0;
        private bool estadoConexion;

        public classTransRecep()
        {
            TramaEnvio = new byte[1019];
            TramaCabeceraEnvio = new byte[5];
            TramaRelleno = new byte[1019];

            TramaRecibida = new byte[1024];

            for (int i=0; i<1019; i++)
            {
                TramaRelleno[i]= 64;
            }
        }

        public void Inicializa(string NombrePuerto, int Baudios)
        {
            nombrePuerto = NombrePuerto;
            baudiosPuerto = Baudios;
            puerto = new SerialPort(nombrePuerto, baudiosPuerto, Parity.Even, 8, StopBits.Two);
            puerto.ReceivedBytesThreshold = 1024;
            //Desencadena el método puerto_DataReceived
            puerto.DataReceived += new SerialDataReceivedEventHandler(puerto_DataReceived);
            try
            {                
                puerto.Open();
                estadoConexion = true;
                //Esta hebra es un bucle infinito xd
                BufferSalidaVacio = true; //Inicialmente el buffer de salida está vacío
                procesoVerificaSalida = new Thread(VerificandoSalida); //
                procesoVerificaSalida.Start();

                archivoEnviar = new classArchivo();
                archivoRecibir = new classArchivo();
                MessageBox.Show("Apertura del puerto " + puerto.PortName);

                contArchivos = 0;

            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("El puerto "+nombrePuerto+" está siendo utilizado, por favor seleccione otro.");
                //puerto = new SerialPort("COM2", 57600, Parity.Even, 8, StopBits.Two);
                //puerto.ReceivedBytesThreshold = 1024;
                //puerto.DataReceived += new SerialDataReceivedEventHandler(puerto_DataReceived);
                //puerto.Open();
                estadoConexion = false;
            }
            catch(System.IO.IOException ex)
            {
                MessageBox.Show("No se puede establecer conexion. "+ex.Message);
                estadoConexion = false;
                //MessageBox.Show("Estado conexion false");
            }
            finally
            {
                
            }
            //MessageBox.Show("Estado conexion true");
        }

        public void CerrarConexion()
        {
            estadoConexion = false;
            puerto.Close();
        }

        private void puerto_DataReceived(object o, SerialDataReceivedEventArgs sd)
        {
            if(puerto.BytesToRead >=1024)
            {
                puerto.Read(TramaRecibida, 0, 1024);

                string TAREA = ASCIIEncoding.UTF8.GetString(TramaRecibida, 0, 1);
                
                switch (TAREA)
                {
                    case "M":
                        procesoRecibirMensaje = new Thread(RecibiendoMensaje);
                        procesoRecibirMensaje.Start();
                        break;
                    case "A":
                        string TipoTramaArchivo = ASCIIEncoding.UTF8.GetString(TramaRecibida, 1, 1);
                        if (TipoTramaArchivo.Equals("I"))
                        {
                            string TramaInf = ASCIIEncoding.UTF8.GetString(TramaRecibida, 0, 1019);
                            //'?' + archivoEnviar.Nombre + '?' + archivoEnviar.Extension + '?' + archivoEnviar.Tipo + '?' + archivoEnviar.Tamano
                            int id = Convert.ToInt16(TramaInf.Substring(2, 3));
                            string nombre = TramaInf.Substring(6, TramaInf.IndexOf('?',7)-6);
                            string ext = TramaInf.Substring((5+nombre.Length)+2,3);
                            //MessageBox.Show("leng"+TramaInf.Length+"eee"+((TramaInf.Length - TramaInf.LastIndexOf('?')) - 1));
                            long tam = 261753; //word: 12217 pdf:261753 img: 13607 sql: 297 word2det: 12899
                            //MessageBox.Show("info: " + nombre + '-' + ext + '-' + tam);
                            IniciaConstruirArchivo(nombre,ext,tam,0);
                        }
                        else if(TipoTramaArchivo.Equals("D"))
                        {
                            //procesoConstruyeArchivo = new Thread(construirArchivo);
                            //procesoConstruyeArchivo.Start();
                            //MessageBox.Show("se recibió trama" + tramasRecibidas);
                            procesoConstruyeArchivo = new Thread(construirArchivo);
                            procesoConstruyeArchivo.Start();
                            //construirArchivo();
                        }
                        break;
                    default: MessageBox.Show("Trama no reconocida"+TAREA);
                        break;
                }
                //string CabRec = ASCIIEncoding.UTF8.GetString(TramaRecibida, 0, 5);
                //int LongMensRec = Convert.ToInt16(CabRec);

                //mensRecibido = ASCIIEncoding.UTF8.GetString(TramaRecibida, 5, LongMensRec);

                //OnLlegoMensaje();
            }

        }

        private void RecibiendoMensaje()
        {
            string CabRec = ASCIIEncoding.UTF8.GetString(TramaRecibida, 1, 4);
            int LongMensRec = Convert.ToInt16(CabRec);

            //Decodificamos el mensaje
            mensRecibido = ASCIIEncoding.UTF8.GetString(TramaRecibida, 5, LongMensRec);

            //Disparamos el evento después de decodificar el mensaje
            OnLlegoMensaje();
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
            string longMensString = "M";

            TramaEnvio = ASCIIEncoding.UTF8.GetBytes(mensajeEnviar);
            //TramaCabeceraEnvio va a contener la longitud del mensaje
            for(int i=1; i<=(4-longReal.Length); i++)
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
                longMensString = "0" + longReal.ToString();*/
            
            TramaCabeceraEnvio = ASCIIEncoding.UTF8.GetBytes(longMensString);
            procesoEnvio = new Thread(Enviando);
            procesoEnvio.Start();
        }

        private void Enviando()
        {
            //Excepción no controlada: System.InvalidOperationException: 'El puerto está cerrado.'
            try
            {
                while (!BufferSalidaVacio)
                {
                    //esperamos
                }
                puerto.Write(TramaCabeceraEnvio, 0, 5);
                puerto.Write(TramaEnvio, 0, TramaEnvio.Length);
                LongRelleno = (1024 - (TramaEnvio.Length + 5));
                if (LongRelleno > 0)
                {
                    puerto.Write(TramaRelleno, 0, LongRelleno);
                }
                //MessageBox.Show("Trama Enviada");
            }
            catch(InvalidOperationException ex)
            {
                MessageBox.Show("No se estableció una conexión, por favor configure el puerto.");
            }
        }

        public void Recibir()
        {
            mensRecibido = puerto.ReadExisting();
            MessageBox.Show(mensRecibido);
        }

        private void VerificandoSalida()
        {
            //Verifica si existen bytes en el buffer de salida 
            while (estadoConexion)
            {
                if(puerto.BytesToWrite > 0)
                {
                    //Bandera que índicad si el buffer de salida está vacío o no
                    BufferSalidaVacio = false;
                }
                else
                {
                    BufferSalidaVacio = true;
                }
            }
        }

        public int BytesPorSalir() //Devuelve la cantidad de bytes que faltan salir del buffer
        {
            int cantBytes = 0;
            if(BufferSalidaVacio == false)
            {
                //Preguntamos cuántos bytes faltan salir cuando el buffer no está vacío
                cantBytes = puerto.BytesToWrite;
            }
            return cantBytes;
        }

        public bool EstadoConexion()
        {
            bool estado;
            estado = puerto.IsOpen;
            return estado;
        }

        //archivos
        public void IniciaEnvioArchivo(string path)
        {
            int tamNombre = path.Length - (path.LastIndexOf('\\')) - (path.Length - path.LastIndexOf('.'))-1;
            //abrirlo, manejar el control de errorres (excepciones)
            //leerlo en stream
            //iniciar una hebra de envío
            FlujoArchivoEnviar = new FileStream(path, FileMode.Open, FileAccess.Read);
            LeyendoArchivo = new BinaryReader(FlujoArchivoEnviar);
            archivoEnviar.Path = path;
            archivoEnviar.Extension = path.Substring(path.LastIndexOf('.') + 1, path.Length - path.LastIndexOf('.') - 1);
            archivoEnviar.Directorio = path.Substring(0, path.LastIndexOf('\\'));
            archivoEnviar.Nombre = path.Substring(path.LastIndexOf('\\') + 1, tamNombre);
            archivoEnviar.Tamano = FlujoArchivoEnviar.Length;
            archivoEnviar.Tipo = "Archivo genérico";
            archivoEnviar.Avance = 0;
            archivoEnviar.Id = contArchivos;
            contArchivos += 1;
            //MessageBox.Show("Se enviará: " + '\n' + archivoEnviar.Nombre+'\n' + archivoEnviar.Tamano+ '\n' + archivoEnviar.Id);

            procesoEnvioArchivo = new Thread(EnviandoInfoArchivo);
            procesoEnvioArchivo.Start();
        }
        private void EnviandoInfoArchivo()
        {
            //byte[] TramaInfoArchivo = new byte [1019];
            //byte[] TramaCabeceraInfoArchivo = new byte[5];
            //TramaCabeceraInfoArchivo = ASCIIEncoding.UTF8.GetBytes("AI001");
            //TramaInfoArchivo = ASCIIEncoding.UTF8.GetBytes("prueba prueba prueba");
            TramaCabeceraEnvio = ASCIIEncoding.UTF8.GetBytes("AI001");
            TramaEnvio = ASCIIEncoding.UTF8.GetBytes('?'+archivoEnviar.Nombre + '?' + archivoEnviar.Extension + '?' + archivoEnviar.Tipo + '?' + archivoEnviar.Tamano);
            Enviando();
            while (BufferSalidaVacio == false)
            {
                //esperamos
            }
            EnviandoArchivo();
            //procesoEnvioArchivo = new Thread(EnviandoArchivo);
            //procesoEnvioArchivo.Start();
            //puerto.Write(TramaCabeceraInfoArchivo, 0, 5);
            //BufferSalidaVacio = false;
            //puerto.Write(TramaInfoArchivo, 0, TramaInfoArchivo.Length);
            //BufferSalidaVacio = false;
            //puerto.Write(TramaRelleno, 0, 1019 - TramaInfoArchivo.Length);
            //BufferSalidaVacio = false;
            //MessageBox.Show(ASCIIEncoding.UTF8.GetString(TramaCabeceraInfoArchivo) + ASCIIEncoding.UTF8.GetString(TramaInfoArchivo));
        }

        private void EnviandoArchivo()
        {
            //byte[] TramaEnvioArchivo; //se ejecuta una sola vez
            //byte[] TramaCabeceraEnvioArchivo; //se ejecuta una sola vez
            //byte[] TramaInfoArchivo;
            //byte[] TramaCabeceraInfoArchivo;
            //TramaEnvioArchivo = new byte[1019];
            //TramaCabeceraEnvioArchivo = new byte[5];
            //TramaCabeceraInfoArchivo = new byte[5];
            //TramaInfoArchivo = new byte[1019];
            //enviar la primera trama con el nbombre archivo
            
            //-MessageBox.Show("Enviando trama con información del archivo");
            //ENVIAR TRAMA DE INFORMACIÓN

            //ENVIAR LAS TRAMAS DE DATOS

            //-MessageBox.Show("trama cabecera: " + ASCIIEncoding.UTF8.GetString(TramaCabeceraEnvioArchivo));
            //while (archivoEnviar.Avance <= archivoEnviar.Tamano - 1019)
            TramaCabeceraEnvio = ASCIIEncoding.UTF8.GetBytes("AD001");
            int TramasEsperando = Convert.ToInt32(Math.Ceiling((double)archivoEnviar.Tamano / 1019));
            //MessageBox.Show("Tramas que se enviarán: " + TramasEsperando);
            TramaEnvio = new byte[1019];
            while (TramasEsperando>0)
            {
                LeyendoArchivo.Read(TramaEnvio, 0, 1019);
                while (BufferSalidaVacio == false)
                {
                    //esperamos
                }
                Enviando();
                TramasEsperando -= 1;
                //archivoEnviar.Avance+=1019;
                //envio de una trama llena de 1019 bytes del archivo
                //espera aleatoria
                //puerto.Write(TramaCabeceraEnvioArchivo,0,5);
                //BufferSalidaVacio = false;
                //puerto.Write(TramaEnvioArchivo, 0, 1019);
                //BufferSalidaVacio = false;
                //tramasEnviadas += 1;
                //-MessageBox.Show("trama enviada: " + tramas);
                //MessageBox.Show("avance : " + archivoEnviar.Avance.ToString());
            }
            //int restante = Convert.ToInt32(archivoEnviar.Tamano - archivoEnviar.Avance);
            //LeyendoArchivo.Read(TramaEnvioArchivo,0,restante);
            ////envio de la última trama + relleno
            //while (BufferSalidaVacio == false)
            //{
            //    //esperamos
            //}
            ////-MessageBox.Show("avance : " + archivoEnviar.Avance.ToString()+"falta:"+ restante.ToString());
            //puerto.Write(TramaCabeceraEnvioArchivo, 0, 5);
            //puerto.Write(TramaEnvioArchivo, 0, restante);
            //puerto.Write(TramaRelleno, 0, 1019 - restante);
            //tramasEnviadas += 1;
            //-MessageBox.Show("Se enviaron: " + (archivoEnviar.Avance+ restante) + " bytes"+'\n'+ (archivoEnviar.Avance/1019)+"KB");
            //-MessageBox.Show("tramas enviadas: " + tramas);
            //while (!BufferSalidaVacio)
            //{
            //    //esperamos
            //}
            LeyendoArchivo.Close();
            FlujoArchivoEnviar.Close();
        }
        public void IniciaConstruirArchivo(string nombre, string ext, long tam, int id)
        {
            //-MessageBox.Show(trama);
            //string nom = trama.Substring(4, trama.IndexOf('?')-1);
            //string tipo = trama.Substring(nom.Length, trama.IndexOf('?', nom.Length));
            //string ext = trama.Substring(tipo.Length, trama.IndexOf('?', tipo.Length));
            //MessageBox.Show(trama.Substring(ext.Length, trama.IndexOf('?', ext.Length)));
            //long tam = 261753;
            //int id;
            string dir = "D:\\PRUEBA2\\received\\";
            string path = dir + nombre +'.' + ext;
            FlujoArchivoRecibir = new FileStream(path, FileMode.Create, FileAccess.Write);
            EscribiendoArchivo = new BinaryWriter(FlujoArchivoRecibir);
            archivoRecibir.Nombre = nombre;
            archivoRecibir.Id = id;
            archivoRecibir.Extension = ext;
            archivoRecibir.Tamano = tam;
            archivoRecibir.Avance = 0;
        }
        private void construirArchivo()
        {
            //debe realizarse en función del tamaño 1019 y la última será tamito
            //-MessageBox.Show("se recibieron: "+archivoRecibir.Tamaño);
            if(archivoRecibir.Avance<=archivoRecibir.Tamano - 1019)
            {
                EscribiendoArchivo.Write(TramaRecibida,5,1019);
                archivoRecibir.Avance += 1019;
            }
            else
            {
                int restante = Convert.ToInt32(archivoRecibir.Tamano - archivoRecibir.Avance);
                EscribiendoArchivo.Write(TramaRecibida, 5, restante);
                FlujoArchivoRecibir.Close();
                EscribiendoArchivo.Close();
                MessageBox.Show("Archivo recibido con éxito.");
            }
        }
    }
}
