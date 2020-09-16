using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winproySerialPort
{
    class Trama
    {
        public byte[] Contenido { get; set; }
        public byte Relleno { get; private set; }
        public string TipoDeTrama { get; set; }
        public int TamanoDeTrama { get; set; }
        public int TamanoDeCabecera { get; set; }
        public int TamanoDeCuerpo { get; set; }

        public Trama(byte[] cuerpo, string cabecera, int tamanoDeTrama = 1024, int tamanoDeCabecera = 5, byte byteDeRelleno = 64)
        {
            TamanoDeTrama = tamanoDeTrama;
            TamanoDeCabecera = tamanoDeCabecera;
            Relleno = byteDeRelleno;
            TamanoDeCuerpo = TamanoDeTrama - TamanoDeCabecera;
            GenerarCabecera(cabecera);
            GenerarCuerpo(cuerpo);
            GenerarTramaEnvio();
        }

        public Trama(byte[] contenido, int tamanoDeTrama = 1024, int tamanoDeCabecera = 5, byte byteDeRelleno = 64)
        {
            TamanoDeTrama = tamanoDeTrama;
            TamanoDeCabecera = tamanoDeCabecera;
            Relleno = byteDeRelleno;
            Contenido = contenido;
            TamanoDeCuerpo = TamanoDeTrama - TamanoDeCabecera;
        }

        public Trama(int tamanoDeTrama = 1024, int tamanoDeCabecera = 5, byte byteDeRelleno = 64)
        {
            TamanoDeTrama = tamanoDeTrama;
            TamanoDeCabecera = tamanoDeCabecera;
            Relleno = byteDeRelleno;
            TamanoDeCuerpo = TamanoDeTrama - TamanoDeCabecera;
            Contenido = new byte[TamanoDeTrama];
        }
        public void GenerarCabecera(string cabecera)
        {
            // Cabecera = Encoding.UTF8.GetBytes(cabecera);
        }
        public void GenerarCuerpo(byte[] cuerpo)
        {
            /* Cuerpo = new byte[TamanoDeCuerpo];
            for (int i = 0; i < TamanoDeCuerpo; i++)
            {
                if (i < cuerpo.Length) Cuerpo[i] = cuerpo[i];
                else Cuerpo[i] = Relleno;
            } */
        }
        public void GenerarTramaEnvio()
        {
            /*
            Contenido = new byte[TamanoDeTrama];
            for (int i = 0; i < TamanoDeTrama; i++)
            {
                Contenido[i] = (i >= TamanoDeCabecera) ? Cuerpo[i - TamanoDeCabecera] : Cabecera[i];
            }*/
        }
        public byte[] ObtenerCabeceraDesdeContenido()
        {
            
            var Cabecera = new byte[TamanoDeCabecera];
            for (int i = 0; i < TamanoDeCabecera; i++)
            {
                Cabecera[i] = Contenido[i];
            }
            return Cabecera;
        }
        public byte[] ObtenerCuerpoDesdeContenido()
        {
            
            TamanoDeCuerpo = TamanoDeTrama - TamanoDeCabecera; 
            var Cuerpo = new byte[TamanoDeCuerpo];
            for (int i = 0; i < TamanoDeCuerpo; i++)
            {
                Cuerpo[i] = Contenido[TamanoDeCabecera + i];
            }
            return Cuerpo;
        }
        public byte[] ObtenerCuerpoValido()
        {
            var temp = new byte[TamanoDeCuerpo];
            var tamanoCuerpoValido = TamanoDeCuerpo;
            for (int i = 0; i < TamanoDeCuerpo; i++)
            {
               /* if (Cuerpo[i] == Relleno)
                {
                    tamanoCuerpoValido = i;
                    break;
                }
                temp[i] = Cuerpo[i]; */
            }
            var cuerpoValido = new byte[tamanoCuerpoValido];
            Array.Copy(temp, cuerpoValido, tamanoCuerpoValido);
            return cuerpoValido;
        }
    }
}
