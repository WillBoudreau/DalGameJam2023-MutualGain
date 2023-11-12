using UnityEngine;

namespace Assets.Scripts
{
    internal class TradeLog
    {
        public GameObject?[,] tempCards;
        public GameObject? offer;
        public GameObject? req;
        public int index;
        public int offerIndex;
        public int target;
        public int reqIndex;

        public TradeLog(GameObject?[,] tempCards, GameObject? offer, GameObject? req, int index, int offerIndex, int target, int reqIndex)
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
