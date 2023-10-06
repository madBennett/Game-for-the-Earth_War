using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//largely as base for player and alien objects

public class Played_Cards : MonoBehaviour
{
    [Header("Inscribed")]

    private Alien alien;
    private User player;
    private GameManager gm;

    public List<Card> playerDeck = new List<Card>();
    public List<Card> alienDeck = new List<Card>();

    public Transform[] playerCardSlots;
    public Transform[] alienCardSlots;
    
    public bool[] playerAvaibleSlots;
    public bool[] alienAvaibleSlots;

    public int defalutSlotNum = 1;

    // Start is called before the first frame update
    void Start()
    {
        alien = FindObjectOfType<Alien>();
        player = FindObjectOfType<User>();
        gm = FindObjectOfType<GameManager>();

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
        if (isPlayer)
        {
            playerDeck.Add(playedCard);
            playedCard.move(playerCardSlots[slotNum].position);
            playerAvaibleSlots[slotNum] = false;
        }
        else
        {
            alienDeck.Add(playedCard);
            playedCard.move(alienCardSlots[slotNum].position);
            alienAvaibleSlots[slotNum] = false;
        }

        opCardsPlayed(slotNum);
    }

    public void opCardsPlayed(int slotNum)
    {
        if (!alienAvaibleSlots[slotNum] && !playerAvaibleSlots[slotNum])
        {
            alienDeck[0].flip();
        }
    }

    public void findWinnerNormPlay(int slotNum = 1)
    {
        if (!alienAvaibleSlots[slotNum] && !playerAvaibleSlots[slotNum])
        {
            Card alienCard = alienDeck[0];
            Card playerCard = playerDeck[0];

            if (checkWarCond())
            {
                //war
                beginWar();
                //return;
            }
            else if (alienCard.num > playerCard.num)
            {
                //alien win
                alien.card_Deck_And_Slots.addToDeck(alienCard, false);
                alien.card_Deck_And_Slots.addToDeck(playerCard, false);
            }
            else
            {
                //player win
                player.card_Deck_And_Slots.addToDeck(alienCard, true);
                player.card_Deck_And_Slots.addToDeck(playerCard, true);
            }

            

            //remove cards and reset aviablity

            alienDeck.Remove(alienCard);
            playerDeck.Remove(playerCard);

            alienCard.gameObject.SetActive(false);
            playerCard.gameObject.SetActive(false);

            playerAvaibleSlots[slotNum] = true;
            alienAvaibleSlots[slotNum] = true;

            player.canPlayCard = true;
            alien.canPlayCard = true;
        }
    }

    public bool checkWarCond()
    {
        bool isWar = false;

        if (!alienAvaibleSlots[defalutSlotNum] && !playerAvaibleSlots[defalutSlotNum])
        {
            Card alienCard = alienDeck[0];
            Card playerCard = playerDeck[0];

            isWar = (alienCard.num == playerCard.num) || (false) || (false);
        }

        return isWar;
    }

    public void beginWar()
    {
        //war behavoir
    }
}
