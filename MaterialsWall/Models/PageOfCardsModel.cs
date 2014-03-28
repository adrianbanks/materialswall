using System.Collections.Generic;

namespace Granta.MaterialsWall.Models
{
    public sealed class PageOfCardsModel
    {
        public int PageNumber{get;set;}
        public IEnumerable<WallCard> Cards{get;set;}
    }
}
