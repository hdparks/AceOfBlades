using AceOfBlades.Components;

namespace AceOfBlades.Hands.Parsers{
    public class FlushParser{

        int FLUSH_LENGTH = 5;

        public List<HashSet<Card>> parse(List<Card> cards){
            var flushes = new List<HashSet<Card>>{};
            var suitDictionary = new Dictionary<Suit, HashSet<Card>>{};

            //  Initialize Dictionary
            foreach(Suit suit in Enum.GetValues(typeof(Suit))){
                suitDictionary[suit] = new HashSet<Card>{};
            }

            //  Fill Dictionary
            foreach(Card card in cards){
                suitDictionary[card.Suit].Add(card);
            }

            //  Take valid flushes
            flushes = suitDictionary.Values.Where(flush => flush.Count >= FLUSH_LENGTH).ToList();

            return flushes;
        }
    }
}