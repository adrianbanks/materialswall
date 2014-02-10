using System;
using System.Drawing;
using Granta.MaterialsWall.Images;
using NUnit.Framework;

namespace Granta.MaterialsWall.Tests.Images
{
    [TestFixture]
    public sealed class ThumbnailGeneratorTests
    {
        [Test]
        public void Scale_ThrowsAnException_WhenTheSuppliedImageIsNull()
        {
            var generator = new ThumbnailGenerator();
            Assert.Throws<ArgumentNullException>(() => generator.Scale(null, 1.0));
        }

        [Test]
        public void Scale_CorrectlyScalesTheOriginalImage()
        {
            var image = new Bitmap(100, 150);
            var generator = new ThumbnailGenerator();
            var newImage = generator.Scale(image, 0.5);
            Assert.That(newImage.Width, Is.EqualTo(50));
            Assert.That(newImage.Height, Is.EqualTo(75));
        }
    }
}
