using System.Collections.Generic;
using Xunit;
using AceOfBlades.Components;
using AceOfBlades.Hands.Parsers;
using System.Linq;


namespace AceOfBlades.Tests.Hands.Parsers{
    public class TestOfAKindParser{
        [Fact]
        public void ShouldReturnOfAKind(){
            //  Given two of a kind
            var cards = new List<Card>{
                new Card{Rank = Rank.Ace, Suit = Suit.Spades},
                new Card{Rank = Rank.Ace, Suit = Suit.Hearts},
            };

            //  When OfAKindParser runs
            var parser = new OfAKindParser();
            var ofAKinds = parser.parse(cards);

            //  Then one of-a-kind set should be returned
            Assert.Single(ofAKinds);

            //  And it includes all cards
            Assert.All(cards, card => ofAKinds[0].Contains(card));
        }

        [Fact]
        public void ShouldReturnNothingIfNoOfAKind(){
            //  Given no of-a-kind match
            var cards = new List<Card>{
                new Card{Rank = Rank.Two, Suit = Suit.Spades},
                new Card{Rank = Rank.Three, Suit = Suit.Hearts},
            };

            //  When OfAKindParser runs
            var parser = new OfAKindParser();
            var ofAKinds = parser.parse(cards);

            //  Then no set should be returned
            Assert.Empty(ofAKinds);
        }

        [Fact]
        public void ShouldReturnAllOfAKinds(){
            //  Given multiple of-a-kind matches
            var cards = new List<Card>{
                new Card{Rank = Rank.Two, Suit = Suit.Hearts},
                new Card{Rank = Rank.Two, Suit = Suit.Spades},
                new Card{Rank = Rank.Two, Suit = Suit.Clubs},
                new Card{Rank = Rank.Three, Suit = Suit.Hearts},
                new Card{Rank = Rank.Three, Suit = Suit.Spades}
            };

            //  When OfAKindParser runs
            var parser = new OfAKindParser();
            var groups = parser.parse(cards);

            //  Then all valid sets should be returned
            Assert.Equal(2, groups.Count);

            //  and all the expected cards are included in some group
            Assert.All(cards,card => groups.Any(group => group.Contains(card)));
        }
    }
}