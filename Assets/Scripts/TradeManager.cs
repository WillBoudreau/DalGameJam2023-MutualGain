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

    public Card[,] inputCards; // dim 0 shall represent players, dim 1 shall represent their stock.
    public bool[] aceCheck;
    public bool[] kingCheck;

    // increment when turn increase
    public int index;
    public TradeState state;

    public Card offer;
    public bool agreement; // did the other player agree to trade?

    void Start()
    {
        // Get the importer.
        importer = GameObject.Find(importerName);
        TradeExporter te = importer.GetComponent<TradeExporter>();

        // Get stuff from the importer.
        inputCards = te.cards;
        aceCheck = te.aceCheck;
        kingCheck = te.kingCheck;
    }

    // name not set in stone, this is run when a card is selected.
    void OnDecide()
    {
        if (state == TradeState.settlement && agreement) { OnAccept(); }

        // Check for ace and settlement, increment if settlement but not ace.
        if (state == TradeState.settlement && !aceCheck[index]) { index++; aceCheck[index] = false; }

        // run this at the end.
        TradeState ts = state;
        if (ts == TradeState.settlement) { state = TradeState.offer; }      
        if (kingCheck[index]) { OnAccept(); index++; } // if player has king, skip their settlement state and move on.
        else { if (ts == TradeState.offer) { state = TradeState.settlement; } }
    }

    void OnAccept()
    {
        Debug.Log("This isn't implemented!!!");
    }
}
