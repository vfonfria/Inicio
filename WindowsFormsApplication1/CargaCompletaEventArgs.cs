using System;
using System.Collections.Generic;

namespace WindowsFormsApplication1
{
    public class CargaCompletaEventArgs:EventArgs
    {
        public List<OpenPop.Mime.Header.MessageHeader> Headers { get; set; }
    }
}