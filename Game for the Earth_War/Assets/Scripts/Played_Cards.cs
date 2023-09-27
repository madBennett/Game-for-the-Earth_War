using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//largely as base for player and alien objects

public class Played_Cards : MonoBehaviour
{
    [Header("Inscribed")]

    private Player player;
    private Alien alien;

    public List<Card> playerDeck = new List<Card>();
    public List<Card> alienDeck = new List<Card>();

    public Transform[] playerCardSlots;
    public Transform[] alienCardSlots;
    
    public bool[] playerAvaibleSlots;
    public bool[] alienAvaibleSlots;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        alien = FindObjectOfType<Alien>();

        for (int i = 0; i < playerAvaibleSlots.Length; i++)
        {
            playerAvaibleSlots[i] = true;
            alienAvaibleSlots[i] = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addToPlayed(bool isPlayer, Card playedCard, int slotNum)
    {
        playerDeck.Add(playedCard);
        playedCard.isPlayableCard = false;
        if (isPlayer)
        {
            playerAvaibleSlots[slotNum] = false;
            playedCard.moveCard(playerCardSlots[slotNum].transform.position);
        }
        else
        {
            alienAvaibleSlots[slotNum] = false;
            playedCard.moveCard(alienCardSlots[slotNum].transform.position);
        }
    }

    public void findWinnerNormPlay(int slotNum = 1)
    {
        if (alienAvaibleSlots[slotNum] && playerAvaibleSlots[slotNum])
        {
            if (alienDeck[slotNum].num > playerDeck[slotNum].num)
            {
                //alien win
                alien.card_Deck_And_Slots.deck.Add(alienDeck[slotNum]);
                alien.card_Deck_And_Slots.deck.Add(playerDeck[slotNum]);
            }
            else if (alienDeck[slotNum].num == playerDeck[slotNum].num)
            {
                //war
                beginWar();
                return;
            }
            else
            {
                //player win
                player.card_Deck_And_Slots.deck.Add(alienDeck[slotNum]);
                player.card_Deck_And_Slots.deck.Add(playerDeck[slotNum]);
            }

            //remove cards and reset aviablity
            alienDeck[slotNum].moveCard(alienDeck[slotNum].startingPos);
            playerDeck[slotNum].moveCard(playerDeck[slotNum].startingPos);

            alienDeck.RemoveAt(slotNum);
            playerDeck.RemoveAt(slotNum);

            playerAvaibleSlots[slotNum] = true;
            alienAvaibleSlots[slotNum] = true;
        }
    }

    public void beginWar()
    {
        //behavior for war
    }
}
