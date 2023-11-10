using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeManager : MonoBehaviour
{
    public string importerName;
    GameObject importer;

    public card[,] inputCards;
    public bool[] aceCheck;
    public bool[] kingCheck;

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

    // void TradeTurn(int playerIndex)
}
