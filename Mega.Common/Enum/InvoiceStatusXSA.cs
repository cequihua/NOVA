using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mega.Common.Enum
{
    #region ADTECH - Status de envio a XSA en base a tabla UDCItem por Id
    public enum InvoiceStatusXSA
    {
        Facturado = 190021, // Enviado y Facturado en XSA
        Pendiente = 190022 // Pendiente por enviar y facturar en XSA
    }
    #endregion
}
