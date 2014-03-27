using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Granta.MaterialsWall.DataAccess;
using Granta.MaterialsWall.Models;

namespace Granta.MaterialsWall.Controllers
{
    public sealed class SearchController : ApiController
    {
        private readonly ICardRepository cardRepository;

        public SearchController(ICardRepository cardRepository)
        {
            if (cardRepository == null)
            {
                throw new ArgumentNullException("cardRepository");
            }
            
            this.cardRepository = cardRepository;
        }

        public IEnumerable<SearchResult> Get(string term)
        {
            var cards = cardRepository.GetCards().Where(c => c.Name.Contains(term));
            return cards.Select(ToSearchResult);
        }

        private SearchResult ToSearchResult(Card card)
        {
            return new SearchResult
            {
                Identifier = card.Identifier,
                Name = card.Name,
                Path = card.Path
            };
        }
    }
}
