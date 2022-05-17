using System.Collections.Generic;
using Xunit;
using AceOfBlades.Components;
using AceOfBlades.Hands.Parsers;


namespace AceOfBlades.Tests.Hands.Parsers{
    public class TestFlushParser{
        [Fact]
        public void ShouldReturnAValidFlush(){
            //  Given a valid flush
            var cards = new List<Card>{
                new Card{Rank = Rank.Two,   Suit = Suit.Spades},
                new Card{Rank = Rank.Three, Suit = Suit.Spades},
                new Card{Rank = Rank.Four,  Suit = Suit.Spades},
                new Card{Rank = Rank.Five,  Suit = Suit.Spades},
                new Card{Rank = Rank.Six,   Suit = Suit.Spades}
            };

            //  When FlushParser runs
            var parser = new FlushParser();
            var flushes = parser.parse(cards);

            //  Then it should have found one flush
            Assert.Single(flushes);
            // and the result should include each card
            foreach(var card in cards){
                Assert.Contains(card, flushes[0]);
            }
        }

        [Fact]
        public void ShouldReturnNothingIfNoValidFlush(){
            //  Given a hand with no flush
            var cards = new List<Card>{};

            //  When FlushParser runs
            var parser = new FlushParser();
            var flushes = parser.parse(cards);

            //  Then it should have found no flushes
            Assert.Empty(flushes);
        }

        [Fact]
        public void ShouldReturnDistinctFlushes(){
            //  Given a hand with multiple flushes
            var cards = new List<Card>{
                //  Flush 1
                new Card{Rank = Rank.Two,   Suit = Suit.Spades},
                new Card{Rank = Rank.Three, Suit = Suit.Spades},
                new Card{Rank = Rank.Four,  Suit = Suit.Spades},
                new Card{Rank = Rank.Five,  Suit = Suit.Spades},
                new Card{Rank = Rank.Six,   Suit = Suit.Spades},
                //  Flush 2
                new Card{Rank = Rank.Two,   Suit = Suit.Hearts},
                new Card{Rank = Rank.Three, Suit = Suit.Hearts},
                new Card{Rank = Rank.Four,  Suit = Suit.Hearts},
                new Card{Rank = Rank.Five,  Suit = Suit.Hearts},
                new Card{Rank = Rank.Six,   Suit = Suit.Hearts}
            };

            //  When FlushParser runs
            var parser = new FlushParser();
            var flushes = parser.parse(cards);

            //  Then there should be two flushes
            Assert.Equal(2,flushes.Count);
        }
    }

}