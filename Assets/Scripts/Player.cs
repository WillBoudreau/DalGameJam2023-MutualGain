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

    public void start()
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

    //accessor methods start here

    //accessor method for score
    public int GetScore()
    {
        return score;
    }
}
