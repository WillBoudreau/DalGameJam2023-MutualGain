using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static UnityEngine.GraphicsBuffer;

namespace Assets.Scripts
{
    internal class TradeLog
    {
        Card?[,] tempCards;
        Card? offer;
        Card? req;

        public TradeLog(Card?[,] tempCards, Card? offer, Card? req)
        {
            this.tempCards = tempCards;
            this.offer = offer;
            this.req = req;
        }
    }
}
