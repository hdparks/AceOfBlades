using System.Collections.Generic;
using Xunit;
using AceOfBlades.Components;
using AceOfBlades.Hands.Parsers;


namespace AceOfBlades.Tests.Hands.Parsers{
    public class TestHandParser{

        HandParser parser;
        public TestHandParser(){
            var straightParser = new StraightParser();
            var royalParser = new RoyalParser();
            var flushParser = new FlushParser();
            var ofAKindParser = new OfAKindParser();
            parser = new HandParser(straightParser, royalParser, flushParser, ofAKindParser);
        }

        [Fact]
        void ShouldReturnRoyalFlush(){
            // Given a valid Royal Flush
            var cards = new List<Card>{
                new Card{Rank = Rank.Ten, Suit = Suit.Spades},
                new Card{Rank = Rank.Jack, Suit = Suit.Spades},
                new Card{Rank = Rank.Queen, Suit = Suit.Spades},
                new Card{Rank = Rank.King, Suit = Suit.Spades},
                new Card{Rank = Rank.Ace, Suit = Suit.Spades}
            };
        
            //  When HandParser runs
            var royalFlush = parser.parse(cards);

            //  Then it should have returned the royal flush
            Assert.Equal(HandValue.RoyalFlush, royalFlush.Value);
        }

        [Fact]
        void ShouldReturnStraightFlush(){
            //  Given a valid straight flush
            var cards = new List<Card>{
                new Card{Rank = Rank.Two, Suit = Suit.Spades},
                new Card{Rank = Rank.Three, Suit = Suit.Spades},
                new Card{Rank = Rank.Four, Suit = Suit.Spades},
                new Card{Rank = Rank.Five, Suit = Suit.Spades},
                new Card{Rank = Rank.Six, Suit = Suit.Spades}
            };

            //  When HandParser runs
            var straightFlush = parser.parse(cards);
            
            //  Then it should have returned the stright flush
            Assert.Equal(HandValue.StraightFlush, straightFlush.Value);
        }
        
        [Fact]
        void ShouldReturnFourOfAKind(){
            //  Given a valid four-of-a-kind (Ace High)
            var cards = new List<Card>{
                new Card{Rank = Rank.Two, Suit = Suit.Spades},
                new Card{Rank = Rank.Two, Suit = Suit.Hearts},
                new Card{Rank = Rank.Two, Suit = Suit.Clubs},
                new Card{Rank = Rank.Two, Suit = Suit.Diamonds},
                new Card{Rank = Rank.Ace, Suit = Suit.Spades}
            };

            //  When HandParser runs
            var fourOfAKind = parser.parse(cards);
            
            //  Then it should have returned the stright flush
            Assert.Equal(HandValue.FourOfAKind, fourOfAKind.Value);
            Assert.Equal(Rank.Ace, fourOfAKind.High);
        }

        [Fact]
        void ShouldReturnFullHouse(){
            //  Given a valid Full House
            var cards = new List<Card>{
                new Card{Rank = Rank.Two, Suit = Suit.Spades},
                new Card{Rank = Rank.Two, Suit = Suit.Hearts},
                new Card{Rank = Rank.Two, Suit = Suit.Clubs},
                new Card{Rank = Rank.Three, Suit = Suit.Diamonds},
                new Card{Rank = Rank.Three, Suit = Suit.Spades}
            };

            //  When HandParser runs
            var fullHouse = parser.parse(cards);
            
            //  Then it should have returned the full house
            Assert.Equal(HandValue.FullHouse, fullHouse.Value);
            foreach(var card in cards){
                Assert.Contains(card, fullHouse.Cards);
            }
        }

        [Fact]
        void ShouldReturnFlush(){
            // Given a valid Flush
            var cards = new List<Card>{
                new Card{Rank = Rank.Two, Suit = Suit.Spades},
                new Card{Rank = Rank.Three, Suit = Suit.Spades},
                new Card{Rank = Rank.Seven, Suit = Suit.Spades},
                new Card{Rank = Rank.Eight, Suit = Suit.Spades},
                new Card{Rank = Rank.Jack, Suit = Suit.Spades}
            };

            // When HandParser runs
            var flush = parser.parse(cards);

            // Then it should return the flush
            Assert.Equal(HandValue.Flush, flush.Value);
            Assert.Equal(Rank.Jack, flush.High);
        }

        [Fact]
        void ShouldReturnStraight(){
            // Given a valid straight
            var cards = new List<Card>{
                new Card{Rank = Rank.Two, Suit = Suit.Spades},
                new Card{Rank = Rank.Three, Suit = Suit.Hearts},
                new Card{Rank = Rank.Four, Suit = Suit.Diamonds},
                new Card{Rank = Rank.Five, Suit = Suit.Clubs},
                new Card{Rank = Rank.Six, Suit = Suit.Spades}
            };

            // When HandParser runs
            var straight = parser.parse(cards);

            // Then it should return the straight
            Assert.Equal(HandValue.Straight, straight.Value);
            Assert.Equal(Rank.Six, straight.High);
        }

        [Fact] 
        void ShouldReturnThreeOfAKind(){
            //  Given a valid three-of-a-kind
            var cards = new List<Card>{
                new Card{Rank = Rank.Two, Suit = Suit.Spades},
                new Card{Rank = Rank.Two, Suit = Suit.Hearts},
                new Card{Rank = Rank.Two, Suit = Suit.Diamonds},
                new Card{Rank = Rank.Five, Suit = Suit.Clubs},
                new Card{Rank = Rank.Ace, Suit = Suit.Spades}
            };

            // When HandParser runs
            var threeOfAKind = parser.parse(cards);

            // Then it should return the two three of a kind
            Assert.Equal(HandValue.ThreeOfAKind, threeOfAKind.Value);
            Assert.Equal(Rank.Ace, threeOfAKind.High);
        }

        [Fact] 
        void ShouldReturnTwoPair(){
            //  Given a valid two-pair
            var cards = new List<Card>{
                new Card{Rank = Rank.Two, Suit = Suit.Spades},
                new Card{Rank = Rank.Two, Suit = Suit.Hearts},
                new Card{Rank = Rank.Three, Suit = Suit.Diamonds},
                new Card{Rank = Rank.Three, Suit = Suit.Clubs},
                new Card{Rank = Rank.Ace, Suit = Suit.Spades}
            };

            // When HandParser runs
            var twoPair = parser.parse(cards);

            // Then it should return the two pairs
            Assert.Equal(HandValue.TwoPairs, twoPair.Value);
            Assert.Equal(Rank.Ace, twoPair.High);
        }

        [Fact]
        void ShouldReturnPair(){
           //  Given a valid two-pair
            var cards = new List<Card>{
                new Card{Rank = Rank.Two, Suit = Suit.Spades},
                new Card{Rank = Rank.Two, Suit = Suit.Hearts},
                new Card{Rank = Rank.Three, Suit = Suit.Diamonds},
                new Card{Rank = Rank.Five, Suit = Suit.Clubs},
                new Card{Rank = Rank.Ace, Suit = Suit.Spades}
            };

            // When HandParser runs
            var pair = parser.parse(cards);

            // Then it should return the two pairs
            Assert.Equal(HandValue.Pair, pair.Value);
            Assert.Equal(Rank.Ace, pair.High);
        }

        [Fact]
        void ShouldReturnHighCard(){
            //  Given a hand with nothing
            var cards = new List<Card>{
                new Card{Rank = Rank.Two, Suit = Suit.Spades},
                new Card{Rank = Rank.Four, Suit = Suit.Hearts},
                new Card{Rank = Rank.Six, Suit = Suit.Diamonds},
                new Card{Rank = Rank.Eight, Suit = Suit.Clubs},
                new Card{Rank = Rank.Ten, Suit = Suit.Spades}
            };

            //  When the HandParser runs
            var highCard = parser.parse(cards);

            //  Then it should return the high card
            Assert.Equal(HandValue.HighCard, highCard.Value);
            Assert.Equal(Rank.Ten, highCard.High);
        }
    }
}