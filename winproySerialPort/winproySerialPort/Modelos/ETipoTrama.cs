using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winproySerialPort.Modelos
{
    enum ETipoTrama
    {
        Archivo = 'A',
        Mensaje = 'M'
    }

    enum ETipoContenido
    {
        Cabecera = 'C',
        Informacion = 'I'
    }
}
