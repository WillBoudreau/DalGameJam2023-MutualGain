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



        // Check for ace, increment if false.
        if (!aceCheck[index]) { index++; }

        // run this at the end.
        TradeState ts = state;
        if (ts == TradeState.offer) { state = TradeState.settlement; }
        if (ts == TradeState.settlement) { state = TradeState.offer; }
    }
}
