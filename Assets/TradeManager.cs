using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeManager : MonoBehaviour
{
    public string importerName;
    GameObject importer;

    public card[,] inputCards;
    public bool[] aceCheck;

    // Start is called before the first frame update
    void Start()
    {
        importer = GameObject.Find(importerName);
        TradeExporter te = importer.GetComponent<TradeExporter>();
        inputCards = te.cards;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
