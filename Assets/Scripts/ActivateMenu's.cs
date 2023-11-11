using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMenu : MonoBehaviour
{
    // Send Will a meesage if you need help at all understanding
    //In game menus as GameObjects
    public GameObject TradeMenu;
    public GameObject ActionMenu;
    public GameObject GameOverMenu;
    //The Player Hand as an int
    public int PlayerHand;
    //Track how many cards in the Draw pile
    public int DrawpileCount;
    public int CardsLeft;
    public GameObject Drawpile;
    public GameObject HalfDraw;
    public GameObject QuarterDraw;
    //Draw a card from the draw pile
    public bool Draw;
    // Start is called before the first frame update
    void Start()
    {
        //DrawPileCount = the tracker for how many cards are left
        //Cards left = the tracker before the deck gets halfed
        DrawpileCount = 52;
        CardsLeft = DrawpileCount/2;
        PlayerHand = 4;
        // Menus set false 
        TradeMenu.SetActive(false);
        ActionMenu.SetActive(false);
        //Drawiles
        HalfDraw.SetActive(false);
        QuarterDraw.SetActive(false); 
    }

    public void ActivateTrade()
    {
        //Activates the Trade menu
        TradeMenu.SetActive(true);
    }
    public void TradeBack()
    {
        TradeMenu.SetActive(false);
    }
    public void ActivateAction()
    {
        //Activates the Action menu
        ActionMenu.SetActive(true);
    }
    public void ActionBack()
    {
        ActionMenu.SetActive(false);
    }
    public void DrawCard()
    {
        Draw = true;
        // When draw pile is at half, half is displayed then a quarter
        if(DrawpileCount == CardsLeft)
        {
            Debug.Log("Half");
            Drawpile.SetActive(false);
            HalfDraw.SetActive(true);
        }
        else if(DrawpileCount == CardsLeft/2)
        {
            HalfDraw.SetActive(false) ;
            QuarterDraw.SetActive(true);
        }
        if(DrawpileCount <= 0)
        {
            DrawpileCount = 0; 
            QuarterDraw.SetActive(false);
            Draw = false;
            PlayerHand = PlayerHand + 0;
            EndGame();
        }
        if(Draw == true)
        {
            PlayerHand++;
            DrawpileCount--;
        }
        else
        {
            Draw = false;
        }
    }
    public void EndGame()
    {
        Debug.Log("Game Over");
    }
}
