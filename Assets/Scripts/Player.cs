using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variables
    [SerializeField] private int playerNumber;
    [SerializeField] private int score;
    [SerializeField] private string suit;

    //player "hand"
    public List<GameObject> stock = new List<GameObject>();

    // Trade things
    public List<GameObject> tradeStock = new List<GameObject>();
    public bool[] bools = new bool[5];


    void Start()
    {
        
    }

    //method called to set values of a player object
    public void SetPlayer(int pNum, GameObject card)
    {
        //sets the player number
        playerNumber = pNum;

        //gets the player's suit
        suit = card.GetComponent<Card>().GetSuit();

        //gives player thier first card
        stock.Add(card);
    }

    //method that adds card to the player's hand
    public void AddCard(GameObject card)
    {
        stock.Add(card);
    }

    //Method to calculate total score of player
    private int CalcScore()
    {
        int value = 0;

        //going through stock to calculate 
        foreach(GameObject card in stock)
        {
            if (card.GetComponent<Card>().GetSuit() == suit)
            {
                value += card.GetComponent<Card>().GetValue();
            }
        }

        return value;
    }

    public GameObject GetCard(int index)
    {
        GameObject card = new GameObject();
        card = stock[index];
        return card;
    }

    //accessor methods start here

    //accessor method for score
    public int GetScore()
    {
        return score;
    }

    public GameObject[] ExportTradeCards()
    {
        List<GameObject> temp = tradeStock;
        foreach(GameObject cardObj in temp)
        {
            Card card = cardObj.GetComponent<Card>();
            switch (card.GetFaceName())
            {
                case "Ace":
                    bools[0] = true; break;
                case "King":
                    bools[1] = true; break;
                case "Queen":
                    bools[2] = true;break;
                case "Jack":
                    bools[3] = true; break;
                case "Joker":
                    bools[4] = true; break;
            }
        }
        return temp.ToArray();
    }

    public bool[] ExportTradeBools()
    {
        return bools;
    }
}
