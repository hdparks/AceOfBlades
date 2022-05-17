using System.Collections.Generic;
using Xunit;
using AceOfBlades.Components;
using AceOfBlades.Hands.Parsers;

namespace AceOfBlades.Tests.Hands.Parsers{
    public class TestStraightParser
    {
        [Fact]
        public void ShouldReturnAValidStraight()
        {
            //  Given a valid straight
            var cards = new List<Card> {
                new Card{Rank = Rank.Two,Suit = Suit.Spades},
                new Card{Rank = Rank.Three,Suit = Suit.Diamonds },
                new Card{Rank = Rank.Four, Suit = Suit.Spades},
                new Card{Rank = Rank.Five, Suit = Suit.Clubs},
                new Card{Rank = Rank.Six, Suit = Suit.Hearts}
            };

            // When StraightParser runs
            var parser = new StraightParser();
            var striaghts = parser.parse(cards);

            // Then it should have found one straight
            Assert.Single(striaghts);

            // and the result should include each card
            foreach(var card in cards){
                Assert.Contains(card, striaghts[0]);
            }
        }

        [Fact]
        public void ShouldReturnNothingIfNoValidStraight(){
            //  Given a hand without a valid straight
            var cards = new List<Card>{};

            // When StraightParser runs
            var parser = new StraightParser();
            var straights = parser.parse(cards);

            // Then it should have found no straights
            Assert.Empty(straights);

        }

        [Fact]
        public void ShouldIncludeAllCardsInPermutableStraight(){
            //  Given a hand with multiple valid straights
            var two = new Card{Rank = Rank.Two, Suit=Suit.Spades};
            var three = new Card{Rank= Rank.Three, Suit=Suit.Spades};
            var four = new Card{Rank= Rank.Four, Suit=Suit.Spades};
            var five = new Card{Rank= Rank.Five, Suit=Suit.Spades};
            var sixOfSpades = new Card{Rank=Rank.Six, Suit=Suit.Spades};
            var sixOfHearts = new Card{Rank=Rank.Six, Suit=Suit.Hearts};

            var cards = new List<Card>{two, three, four, five, sixOfHearts, sixOfSpades};

            //  When StraightParser runs
            var parser = new StraightParser();
            var straights = parser.parse(cards);

            // Then there should be a single straight
            Assert.Single(straights);

            // and the result should include each card
            foreach(var card in cards){
                Assert.Contains(card, straights[0]);
            }
        }

        [Fact]
        public void ShouldReturnDistinctStraights(){
            //  Given a hand with multiple valid straights
            var cards = new List<Card>{
                //  Straight 1
                new Card {Rank=Rank.Two, Suit=Suit.Spades},
                new Card {Rank=Rank.Three, Suit=Suit.Spades},
                new Card {Rank=Rank.Four, Suit=Suit.Spades},
                new Card {Rank=Rank.Five, Suit=Suit.Spades},
                new Card {Rank=Rank.Six, Suit=Suit.Spades},
                //  Straight 2
                new Card {Rank=Rank.Ten, Suit=Suit.Spades},
                new Card {Rank=Rank.Jack, Suit=Suit.Spades},
                new Card {Rank=Rank.Queen, Suit=Suit.Spades},
                new Card {Rank=Rank.King, Suit=Suit.Spades},
                new Card {Rank=Rank.Ace, Suit=Suit.Spades},
            };

            //   When StraightParser runs
            var parser = new StraightParser();
            var straights = parser.parse(cards);

            //  Then parser should return two distinct straights
            Assert.Equal(2, straights.Count);
        }
    }
}


