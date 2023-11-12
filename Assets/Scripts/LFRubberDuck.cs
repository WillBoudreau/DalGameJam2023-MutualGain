using System;
using System.Collections.Generic;

// This makes our list of ints that we choose from when randomizing things.

int tempPlayers = 4;
int tempTradeCardLimit = 3;
Card[,] tempCards = new Card[4, 3];
Card[,] newCards = new Card[4, 3];

List<Card> cards = new List<Card>();

foreach (Card card in tempCards)
{
    cards.Add(card);
}

for (int i = 0; i < tempPlayers; i++)
{
    for (int j = 0; j < tempTradeCardLimit; j++)
    {
        // randomly pick a number
        Random r = new Random();
        int index = r.Next(cards.Count);

        // assign card and prevent it from being reused
        newCards[i, j] = cards[index];
        cards.Remove(cards[index]);
    }
}

// assign the new cards thing here.
