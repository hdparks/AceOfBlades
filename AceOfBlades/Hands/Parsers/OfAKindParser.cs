using AceOfBlades.Components;

namespace AceOfBlades.Hands.Parsers{
    public class OfAKindParser{
        int MIN_OF_A_KIND = 2;

        public List<HashSet<Card>> parse(List<Card> cards){
            var rankDictionary = new Dictionary<Rank, HashSet<Card>>{};
            foreach(var card in cards){
                rankDictionary.TryGetValue(card.Rank, out var ofAKind);
                if (ofAKind == null){
                    ofAKind = new HashSet<Card>{};
                    rankDictionary[card.Rank] = ofAKind;
                }
                ofAKind.Add(card);
            }

            var ofAKinds = rankDictionary.Values.Where(ofAKind => ofAKind.Count >= MIN_OF_A_KIND).ToList();
            return ofAKinds;
        }
    }
}