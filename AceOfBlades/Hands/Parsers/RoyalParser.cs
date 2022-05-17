using AceOfBlades.Components;

namespace AceOfBlades.Hands.Parsers{
    public class RoyalParser{
        Rank[] ROYALS = new Rank[]{
            Rank.Ten,
            Rank.Jack,
            Rank.Queen,
            Rank.King,
            Rank.Ace
        };

        public HashSet<Card> parse(List<Card> cards){
            var royals = new HashSet<Card>(cards.Where(card => ROYALS.Contains(card.Rank)));
            return royals;
        }
    }

}