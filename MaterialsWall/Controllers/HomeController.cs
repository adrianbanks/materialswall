using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Granta.MaterialsWall.DataAccess;
using Granta.MaterialsWall.Models;

namespace Granta.MaterialsWall.Controllers
{
    public sealed class HomeController : Controller
    {
        private readonly ICardRepository cardRepository;
        private readonly IAppSettingsConfiguredPaginator<Card> paginator;
        private readonly CardToWallCardConverter cardConverter;

        public HomeController(ICardRepository cardRepository, IAppSettingsConfiguredPaginator<Card> paginator, CardToWallCardConverter cardConverter)
        {
            if (cardRepository == null)
            {
                throw new ArgumentNullException("cardRepository");
            }

            if (paginator == null)
            {
                throw new ArgumentNullException("paginator");
            }

            if (cardConverter == null)
            {
                throw new ArgumentNullException("cardConverter");
            }
            
            this.cardRepository = cardRepository;
            this.paginator = paginator;
            this.cardConverter = cardConverter;
        }

        public ActionResult Index()
        {
            var cards = GetCardsOnPage(1);
            return View(cards);
        }

        public ActionResult Page(int p = 1)
        {
            var cards = GetCardsOnPage(p);
            var model = new PageOfCardsModel {PageNumber = p, Cards = cards};
            return View(model);
        }

        private IEnumerable<WallCard> GetCardsOnPage(int pageNumber)
        {
            var cards = cardRepository.GetCards().ToList();
            var pagedCards = paginator.GetPage(pageNumber, cards);
            return pagedCards.Select(cardConverter.ToWallCard);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}