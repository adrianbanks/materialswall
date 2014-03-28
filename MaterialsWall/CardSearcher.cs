using System.Collections.Generic;
using System.Linq;
using Granta.MaterialsWall.Models;

namespace Granta.MaterialsWall
{
    public interface ICardSearcher
    {
        IEnumerable<Card> FilterCards(IEnumerable<Card> cards, string searchTerm);
    }

    public sealed class CardSearcher : ICardSearcher
    {
        public IEnumerable<Card> FilterCards(IEnumerable<Card> cards, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return cards;
            }

            return cards.Where(c => NamePartStartsWith(c.Name.ToLower(), searchTerm.ToLower()));
        }

        private bool NamePartStartsWith(string name, string toMatch)
        {
            var nameParts = name.ToLower().Split(new [] {' ', '-'});
            return nameParts.Any(p => p.StartsWith(toMatch));
        }
    }
}
