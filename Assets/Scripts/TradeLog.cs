namespace Assets.Scripts
{
    internal class TradeLog
    {
        public Card?[,] tempCards;
        public Card? offer;
        public Card? req;
        public int index;
        public int offerIndex;
        public int target;
        public int reqIndex;

        public TradeLog(Card?[,] tempCards, Card? offer, Card? req, int index, int offerIndex, int target, int reqIndex)
        {
            this.tempCards = tempCards;
            this.offer = offer;
            this.req = req;
            this.index = index;
            this.offerIndex = offerIndex;
            this.target = target;
            this.reqIndex = reqIndex;
        }
    }
}
