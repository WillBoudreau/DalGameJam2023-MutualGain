using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TradeState
{
    offer,
    settlement
}

public class TradeManager : MonoBehaviour
{
    public string importerName;
    GameObject importer;

    public Card[,] cards; // dim 0 shall represent players, dim 1 shall represent their stock.
    public bool[] aceCheck;
    public bool[] kingCheck;
    public bool[] jokerCheck;

    // increment when turn increase
    public int index;
    public TradeState state;

    public int offerIndex;
    public int reqIndex;
    public bool agreement; // did the other player agree to trade?
    public int target; // target of the trade

    void Start()
    {
        // Get the importer.
        importer = GameObject.Find(importerName);
        TradeExporter te = importer.GetComponent<TradeExporter>();

        // Get stuff from the importer.
        cards = te.cards;
        aceCheck = te.aceCheck;
        kingCheck = te.kingCheck;

        // check for joker
    }

    // name not set in stone, this is run when a card is selected.
    void OnDecide()
    {
        // if the trade was awaiting acceptance and was accepted, do things.
        if (state == TradeState.settlement && agreement) { OnAccept(); }

        // Check for ace and settlement, increment if settlement but not ace.
        if (state == TradeState.settlement && !aceCheck[index]) { index++; aceCheck[index] = false; }

        // manage states
        TradeState ts = state;
        if (kingCheck[index]) { OnAccept(); index++; } // if player has king, skip their settlement state and move on.
        else { if (ts == TradeState.offer) { state = TradeState.settlement; } }
        if (ts == TradeState.settlement) { state = TradeState.offer; OnNewTurn(); }
    }

    void OnAccept()
    {
        // get the cards
        Card[,] tempCards = cards;
        Card offer = tempCards[index, offerIndex];
        Card req = tempCards[target, reqIndex];

        // swap the cards
        tempCards[index, offerIndex] = req;
        tempCards[target, reqIndex] = offer;

        cards = tempCards;
    }

    void OnNewTurn()
    {
        Debug.Log("This isn't implemented!!!");
        // this is mostly going to be UI stuff, so I'm postponing this.
    }
}
