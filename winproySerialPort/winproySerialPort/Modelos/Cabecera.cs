using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winproySerialPort.Modelos
{
    class Cabecera
    {
        public ETipoTrama TipoTrama { get; set; }
        public ETipoContenido TipoContenido { get; set; }
        public int IdRecepcion { get; set; }
        public Cabecera(Trama trama)
        {
            var cabeceraTrama = trama.ObtenerCabeceraDesdeContenido();
            var cabeceraDecodificada = Encoding.UTF8.GetString(cabeceraTrama);

            TipoTrama = (ETipoTrama) cabeceraDecodificada.ElementAt(0);
            TipoContenido = (ETipoContenido) cabeceraDecodificada.ElementAt(1);
            IdRecepcion = int.Parse(cabeceraDecodificada.Substring(2, 3));
        }
    }
}
