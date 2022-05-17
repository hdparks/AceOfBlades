using System.Collections.Generic;
using Xunit;
using AceOfBlades.Components;
using AceOfBlades.Hands.Parsers;


namespace AceOfBlades.Tests.Hands.Parsers{
    public class TestRoyalParser{
        [Fact]
        public void ShouldReturnAllRoyals(){
            //  Given a hand with royals
            var cards = new List<Card>{
                new Card{Rank = Rank.Ten,   Suit = Suit.Spades},
                new Card{Rank = Rank.Jack,  Suit = Suit.Spades},
                new Card{Rank = Rank.Queen, Suit = Suit.Spades},
                new Card{Rank = Rank.King,  Suit = Suit.Spades},
                new Card{Rank = Rank.Ace,   Suit = Suit.Spades},
            };

            //  When RoyalParser runs
            var parser = new RoyalParser();
            var royals = parser.parse(cards);

            //  Then all cards should be included in output
            
            Assert.All(cards, card => royals.Contains(card));
        }

        [Fact]
        public void ShouldNotReturnNonRoyals(){
            //  Given a hand with non-royals
            var cards = new List<Card>{
                new Card{Rank = Rank.Two,   Suit = Suit.Spades},
                new Card{Rank = Rank.Three,  Suit = Suit.Spades},
                new Card{Rank = Rank.Four, Suit = Suit.Spades},
                new Card{Rank = Rank.Five,  Suit = Suit.Spades},
                new Card{Rank = Rank.Six,   Suit = Suit.Spades},
                new Card{Rank = Rank.Seven,   Suit = Suit.Spades},
                new Card{Rank = Rank.Eight,  Suit = Suit.Spades},
                new Card{Rank = Rank.Nine, Suit = Suit.Spades}
            };

            //  When RoyalParser runs
            var parser = new RoyalParser();
            var royals = parser.parse(cards);

            //  Then there should be nothing in the output
            Assert.Empty(royals);
        }
    }

}