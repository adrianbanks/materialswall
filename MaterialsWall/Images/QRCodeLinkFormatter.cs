using System;

namespace Granta.MaterialsWall.Images
{
    public interface IQRCodeLinkFormatter
    {
        string FormatUrl(string baseUrl, Guid identifier);
    }

    public sealed class QRCodeLinkFormatter : IQRCodeLinkFormatter
    {
        public string FormatUrl(string baseUrl, Guid identifier)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new ArgumentNullException("baseUrl");
            }
            
            return string.Format("{0}QR/{1}", baseUrl, identifier);
        }
    }
}
