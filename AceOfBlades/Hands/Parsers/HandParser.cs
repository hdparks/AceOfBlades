using AceOfBlades.Components;

namespace AceOfBlades.Hands.Parsers{

    public class Hand {
        public HandValue Value;
        public Rank? High;
        public List<Card> Cards;
    }

    public class HandParser{

        int HAND_SIZE = 5;

        StraightParser _straightParser;
        RoyalParser _royalParser;
        FlushParser _flushParser;
        OfAKindParser _ofAKindParser;

        HashSet<Card> royals;
        List<HashSet<Card>> straights;
        List<HashSet<Card>> flushes;
        List<HashSet<Card>> ofAKinds;

        public HandParser(
            StraightParser straightParser, 
            RoyalParser royalParser,
            FlushParser flushParser,
            OfAKindParser ofAKindParser){
            _flushParser = flushParser;
            _royalParser = royalParser;
            _straightParser = straightParser;
            _ofAKindParser = ofAKindParser;
        }

        public Hand parse(List<Card> cards){

            royals = _royalParser.parse(cards);
            straights = _straightParser.parse(cards);
            flushes = _flushParser.parse(cards);
            ofAKinds = _ofAKindParser.parse(cards);
            
            foreach(var getHand in new []{
                GetRoyalFlush, 
                GetStraightFlush,
                GetFourOfAKind,
                GetFullHouse,
                GetFlush,
                GetStraight,
                GetThreeOfAKind,
                GetTwoPairs,
                GetPair,
                GetHighCard
                }){
                var hand = getHand(cards);
                if (hand != null) { return hand; }
            }
            
            //  If unable to parse hand, throw exception
            throw new Exception("Unable to parse hand");
        }

        public Hand? GetRoyalFlush(List<Card> cards){
            //  Royal Flush = Royal + Straight + Flush
            foreach(var straight in straights){
                foreach(var flush in flushes){
                   var intersection = straight.Intersect(flush).Intersect(royals);
                   if (intersection.Count() >= HAND_SIZE){
                       return new Hand {
                           Value = HandValue.RoyalFlush,
                           Cards = intersection.ToList(),
                           High = null
                       };
                   }
                }
            }
            return null;
        }

        public Hand? GetStraightFlush(List<Card> cards){
            //  Straight Flush = Straight + Flush
            foreach(var straight in straights){
                foreach(var flush in flushes){
                    var intersection = straight.Intersect(flush);
                    if (intersection.Count() >= HAND_SIZE){
                        return new Hand {
                            Value = HandValue.StraightFlush,
                            Cards = intersection.ToList(),
                            High = null
                        };
                    }
                }
            }
            return null;
        }

        public Hand? GetFourOfAKind(List<Card> cards){
            //  Four of a kind
            foreach(var ofAKind in ofAKinds){
                if (ofAKind.Count >= 4){
                    //  Get the high card
                    var others = cards.Where(c => !ofAKind.Contains(c));
                    var highCard = others.OrderByDescending(c => c.Rank).FirstOrDefault();

                    return new Hand {
                        Value = HandValue.FourOfAKind,
                        Cards = ofAKind.Append(highCard).ToList(),
                        High = highCard.Rank
                    };
                }
            }
            return null;
        }

        public Hand? GetFullHouse(List<Card> cards){
            var threeOfAKinds = ofAKinds.Where(o => o.Count >= 3).OrderByDescending(o => o.First().Rank);
            var twoOfAKinds = ofAKinds.Where(o => o.Count >= 2).OrderByDescending(o => o.First().Rank);

            foreach(var three in threeOfAKinds){
                foreach(var two in twoOfAKinds){
                    if (three == two) continue;

                    return new Hand{
                        Value = HandValue.FullHouse,
                        Cards = three.Take(3).Concat(two.Take(2)).ToList(),
                        High = null
                    };
                }
            }
            return null;
        }

        public Hand? GetFlush(List<Card> cards){
            var flush = flushes.OrderByDescending(f => f.OrderByDescending(c => c.Rank)).FirstOrDefault();
            if (flush != null){
                return new Hand{
                    Value = HandValue.Flush,
                    Cards = flush.ToList(),
                    High = flush.OrderByDescending(c => c.Rank).First().Rank
                };
            }
            return null;
        }

        public Hand? GetStraight(List<Card> cards){
            var straight = straights.OrderByDescending(f => f.OrderByDescending(c => c.Rank)).FirstOrDefault();
            if (straight != null){
                return new Hand{
                    Value = HandValue.Straight,
                    Cards = straight.ToList(),
                    High = straight.OrderByDescending(c => c.Rank).First().Rank
                };
            }
            return null;
        }

        public Hand? GetThreeOfAKind(List<Card> cards){
            var threeOfAKind = ofAKinds.Where(o => o.Count >= 3).OrderByDescending(o => o.First().Rank).FirstOrDefault();
            if (threeOfAKind != null){
                var others = cards.Where(c => !threeOfAKind.Contains(c)).OrderByDescending(c => c.Rank).Take(2);
                var highCard = others.First();
                return new Hand{
                    Value = HandValue.ThreeOfAKind,
                    Cards = threeOfAKind.Concat(others).ToList(),
                    High = highCard.Rank
                };
            }
            return null;
        }

        public Hand? GetTwoPairs(List<Card> cards){
            var pairs = ofAKinds.Where(o => o.Count >= 2).OrderByDescending(o => o.First().Rank).Take(2);
            if (pairs.Count() >= 2){
                var handCards = pairs.SelectMany(p => p);
                var highCard = cards.Where(c => !handCards.Contains(c)).First();
                return new Hand{
                    Value = HandValue.TwoPairs,
                    Cards = handCards.Append(highCard).ToList(),
                    High = highCard.Rank
                };
            }
            return null;
        }

        public Hand? GetPair(List<Card> cards){
            var pair = ofAKinds.Where(o => o.Count >= 2).OrderByDescending(o => o.First().Rank).FirstOrDefault();
            if (pair != null){
                var others = cards.Where(c => !pair.Contains(c)).OrderByDescending(c => c.Rank).Take(3);
                var highCard = others.First();
                return new Hand{
                    Value = HandValue.Pair,
                    Cards = pair.Concat(others).ToList(),
                    High = highCard.Rank
                };
            }
            return null;
        }

        public Hand? GetHighCard(List<Card> cards){
            var handCards = cards.OrderByDescending(c => c.Rank).Take(5).ToList();
            return new Hand{
                Value = HandValue.HighCard,
                Cards = handCards,
                High = handCards.First().Rank
            };
        }
    }
}