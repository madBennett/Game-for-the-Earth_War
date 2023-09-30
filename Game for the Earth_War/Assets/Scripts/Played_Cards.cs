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
            playedCard.transform.position = playerCardSlots[slotNum].transform.position;
            playerAvaibleSlots[slotNum] = false;
        }
        else
        {
            alienDeck.Add(playedCard);
            playedCard.transform.position = alienCardSlots[slotNum].transform.position;
            alienAvaibleSlots[slotNum] = false;
        }
    }

    public void findWinnerNormPlay(int slotNum)
    {
        if (!alienAvaibleSlots[slotNum] && !playerAvaibleSlots[slotNum])
        {
            Card alienCard = alienDeck[0];
            Card playerCard = playerDeck[0];

            alienCard.flip();

            //wait for time
            gm.wait(200);//not working!!!!!!

            if (alienCard.num > playerCard.num)
            {
                //alien win
                alien.card_Deck_And_Slots.addToDeck(alienCard, false);
                alien.card_Deck_And_Slots.addToDeck(playerCard, false);
            }
            else if (alienCard.num == playerCard.num)
            {
                //war
                beginWar();
                return;
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

    public void beginWar()
    {
        //behavior for war
    }
}
