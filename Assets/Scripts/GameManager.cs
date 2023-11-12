using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //UI Text References for turn counters
    public TextMeshProUGUI roundCounterUI;
    public TextMeshProUGUI playersTurnUI;
    private string roundCounterText;
    private string playersTurnText;
    // game start text screen references
    public TextMeshProUGUI gameStartTextObject;
    private string gameStartText;
    // Next turn text object reference
    public TextMeshProUGUI nextTurnTextObject;
    private string nextTurnText;
    // UI Game object references
    public GameObject tradeRoundUI;
    public GameObject playerTurnScreen;
    public GameObject tradeMenu;
    public GameObject turnUI;
    //object reference
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject Deck;
    [SerializeField] private GameObject ScreenHider;

    //creating lists for cards
    [SerializeField] private List<GameObject> deck = new List<GameObject>();
    [SerializeField] private List<GameObject> attribution = new List<GameObject>(); //hand of cards for giving players suits

    //players and list
    [SerializeField] private List<GameObject> pList = new List<GameObject>(); //List of players
    [SerializeField] private List<GameObject> turnOrder = new List<GameObject>(); //List for player turn order

    //initialization for trade matrix

    //variables
    private bool lastRound = false;
    private int roundNumber =0;
    private int turnCounter;

    // Start is called before the first frame update
    void Start()
    {
        //making list for attribution

        //2 of spades
        attribution.Add(Instantiate(prefab));
        attribution[0].GetComponent<Card>().SetCard(1);

        //2 of hearts
        attribution.Add(Instantiate(prefab));
        attribution[1].GetComponent<Card>().SetCard(14);

        //2 of diamonds
        attribution.Add(Instantiate(prefab));
        attribution[2].GetComponent<Card>().SetCard(27);

        //2 of clubs
        attribution.Add(Instantiate(prefab));
        attribution[3].GetComponent<Card>().SetCard(40);

        //adding players to player list (naming is for clarity in scene view
        pList.Add(new GameObject());
        pList[0].name = "Player 1";
        pList[0].AddComponent<Player>();

        pList.Add(new GameObject());
        pList[1].name = "Player 2";
        pList[1].AddComponent<Player>();

        pList.Add(new GameObject());
        pList[2].name = "Player 3";
        pList[2].AddComponent<Player>();

        pList.Add(new GameObject());
        pList[3].name = "Player 4";
        pList[3].AddComponent<Player>();

        int j = 1;

        //giving values to players
        foreach (GameObject player in pList) 
        {
            int rand = Random.Range(0, attribution.Count); //generating random number to assign suit
            player.GetComponent<Player>().SetPlayer(j, attribution[rand]); //draws one card from the attribution hand 
            attribution[rand].transform.parent = player.transform; //sets the card as a child of the player for visual clarity in scene menu
            attribution.Remove(attribution[rand]); //removes card for next draw

            j++;
        }

        //setting up deck
        for (int i=1; i<= 54; i++)
        {
            //remove "2" cards that were already handed out
            if (i==1 || i==14 || i==27 || i==40)
            {
                continue; //skips iteration of the for loop
            }
            
            GameObject card = Instantiate(prefab);
            card.GetComponent<Card>().SetCard(i);
            
            deck.Add(card); //add card to the deck
            card.transform.parent = Deck.transform; //sets cards as a child of the Deck object for visual clarity in scene menu
        }
    }


    // Update is called once per frame
    void Update()
    {
        SetUICounterText(); // Test Round counter Text
        SetGameStartText(); // Sets Starting test
        SetNextTurnText(); // Sets text prompt for next player
    }

    // Method for Drawing a card
    public void Draw(GameObject player)
    {
        if (deck.Count > 0)
        {
            int rand = Random.Range(0, deck.Count); //getting index for random card
            player.GetComponent<Player>().AddCard(deck[rand]); //this adds to the player's list inventory
            deck[rand].transform.parent = player.transform; //set player as parent of card
            deck.Remove(deck[rand]); //removes card from deck
        }

        if(deck.Count == 0) // check to see if deck is now empty
        {
            lastRound = true;
        }   
    }

    //Method for setting turn order. When moving through turn order, players will be removed from the turn order list
    private void SetTurnOrder(int round)
    {
        //clears previous turn order
        foreach (GameObject player in turnOrder)
        {
            turnOrder.Remove(player);
        }

        //sets new turn order
        turnOrder.Add(pList[(round-1) % 4]);
        turnOrder.Add(pList[(round) % 4]);
        turnOrder.Add(pList[(round+1) % 4]);
        turnOrder.Add(pList[(round+2) % 4]);
    }

    //Method that starts the round (called on end of trade)
    public void StartRound()
    {
        // makes sure the right UI elements are active at the start of each round
        if(playerTurnScreen.activeSelf == false)
        {
            playerTurnScreen.SetActive(true);
        }
        if(tradeRoundUI.activeSelf == true)
        {
            tradeRoundUI.SetActive(false);
        }
        roundNumber++; //sets the round number
        SetTurnOrder(roundNumber); //sets turn order
        turnCounter = 0; //clears turn counter from last round (or sets it for first round)
        StartTurn(turnOrder[turnCounter]); //starts turn of first player in turn order
    }

    //Method to start a player's turn
    public void StartTurn(GameObject player)
    {
        // makes sure the right UI elements are active at the start of each turn
        if(turnUI.activeSelf == false && tradeMenu.activeSelf == true)
        {
            turnUI.SetActive(true);
            tradeMenu.SetActive(false);
        }
        ToggleScreenHide(); //hides information until player pushes button
        turnCounter++;
        Draw(turnOrder[turnCounter - 1]); //draws a card for the player who's turn it is
        //needs to be connected to UI
    }

    //Method for player to take a draw action 
    public void DrawAction()
    {
        Draw(turnOrder[turnCounter-1]); //draws a card for the player who's turn it is
        //needs to be connected to UI
    }

    //Method for player to take a trade action
    public void TradeAction()
    {
        //needs to make trade matrix (implement when trade is available)
    }

    //Method to end turn
    public void EndTurn()
    {
        if (turnCounter < 4)
        {
            StartTurn(turnOrder[turnCounter]); //calls the turn of the next player
        }
        else
        {
            StartTrade();
        }
        
    }

    public void StartTrade()
    {
        tradeRoundUI.SetActive(true);
        playerTurnScreen.SetActive(false);
        //needs to be connected to trade
    }

    // Method that toggles the screen hider (hide player hand between turns)
    public void ToggleScreenHide()
    {
        if (ScreenHider.activeSelf == true)
        {
            ScreenHider.SetActive(false);
        }
        else
        {
            ScreenHider.SetActive(true);
        }
    }
    // method that sets the text for the UI player turn and what round it is.
    private void SetUICounterText()
    {
        if(playersTurnUI == null || roundCounterUI == null)
        {return;}
        else
        {
            if(turnCounter-1 < 0)
            {
                turnCounter = 1;
            }
            playersTurnText = string.Format("{0}'s turn", pList[turnCounter-1].name);
            roundCounterText = string.Format("Round: {0}", roundNumber);
            playersTurnUI.text = playersTurnText;
            roundCounterUI.text = roundCounterText;
        }
    }
    // Method that sets starting game text
    private void SetGameStartText()
    {
        if(gameStartTextObject == null)
        {return;}
        else
        {
            gameStartText = "Are you ready to start the game?";
            gameStartTextObject.text = gameStartText;
        }
    }
    // method that sets next turn message appropriately 
    private void SetNextTurnText()
    {
        nextTurnText = string.Format("{0} are you ready to start your turn?", pList[turnCounter-1].name);
        nextTurnTextObject.text = nextTurnText;
    }
    // debug End trade method to test UI and Turn loop. 
    public void EndTrade()
    {
        // what would be the end of all trading. 
        StartRound();
    }
}
