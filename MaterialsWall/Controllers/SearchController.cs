using System;
using System.Linq;
using System.Web.Mvc;
using Granta.MaterialsWall.DataAccess;

namespace Granta.MaterialsWall.Controllers
{
    public class SearchController : Controller
    {
        private readonly ICardRepository cardRepository;
        private readonly ICardSearcher cardSearcher;
        private readonly CardToWallCardConverter cardConverter;

        public SearchController(ICardRepository cardRepository, ICardSearcher cardSearcher, CardToWallCardConverter cardConverter)
        {
            if (cardRepository == null)
            {
                throw new ArgumentNullException("cardRepository");
            }

            if (cardSearcher == null)
            {
                throw new ArgumentNullException("cardSearcher");
            }
            
            if (cardConverter == null)
            {
                throw new ArgumentNullException("cardConverter");
            }
            
            this.cardRepository = cardRepository;
            this.cardSearcher = cardSearcher;
            this.cardConverter = cardConverter;
        }

        public ActionResult Index(string term)
        {
            var cards = cardRepository.GetCards();
            cards = cardSearcher.FilterCards(cards, term);
            var wallCards = cards.Select(cardConverter.ToWallCard);
            return PartialView("_PageOfCards", wallCards);
        }
    }
}
