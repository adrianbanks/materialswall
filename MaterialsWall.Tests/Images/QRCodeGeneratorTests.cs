using System;
using Granta.MaterialsWall.Images;
using NSubstitute;
using NUnit.Framework;
using ZXing;
using BarcodeReader = ZXing.Presentation.BarcodeReader;

namespace Granta.MaterialsWall.Tests.Images
{
    [TestFixture]
    public sealed class QRCodeGeneratorTests
    {
        [Test]
        public void Ctor_ThrowsAnException_WhenTheSuppliedQRCodeLinkFormatterIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new QRCodeGenerator(null));
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Generate_ThrowsAnException_WhenThesuppliedSizeIsInvalid(int size)
        {
            var linkFormatter = Substitute.For<IQRCodeLinkFormatter>();
            var generator = new QRCodeGenerator(linkFormatter);
            Assert.Throws<ArgumentException>(() => generator.Generate("baseUrl", Guid.NewGuid(), size));
        }

        [TestCase(100)]
        [TestCase(75)]
        public void Generate_CreatesAQRCodeOfTheCorrectSpecifiedSize(int size)
        {
            var identifier = Guid.NewGuid();
            var linkFormatter = Substitute.For<IQRCodeLinkFormatter>();
            linkFormatter.FormatUrl("baseUrl", identifier).Returns("a/formatted/link/");
            var generator = new QRCodeGenerator(linkFormatter);

            var qrCodeImage = generator.Generate("baseUrl", identifier, size);
            
            Assert.That(qrCodeImage.Width, Is.EqualTo(size));
            Assert.That(qrCodeImage.Height, Is.EqualTo(size));
        }

        [Test]
        public void Generate_CreatesAQRCodeWithTheCorrectUrlEncodedInsideIt()
        {
            var identifier = Guid.NewGuid();
            var linkFormatter = Substitute.For<IQRCodeLinkFormatter>();
            linkFormatter.FormatUrl("baseUrl", identifier).Returns("a/formatted/link/");
            var generator = new QRCodeGenerator(linkFormatter);

            var qrCodeImage = generator.Generate("baseUrl", identifier, 100);

            var source = new BitmapLuminanceSource(qrCodeImage);
            var reader = new BarcodeReader();
            var result = reader.Decode(source);
            Assert.That(result.Text, Is.EqualTo("a/formatted/link/"));
        }
    }
}
