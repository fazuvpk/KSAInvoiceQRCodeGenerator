using KSAQRCodeGenerator.Extensions;
using KSAQRCodeGenerator.Helper;
using KSAQRCodeGenerator.Models;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace KSAQRCodeGenerator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetQRCodeWithTag(QRGeneratorInput input)
        {
            string base64Image = "";

            string base64Text;

            MemoryStream stream = new MemoryStream();
            stream.Append(1);
            var sellerNameBytes = Encoding.UTF8.GetBytes(input.SellerName);
            stream.Append(Convert.ToByte(sellerNameBytes.Length));
            stream.Append(sellerNameBytes);

            stream.Append(2);
            var vatNoBytes = Encoding.UTF8.GetBytes(input.VATNumber);
            stream.Append(Convert.ToByte(vatNoBytes.Length));
            stream.Append(vatNoBytes);

            stream.Append(3);
            var timestampBytes = Encoding.UTF8.GetBytes(input.TimeStamp);
            stream.Append(Convert.ToByte(timestampBytes.Length));
            stream.Append(timestampBytes);

            stream.Append(4);
            var invoiceTotalBytes = Encoding.UTF8.GetBytes(input.InvoiceTotal);
            stream.Append(Convert.ToByte(invoiceTotalBytes.Length));
            stream.Append(invoiceTotalBytes);

            stream.Append(5);
            var vatTotalBytes = Encoding.UTF8.GetBytes(input.VATTotal);
            stream.Append(Convert.ToByte(vatTotalBytes.Length));
            stream.Append(vatTotalBytes);

            var byteArr = stream.ToArray();
            base64Text = Convert.ToBase64String(byteArr);

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(base64Text, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap bImage = qrCode.GetGraphic(3);
            using (var ms = new MemoryStream())
            {
                bImage.Save(ms, ImageFormat.Jpeg);
                byte[] byteImage = ms.ToArray();
                base64Image = Convert.ToBase64String(byteImage);
            }
            return Json(new { QRCode = base64Image, Base64Text = base64Text, DecodedText = Base64Helper.Decode(base64Text) }, JsonRequestBehavior.AllowGet);
        }
    }
}