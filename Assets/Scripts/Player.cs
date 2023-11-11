using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variables
    int playerNumber;
    int score;
    string suit;

    //player "hand"
    public List<Card> stock;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //method called to set values of a player object
    public void setPlayer(int pNum, Card card)
    {
        //sets the player number
        playerNumber = pNum;

        //gets the player's suit
        suit = card.getSuit();

        //gives player thier first card
        stock.Add(card);
    }

    //Method to calculate total score of player
    private int calcScore()
    {
        int value = 0;

        //going through stock to calculate 
        foreach(Card card in stock)
        {
            if (card.getSuit() == suit)
            {
                value += card.getValue();
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
