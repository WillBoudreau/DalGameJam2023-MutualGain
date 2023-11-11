using Assets.Scripts;
using System;
using UnityEngine;

public enum TradeState
{
    offer,
    settlement,
    counterOffer, // Idk if/when these'll be used, but just in case.
    counterSettlement
}

public class TradeManager : MonoBehaviour
{
    public string importerName;
    GameObject importer;

    TradeLog[] tradeLogs;

    public int players = 4;
    public Card?[,] cards; // dim 0 shall represent players, dim 1 shall represent their stock.
    public Card?[,] newCards; // cards after trading, in case of queen.
    public bool[] aceCheck;
    public bool[] kingCheck;
    public bool[] queenCheck;
    public bool[] jackCheck;
    public bool[] jokerCheck;

    // increment when turn increase
    public int index = 0;
    public int tradeID = 0;
    public TradeState state;

    public int offerIndex;
    public int reqIndex;
    public bool agreement; // did the other player agree to trade?
    public int target; // target of the trade

    void Start()
    {
        // Setup tradelog things
        tradeLogs = new TradeLog[players * 2]; // In case of aces, we multiply by 2.

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
        Card?[,] tempCards = cards;
        Card? offer = tempCards[index, offerIndex];
        Card? req = tempCards[target, reqIndex];
        tradeLogs[tradeID] = new TradeLog(tempCards, offer, req, index, offerIndex, target, reqIndex);


        if (req == null || offer == null) { throw new NullReferenceException("Somehow, either the requested or offered card was null. This wrong."); }

        // swap the cards
        tempCards[index, offerIndex] = req;
        tempCards[target, reqIndex] = offer;

        newCards = tempCards;
        tradeID++;
    }

    void OnNewTurn()
    {
        Debug.Log("This isn't implemented!!!");
        // this is mostly going to be UI stuff, so I'm postponing this.
    }
}
