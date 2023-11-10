using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TradeState
{
    offer,
    decision
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

    // name not set in stone
    void OnOfferDecide(
}
