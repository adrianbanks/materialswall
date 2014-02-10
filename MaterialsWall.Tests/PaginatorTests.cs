using System;
using System.Linq;
using NUnit.Framework;

namespace Granta.MaterialsWall.Tests
{
    [TestFixture]
    public sealed class PaginatorTests
    {
        [TestCase(-1)]
        [TestCase(0)]
        public void GetPage_ThrowsAnException_WhenTheSuppliedPageSizeIsInvalid(int pageSize)
        {
            var paginator = new Paginator<int>();
            Assert.Throws<ArgumentException>(() => paginator.GetPage(pageSize, 1, new[] {1}));
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void GetPage_ThrowsAnException_WhenTheSuppliedPageNumberIsInvalid(int pageNumber)
        {
            var paginator = new Paginator<int>();
            Assert.Throws<ArgumentException>(() => paginator.GetPage(1, pageNumber, new[] {1}));
        }

        [Test]
        public void GetPage_ThrowsAnException_WhenTheSuppliedItemsAreNull()
        {
            var paginator = new Paginator<int>();
            Assert.Throws<ArgumentNullException>(() => paginator.GetPage(1, 1, null));
        }

        [Test]
        public void GetPage_ReturnsAnEmptyEnumerable_WhenTheSuppliedItemsAreNull()
        {
            var paginator = new Paginator<int>();
            var pagedItems = paginator.GetPage(1, 1, Enumerable.Empty<int>());
            Assert.IsEmpty(pagedItems);
        }

        [Test]
        public void GetPage_ReturnsAnEmptyEnumerable_WhenTheNumberOfRequestedItemsIsBeyondTheEndOfTheSequence()
        {
            var paginator = new Paginator<int>();
            var pagedItems = paginator.GetPage(3, 2, new[] {1, 2, 3});
            Assert.IsEmpty(pagedItems);
        }

        [Test]
        public void GetPage_ReturnsTheCorrectItems_WhenNotOnTheLastPage()
        {
            var paginator = new Paginator<int>();
            var pagedItems = paginator.GetPage(2, 2, new[] {1, 2, 3, 4, 5});
            Assert.That(pagedItems, Is.EqualTo(new[] {3, 4}));
        }

        [Test]
        public void GetPage_ReturnsTheRemainingItems_WhenOnTheLastPage()
        {
            var paginator = new Paginator<int>();
            var pagedItems = paginator.GetPage(3, 2, new[] {1, 2, 3, 4, 5});
            Assert.That(pagedItems, Is.EqualTo(new[] {4, 5}));
        }
    }
}
