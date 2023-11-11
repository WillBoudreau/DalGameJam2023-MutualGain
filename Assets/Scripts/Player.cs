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
    public void setPlayer(int pNum, GameObject card)
    {
        //sets the player number
        playerNumber = pNum;

        //gets the player's suit
        suit = card.GetComponent<Card>().getSuit();

        //gives player thier first card
        stock.Add(card);
    }

    //Method to calculate total score of player
    private int calcScore()
    {
        int value = 0;

        //going through stock to calculate 
        foreach(GameObject card in stock)
        {
            if (card.GetComponent<Card>().getSuit() == suit)
            {
                value += card.GetComponent<Card>().getValue();
            }
        }

        return value;
    }

    //accessor methods start here

    //accessor method for score
    public int getScore()
    {
        return score;
    }
}
