using Granta.MaterialsWall.Models;

namespace Granta.MaterialsWall
{
    public sealed class CardToWallCardConverter
    {
        public WallCard ToWallCard(Card card)
        {
            return new WallCard
            {
                Identifier = card.Identifier,
                Name = card.Name,
                Path = card.Path
            };
        }
    }
}