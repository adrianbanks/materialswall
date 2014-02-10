using System;
using Granta.MaterialsWall.Images;
using NUnit.Framework;

namespace Granta.MaterialsWall.Tests.Images
{
    [TestFixture]
    public sealed class QRCodeLinkFormatterTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void Generate_ThrowsAnException_WhenTheSuppliedBaseUrlIsInvalid(string baseUrl)
        {
            var formatter = new QRCodeLinkFormatter();
            Assert.Throws<ArgumentNullException>(() => formatter.FormatUrl(baseUrl, Guid.NewGuid()));
        }
    }
}
