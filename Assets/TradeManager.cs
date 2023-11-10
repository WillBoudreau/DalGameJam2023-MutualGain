using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeManager : MonoBehaviour
{
    public string importerName;
    GameObject importer;

    public card[,] inputCards;
    public bool[] aceCheck;

    void Start()
    {
        // Get each player's cards from the importer.
        importer = GameObject.Find(importerName);
        TradeExporter te = importer.GetComponent<TradeExporter>();
        inputCards = te.cards;
    }

    
}
