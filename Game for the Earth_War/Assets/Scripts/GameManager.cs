using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Inscribed")]

    int deckSize = 52;
    
    public List<Card> deck = new List<Card>();

    // Start is called before the first frame update
    void Start()
    {
        //create initial deck
        /*
        string[] suit = { "spade", "heart", "club", "diamond" };
        
        for (int i = 0; i < deck.Count; i++)
        {
            deck[i].setCard((i % 13) + 1, suit[i % 4]);
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Card> getStartingDeck(bool isPlayer)
    {
        List<Card> playerDeck = new List<Card>();

        for (int i = 0; i < deckSize / 2; i++)
        {
            Card tempCard = deck[Random.Range(0, deck.Count - 1)];//get rand card
            tempCard.faceUp = isPlayer;
            tempCard.isPlayableCard = isPlayer;

            if (!isPlayer)
            {
                tempCard.flip();
            }

            playerDeck.Add(tempCard);
            deck.Remove(tempCard);
        }

        return playerDeck;
    }
}
