namespace AceOfBlades.Components {

    public struct Hand {
        public HandValue Value;
        public Rank? High;
        public List<Guid> Cards;
    }
    
    public enum HandValue {
        RoyalFlush,
        StraightFlush,
        FourOfAKind,
        FullHouse,
        Flush,
        Straight,
        ThreeOfAKind,
        TwoPairs,
        Pair,
        HighCard
    }
    
    
}