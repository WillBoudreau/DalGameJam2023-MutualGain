using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    //object references
    [SerializeField] private Sprite[] faces;
    [SerializeField] private Sprite[] cardBacks;
    public Image display;
    private Sprite face;
    private Sprite back;

    //variables
    public int id = 0;
    private int value; //id of 0 = blank card
    private string suit, faceName;
    private bool faceUp = true;

    //temp variables
    public int pNum = 1;

    // Start is called before the first frame update
    void Start()
    {
        setCard(id);
        SetCardBack(pNum);
        setImage();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setCard(int ID)
    {
        //set ID to be remembered
        id = ID;



        //in Spades group
        if (ID >= 1 && ID <= 13)
        {
            suit = "Spades";
        }
        //in Hearts group
        else if (ID >= 14 && ID <= 26)
        {
            suit = "Hearts";
            ID -= 13;
        }
        //in Diamonds group
        else if (ID >= 27 && ID <= 39)
        {
            suit = "Diamonds";
            ID -= 26;
        }
        //in Clubs group
        else if (ID >= 40 && ID <= 52)
        {
            suit = "Clubs";
            ID -= 39;
        }
        //jokers
        else if (ID == 53 || ID == 54)
        {
            suit = "NA";
            faceName = "Joker";
            value = 0;
        }
        //error handling
        else
        {
            suit = null;
            faceName = "blank";
            id = 0;
            value = 0;
        }

        //setting faceName and value
        switch (ID)
        {
            case 1:
                faceName = "Two";
                value = 2;
                break;
            case 2:
                faceName = "Three";
                value = 3;
                break;
            case 3:
                faceName = "Four";
                value = 4;
                break;
            case 4:
                faceName = "Five";
                value = 5;
                break;
            case 5:
                faceName = "Six";
                value = 6;
                break;
            case 6:
                faceName = "Seven";
                value = 7;
                break;
            case 7:
                faceName = "Eight";
                value = 8;
                break;
            case 8:
                faceName = "Nine";
                value = 9;
                break;
            case 9:
                faceName = "Ten";
                value = 10;
                break;
            case 10:
                faceName = "Jack";
                value = 0;
                break;
            case 11:
                faceName = "Queen";
                value = 0;
                break;
            case 12:
                faceName = "King";
                value = 0;
                break;
            case 13:
                faceName = "Ace";
                value = 0;
                break;

            default:
                break;
        }

        //get card face sprite
        face = faces[id];
    }

    //function that sets the card back acording to player number
    public void SetCardBack(int Pnum)
    {
        if (Pnum > 0 && Pnum <= 4)
        {
            back = cardBacks[Pnum - 1];
        }
        //error handling
        else
        {
            back = faces[0];
        }
    }

    //function that flips the card
    public void flip()
    {
        //sets the image for the card
        if (faceUp)
        {
            display.sprite = back;
        }
        else
        {
            display.sprite = face;
        }

        faceUp = !faceUp;
    }

    //function that hides a card while keeping it in scene
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    //function that sets initial image
    private void setImage()
    {
        display.sprite = face;
    }

    //accessor method start here

    //accessor method for value
    public int getValue()
    {
        return value;
    }

    //accessor method for suit
    public string getSuit()
    {
        return suit;
    }
}