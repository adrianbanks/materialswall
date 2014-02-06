using System;
using Granta.MaterialsWall.DataAccess;
using Granta.MaterialsWall.Models;
using NUnit.Framework;

namespace Granta.MaterialsWall.Tests.DataAccess
{
    [TestFixture]
    public sealed class CardFactoryTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        [TestCase("something that won't parse as a guid")]
        public void Create_ReturnsNull_WhenTheIdentifierIsInvalid(string identifier)
        {
            var factory = new CardFactory();
            var card = factory.Create(identifier, "name", null, null, null, null, null, null, null);
            Assert.IsNull(card);
        }

        [Test]
        public void Create_ReturnsACard_WhenTheIdentifierIsValid()
        {
            var factory = new CardFactory();
            var card = factory.Create("b616b280-9d67-41e4-97b7-8200f387964f", "name", null, null, null, null, null, null, null);
            Assert.IsNotNull(card);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void Create_ReturnsNull_WhenTheNameIsInvalid(string name)
        {
            var factory = new CardFactory();
            var card = factory.Create("b616b280-9d67-41e4-97b7-8200f387964f", name, null, null, null, null, null, null, null);
            Assert.IsNull(card);
        }

        [Test]
        public void Create_ReturnsACard_WhenTheNameIsValid()
        {
            var factory = new CardFactory();
            var card = factory.Create("b616b280-9d67-41e4-97b7-8200f387964f", "name", null, null, null, null, null, null, null);
            Assert.IsNotNull(card);
        }

        [Test]
        public void Create_ReturnsACard_WithTheCorrectValues()
        {
            var factory = new CardFactory();
            var links = new[] {new Link("url", "text")};
            
            var card = factory.Create("b616b280-9d67-41e4-97b7-8200f387964f", "name", "123", "description", "typical uses", "source", "sample", "path", links);
            
            Assert.That(card.Identifier, Is.EqualTo(Guid.Parse("b616b280-9d67-41e4-97b7-8200f387964f")));
            Assert.That(card.Name, Is.EqualTo("name"));
            Assert.That(card.Id, Is.EqualTo("123"));
            Assert.That(card.Description, Is.EqualTo("description"));
            Assert.That(card.TypicalUses, Is.EqualTo("typical uses"));
            Assert.That(card.Source, Is.EqualTo("source"));
            Assert.That(card.Sample, Is.EqualTo("sample"));
            Assert.That(card.Path, Is.EqualTo("path"));
            Assert.That(card.Links, Is.EqualTo(links));
        }
    }
}
