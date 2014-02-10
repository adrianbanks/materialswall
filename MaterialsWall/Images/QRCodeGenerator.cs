using System;
using System.Drawing;
using ZXing;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace Granta.MaterialsWall.Images
{
    public interface IQRCodeGenerator
    {
        Bitmap Generate(string baseUrl, Guid identifier, int size);
    }

    public sealed class QRCodeGenerator : IQRCodeGenerator
    {
        private readonly IQRCodeLinkFormatter qCodeLinkFormatter;

        public QRCodeGenerator(IQRCodeLinkFormatter qCodeLinkFormatter)
        {
            if (qCodeLinkFormatter == null)
            {
                throw new ArgumentNullException("qCodeLinkFormatter");
            }
            
            this.qCodeLinkFormatter = qCodeLinkFormatter;
        }

        public Bitmap Generate(string baseUrl, Guid identifier, int size)
        {
            if (size < 1)
            {
                throw new ArgumentException("Size must be a positive integer", "size");
            }

            string url = qCodeLinkFormatter.FormatUrl(baseUrl, identifier);
            var writer = new BarcodeWriter {Format = BarcodeFormat.QR_CODE};
            var options = new QrCodeEncodingOptions {ErrorCorrection = ErrorCorrectionLevel.L, Height = size, Width = size};
            writer.Options = options;
            return writer.Write(url);
        }
    }
}
