using System;
using Granta.MaterialsWall.Models;
using NUnit.Framework;

namespace Granta.MaterialsWall.Tests.Models
{
    [TestFixture]
    public sealed class LinkTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void Ctor_ThrowsAnException_WhenTheUrlIsInvalid(string url)
        {
            Assert.Throws<ArgumentNullException>(() => new Link(url, "text"));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void TextGetsSetToTheUrl_WhenTheTextIsNullOrWhiteSpace(string text)
        {
            var link = new Link("url", text);
            Assert.That(link.Text, Is.EqualTo("url"));
        }
    }
}
