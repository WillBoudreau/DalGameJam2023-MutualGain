using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //pbject reference
    [SerializeField] GameObject prefab;

    //creating lists for cards
    [SerializeField] private List<GameObject> deck = new List<GameObject>();
    [SerializeField] private List<GameObject> attribution = new List<GameObject>(); //hand of cards for giving players suits

    //players and list
    [SerializeField] private List<GameObject> pList = new List<GameObject>(); //List of players

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

        //adding players to player list
        pList.Add(new GameObject());
        pList[0].AddComponent<Player>();

        pList.Add(new GameObject());
        pList[1].AddComponent<Player>();

        pList.Add(new GameObject());
        pList[2].AddComponent<Player>();

        pList.Add(new GameObject());
        pList[3].AddComponent<Player>();

        int j = 1;

        //giving values to players
        foreach (GameObject player in pList) 
        {
            int rand = Random.Range(0, attribution.Count); //generating random number to assign suit
            player.GetComponent<Player>().setPlayer(j, attribution[rand]); //draws one card from the attribution hand 
            attribution.Remove(attribution[rand]); //removes card for next draw

            j++;
        }

        //setting up deck
        for (int i=1; i<= 54; i++)
        {
            //remove "2" cards that were already handed out
            if (i==1 || i==14 || i==27 || i==40)
            {
                continue; //skips itteration of the for loop
            }
            
            GameObject card = Instantiate(prefab);
            card.GetComponent<Card>().SetCard(i);
            
            deck.Add(card); //add card to the deck
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
