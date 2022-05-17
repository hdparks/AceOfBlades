using AceOfBlades.Components;

namespace AceOfBlades.Hands.Parsers{
    public class StraightParser{

        int STRAIGHT_LENGTH = 5;

        public List<HashSet<Card>> parse(List<Card> cards){
            var straights = new List<HashSet<Card>>{};

            //  Initialize Rank Dictionary
            var rankDictionary = new Dictionary<Rank, List<Card>>{};
            foreach(Rank rank in Enum.GetValues(typeof(Rank))){
                rankDictionary[rank] = new List<Card>{};
            }

            //  Fill Rank Dictionary
            foreach(var card in cards){
                rankDictionary[card.Rank].Add(card);
            }
            
            //  Extract straights
            var currentStraight = new HashSet<Card>{};
            var currentStraightLength = 0;
            foreach(Rank rank in Enum.GetValues(typeof(Rank))){
                //  Get the cards in that rank
                var rankCards = rankDictionary[rank];
                if (rankCards.Count() > 0){
                    // Add cards to current straight
                    foreach(var card in rankCards){
                        currentStraight.Add(card);
                    }
                    currentStraightLength += 1;
                
                } else {
                    //  If straight is valid, add to list
                    if (currentStraightLength >= STRAIGHT_LENGTH){
                        straights.Add(currentStraight);
                    }

                    //  Start building new straight
                    currentStraight = new HashSet<Card>{};
                    currentStraightLength = 0;
                }
            }

            //  Finally, if straight is valid, add to list
            if (currentStraightLength >= STRAIGHT_LENGTH){
                straights.Add(currentStraight);
            }

            return straights;
        }
    }
}