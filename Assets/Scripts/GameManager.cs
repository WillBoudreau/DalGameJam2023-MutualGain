using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Reference for table
    public GameObject Table;
    // Transforms of all player locations
    public Transform Player1Location;
    public Transform Player2Location;
    public Transform Player3Location;
    public Transform Player4Location;
    // References for hand location
    public GameObject stockZone;
    public GameObject tradeStockZone; 
    public int startingHandSize = 4;
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
    public GameObject tradePanel;
    public GameObject actionPanel;
    public GameObject turnUI;
    public GameObject tradeManager;
    // Active Player hand
    public List<GameObject> activeStock;
    public List<GameObject> activeTradeStock;
    private List<GameObject> activeOffer1;
    public List<Transform> offer1Locations;
    private List<GameObject> activeOffer2;
    public List<Transform> offer2Locations;
    private List<GameObject> activeOffer3;
    public List<Transform> offer3Locations;
    //object reference
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject Deck;
    [SerializeField] private GameObject ScreenHider;
    // Trade Text references
    public TextMeshProUGUI Offer1Text;
    public TextMeshProUGUI Offer2Text;
    public TextMeshProUGUI Offer3Text;
    //creating lists for cards
    [SerializeField] private List<GameObject> deck = new List<GameObject>();
    [SerializeField] private List<GameObject> attribution = new List<GameObject>(); //hand of cards for giving players suits

    //players and list
    [SerializeField] private List<GameObject> pList = new List<GameObject>(); //List of players
    [SerializeField] private List<GameObject> turnOrder = new List<GameObject>(); //List for player turn order

    //initialization for trade matrix
    // Get the exporter.
    public GameObject tradeExporter;
    //variables
    private bool lastRound = false;
    private int roundNumber =0;
    private int turnCounter;
    public int tradeTurnCounter;
    public bool gameStarted = false;
    public bool canTrade = false;

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


        tradeManager = GameObject.Find("TradeManager");

        for (int i = 1; i<=26; i++)
        {

        }

    }


    // Update is called once per frame
    
    void Update()
    {
        if(gameStarted == true)
        {
            SetTableLocation();
        }
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
        turnOrder.Clear();

        //sets new turn order
        turnOrder.Add(pList[(round-1) % 4]);
        turnOrder.Add(pList[(round) % 4]);
        turnOrder.Add(pList[(round+1) % 4]);
        turnOrder.Add(pList[(round+2) % 4]);
    }

    //Method that starts the round (called on end of trade)
    public void StartRound()
    {
        if(gameStarted == false)
        {gameStarted = true;}
        // makes sure the right UI elements are active at the start of each round
        if(playerTurnScreen.activeSelf == false)
        {
            playerTurnScreen.SetActive(true);
            if(turnUI.activeSelf == false)
            {
                turnUI.SetActive(true);
            }
        }
        if(tradeRoundUI.activeSelf == true)
        {tradeRoundUI.SetActive(false);}
        if(roundNumber == 0)
        {
            DrawStartHand();
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
        SetNextTurnText(); // Sets text prompt for next player
        SetUICounterText(); // Test Round counter Text
        Draw(turnOrder[turnCounter - 1]); //draws a card for the player who's turn it is
        GetHand();
    }

    //Method for player to take a draw action 
    public void DrawAction()
    {
        Draw(turnOrder[turnCounter-1]); //draws a card for the player who's turn it is
        GetHand();
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
        //ends current turn
        ResetHand();
        if (turnCounter < 4)
        {
            SetNextTurnText(); // Sets text prompt for next player
            StartTurn(turnOrder[turnCounter]); //calls the turn of the next player
        }
        else
        {
            StartTrade();
        }
        
    }

    public void StartTrade()
    {
        canTrade = true;
        tradeRoundUI.SetActive(true);
        playerTurnScreen.SetActive(false);
        //needs to be connected to trade
        GameObject exporter = GameObject.Find("TradeExporter");
        TradeExporter te = exporter.GetComponent<TradeExporter>();

        // Assign vars.
        te.cards = new GameObject[4, 3];
        te.aceCheck = new bool[4];
        te.kingCheck = new bool[4];
        te.queenCheck = new bool[4];
        te.jackCheck = new bool[4];
        te.jokerCheck = new bool[4];

        for (int i = 0; i < 4; i++)
        {
            Player p = turnOrder[i].GetComponent<Player>();
            GameObject[] pCards = p.ExportTradeCards();
            for (int j = 0; j < pCards.Length-1; j++)
            {
                te.cards[i,j] = pCards[j];
            }
            bool[] bools = p.ExportTradeBools();
            te.aceCheck[i] = bools[0];
            te.kingCheck[i] = bools[1];
            te.queenCheck[i] = bools[2];
            te.jackCheck[i] = bools[3];
            te.jokerCheck[i] = bools[4];
        }

        tradeManager.SetActive(true);
        tradeManager.SendMessage("Startup");
        tradeTurnCounter += 1;
        GetTradeHand();
        GetTradeOffers();
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
            playersTurnText = string.Format("{0}'s turn", turnOrder[turnCounter-1].name);
            roundCounterText = string.Format("Round: {0}", roundNumber);
            playersTurnUI.text = playersTurnText;
            roundCounterUI.text = roundCounterText;
        }
    }
    // Method that sets starting game text
    private void SetGameStartText()
    {
        // sets game starting text
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
        //sets text for next turn screen blocker
        nextTurnText = string.Format("{0} are you ready to start your turn?", turnOrder[turnCounter-1].name);
        nextTurnTextObject.text = nextTurnText;
    }
    // debug End trade method to test UI and Turn loop. 
    public void EndTrade()
    {
        // Get the exporter.
        GameObject exporter = GameObject.Find("TradeExporter");
        TradeExporter te = exporter.GetComponent<TradeExporter>();
        // Retrieve the cards.
        GameObject[,] cards = te.cards;

        // We need to send each player their exported cards.
        for (int i = 0; i < 4; i++)
        {
            Player p = turnOrder[i].GetComponent<Player>();
            GameObject[] send = new GameObject[cards.GetLength(1)];
            for(int j = 0; j < cards.GetLength(1); j++)
            {
                send[j] = cards[i, j];
            }
            p.ImportTradedCards(send);
        }

        // what would be the end of all trading. 
        tradeManager.SetActive(false);
        StartRound();
    }
    private void SetTableLocation()
    {
        // rotates table to active players turn
        if(turnOrder[turnCounter-1].name == "Player 1")
        {
            Table.transform.position = Player1Location.position;
            Table.transform.rotation = Player1Location.rotation;
        }
        if(turnOrder[turnCounter-1].name == "Player 2")
        {
            Table.transform.position = Player2Location.position;
            Table.transform.rotation = Player2Location.rotation;
        }
        if(turnOrder[turnCounter-1].name == "Player 3")
        {
            Table.transform.position = Player3Location.position;
            Table.transform.rotation = Player3Location.rotation;
        }
        if(turnOrder[turnCounter-1].name == "Player 4")
        {
            Table.transform.position = Player4Location.position;
            Table.transform.rotation = Player4Location.rotation;
        }
    }
    private void GetHand()
    {
        // gets the active players hand
        activeStock = turnOrder[turnCounter-1].GetComponent<Player>().stock;
        int stockLength = activeStock.Count;
        // sets hand to child of hand zone so they become visible. 
        for(int i = 0; i < stockLength; i++)
        {
            activeStock[i].transform.parent = stockZone.transform;
            activeStock[i].transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        }
    }
    private void GetTradeHand()
    {
        // gets the active players trade stock. 
        activeTradeStock = turnOrder[tradeTurnCounter-1].GetComponent<Player>().tradeStock;
        int tradeStockLength = activeTradeStock.Count;
        for(int i = 0; i < tradeStockLength; i++)
        {
            activeTradeStock[i].transform.parent = tradeStockZone.transform;
        }
    }
    private void ResetHand()
    {
        //resets the active hand so next players hand will become active.
        int stockLength = activeStock.Count;
        for(int i = 0; i < stockLength; i++)
        {
            activeStock[i].transform.parent = turnOrder[turnCounter-1].transform;
        }
    }
    private void DrawStartHand()
    {
        // draws the starting hand
        for(int i = 0; i < pList.Count; i++)
        {
            for(int j = 0; j < startingHandSize; j++)
            {
                Draw(pList[i]);
            }
        }
    }
    public void ClearTradeMenu()
    {
        // clears values so next player can use areas
        tradePanel.GetComponent<OfferDropzone>().cardsForOffer = 0;
        actionPanel.GetComponent<ActionDropzone>().cardSloted = false;
    }
    private void GetTradeOffers()
    {
        if(turnOrder[tradeTurnCounter-1].name == "Player 1")
        {

            // gets players 2-4 trade offers
            if(activeOffer1 != null)
            {
                activeOffer1 = turnOrder[tradeTurnCounter].GetComponent<Player>().tradeStock;
                Offer1Text.text = turnOrder[tradeTurnCounter].GetComponent<Player>().name + "'s Trade offer";
                activeOffer1[0].transform.parent = offer1Locations[0].transform;
                activeOffer1[1].transform.parent = offer1Locations[1].transform;
                activeOffer1[2].transform.parent = offer1Locations[2].transform;
            }
            if(activeOffer2 != null)
            {
                activeOffer2 = turnOrder[tradeTurnCounter+1].GetComponent<Player>().tradeStock;
                Offer2Text.text = turnOrder[tradeTurnCounter+1].GetComponent<Player>().name + "'s Trade offer";
                activeOffer2[0].transform.parent = offer2Locations[0].transform;
                activeOffer2[1].transform.parent = offer2Locations[1].transform;
                activeOffer2[2].transform.parent = offer2Locations[2].transform;
            }
            if(activeOffer3 != null)
            {
                activeOffer3 = turnOrder[tradeTurnCounter+2].GetComponent<Player>().tradeStock;
                Offer3Text.text = turnOrder[tradeTurnCounter+2].GetComponent<Player>().name + "'s Trade offer";
                activeOffer3[0].transform.parent = offer3Locations[0].transform;
                activeOffer3[1].transform.parent = offer3Locations[1].transform;
                activeOffer3[2].transform.parent = offer3Locations[2].transform;
            }
        }
        if(turnOrder[tradeTurnCounter-1].name == "Player 2")
        {
            // gets players 1,3,4 trade offers
            activeOffer1 = turnOrder[tradeTurnCounter-2].GetComponent<Player>().tradeStock;
            Offer1Text.text = turnOrder[tradeTurnCounter-2].GetComponent<Player>().name + "'s Trade offer";
            activeOffer2 = turnOrder[tradeTurnCounter].GetComponent<Player>().tradeStock;
            Offer2Text.text = turnOrder[tradeTurnCounter].GetComponent<Player>().name + "'s Trade offer";
            activeOffer3 = turnOrder[tradeTurnCounter+1].GetComponent<Player>().tradeStock;
            Offer3Text.text = turnOrder[tradeTurnCounter+1].GetComponent<Player>().name + "'s Trade offer";
            activeOffer1[0].transform.parent = offer1Locations[0].transform;
            activeOffer1[1].transform.parent = offer1Locations[1].transform;
            activeOffer1[2].transform.parent = offer1Locations[2].transform;
            activeOffer2[0].transform.parent = offer2Locations[0].transform;
            activeOffer2[1].transform.parent = offer2Locations[1].transform;
            activeOffer2[2].transform.parent = offer2Locations[2].transform;
            activeOffer3[0].transform.parent = offer3Locations[0].transform;
            activeOffer3[1].transform.parent = offer3Locations[1].transform;
            activeOffer3[2].transform.parent = offer3Locations[2].transform;
        }
        if(turnOrder[tradeTurnCounter-1].name == "Player 3")
        {
            // gets players 1,2,4 trade offers.
            activeOffer1 = turnOrder[tradeTurnCounter-3].GetComponent<Player>().tradeStock;
            Offer1Text.text = turnOrder[tradeTurnCounter-3].GetComponent<Player>().name + "'s Trade offer";
            activeOffer2 = turnOrder[tradeTurnCounter-2].GetComponent<Player>().tradeStock;
            Offer2Text.text = turnOrder[tradeTurnCounter-2].GetComponent<Player>().name + "'s Trade offer";
            activeOffer3 = turnOrder[tradeTurnCounter].GetComponent<Player>().tradeStock;
            Offer3Text.text = turnOrder[tradeTurnCounter].GetComponent<Player>().name + "'s Trade offer";
            activeOffer1[0].transform.parent = offer1Locations[0].transform;
            activeOffer1[1].transform.parent = offer1Locations[1].transform;
            activeOffer1[2].transform.parent = offer1Locations[2].transform;
            activeOffer2[0].transform.parent = offer2Locations[0].transform;
            activeOffer2[1].transform.parent = offer2Locations[1].transform;
            activeOffer2[2].transform.parent = offer2Locations[2].transform;
            activeOffer3[0].transform.parent = offer3Locations[0].transform;
            activeOffer3[1].transform.parent = offer3Locations[1].transform;
            activeOffer3[2].transform.parent = offer3Locations[2].transform;
        }
        if(turnOrder[tradeTurnCounter-1].name == "Player 4")
        {
            // gets players 1-3 trades offers.
            activeOffer1 = turnOrder[tradeTurnCounter-4].GetComponent<Player>().tradeStock;
            Offer1Text.text = turnOrder[tradeTurnCounter-4].GetComponent<Player>().name + "'s Trade offer";
            activeOffer2 = turnOrder[tradeTurnCounter-3].GetComponent<Player>().tradeStock;
            Offer2Text.text = turnOrder[tradeTurnCounter-3].GetComponent<Player>().name + "'s Trade offer";
            activeOffer3 = turnOrder[tradeTurnCounter-2].GetComponent<Player>().tradeStock;
            Offer3Text.text = turnOrder[tradeTurnCounter-2].GetComponent<Player>().name + "'s Trade offer";
            activeOffer1[0].transform.parent = offer1Locations[0].transform;
            activeOffer1[1].transform.parent = offer1Locations[1].transform;
            activeOffer1[2].transform.parent = offer1Locations[2].transform;
            activeOffer2[0].transform.parent = offer2Locations[0].transform;
            activeOffer2[1].transform.parent = offer2Locations[1].transform;
            activeOffer2[2].transform.parent = offer2Locations[2].transform;
            activeOffer3[0].transform.parent = offer3Locations[0].transform;
            activeOffer3[1].transform.parent = offer3Locations[1].transform;
            activeOffer3[2].transform.parent = offer3Locations[2].transform;
        }
    }
    public void ConfirmTradeButton()
    {
        // used by confirm button to end trade turn.
        if(tradeTurnCounter < 4)
        {
            StartTrade();
        }
        else
        {
            tradeTurnCounter = 0;
            EndTrade();
        }
    }
    public void canTradeToggle()
    {
        //prevents player from trading a 2nd time.
        if(canTrade == false)
        {
            canTrade = true;
        }
        else
        {
            canTrade = false;
        }
    }
}
