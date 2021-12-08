using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KSAQRCodeGenerator.Models
{
    public class QRGeneratorInput
    {
        public string SellerName { get; set; }
        public string VATNumber { get; set; }
        public string TimeStamp { get; set; }
        public string InvoiceTotal { get; set; }
        public string VATTotal { get; set; }
    }
}