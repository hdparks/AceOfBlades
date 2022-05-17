using AceOfBlades.Hands.Parsers;
using AceOfBlades.Components;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var cards = new List<Card>{
    new Card{Rank = Rank.Two, Suit = Suit.Spades},
    new Card{Rank = Rank.Three, Suit = Suit.Spades},
    new Card{Rank = Rank.Four, Suit = Suit.Spades},
    new Card{Rank = Rank.Five, Suit = Suit.Spades},
    new Card{Rank = Rank.Six, Suit = Suit.Spades}
};

var straightParser = new StraightParser();
var royalParser = new RoyalParser();
var flushParser = new FlushParser();
var ofAKindParser = new OfAKindParser();
var parser = new HandParser(straightParser, royalParser, flushParser, ofAKindParser);

var hand = parser.parse(cards);
Console.WriteLine(hand.Value);