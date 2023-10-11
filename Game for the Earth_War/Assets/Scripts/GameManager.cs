using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Inscribed")]

    private int deckSize = 52;
    private bool isWar = false;
    private Alien alien;
    private User player;
    private Played_Cards played_Cards;

    public float waitTime = 2f;
    public float startTime = 999;
    public int defalutSlotNum = 1;
    public bool checkCards = false;
    public List<Card> deck = new List<Card>();

    // Start is called before the first frame update
    void Start()
    {
        alien = FindObjectOfType<Alien>();
        player = FindObjectOfType<User>();
        played_Cards = FindObjectOfType<Played_Cards>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!(player.card_Deck_And_Slots.deck.Count == 0) || !(player.card_Deck_And_Slots.deck.Count == 0) 
            || !(true))//replace true with timer
        {
            if (!isWar)
            {
                alien.playCard(defalutSlotNum);
                player.playCard(defalutSlotNum);
                played_Cards.findWinNormPlay(defalutSlotNum);

                if ((Time.time - startTime >= waitTime) && checkCards)
                {
                    checkCards = false;
                    played_Cards.finishNormPlay(defalutSlotNum);
                    isWar = (played_Cards.winType == Played_Cards.WinType.WAR);
                }
            }
            else
            {
                if (alien.currWarSlot < played_Cards.alienAvaibleSlots.Length)
                {
                    if (alien.playCard(alien.currWarSlot))
                    {
                        // Debug.Log("Played card: " + played_Cards.alienDeck[alien.currWarSlot]);
                        alien.currWarSlot = alien.currWarSlot + 1;
                        alien.canPlayCard = alien.currWarSlot < played_Cards.alienAvaibleSlots.Length;
                    }
                }
                if (player.currWarSlot < played_Cards.playerCardSlots.Length)
                {
                    if (player.playCard(player.currWarSlot))
                    {
                        player.currWarSlot = player.currWarSlot + 1;
                        player.canPlayCard = player.currWarSlot < played_Cards.playerCardSlots.Length;
                    }
                }
                if ((alien.currWarSlot >= played_Cards.alienAvaibleSlots.Length) 
                    && (player.currWarSlot >= played_Cards.playerCardSlots.Length)
                    && checkCards)
                {
                    checkCards = false;
                    int alienWarScore = 0;
                    int playerWarScore = 0;
                    for (int i = 0; i < played_Cards.alienAvaibleSlots.Length; i++)
                    {
                        if (played_Cards.findWinWarPlay(i) == Played_Cards.WinType.ALIEN_WIN)
                        {
                            alienWarScore++;
                        }
                        else
                        {
                            playerWarScore++;
                        }
                        played_Cards.displayDialog("Its\n" + playerWarScore + "to " + alienWarScore);
                    }

                    played_Cards.finishWarPlay(playerWarScore > alienWarScore);

                    alien.canPlayCard = true;
                    player.canPlayCard = true;
                    isWar = false;
                }
            }
        }
        else
        {
            //exit screens
            alien.canPlayCard = false;
            player.canPlayCard = false;
        }
    }

    public List<Card> getStartingDeck(bool isPlayer)
    {
        List<Card> playerDeck = new List<Card>();

        for (int i = 0; i < deckSize / 2; i++)
        {
            Card tempCard = deck[Random.Range(0, deck.Count - 1)];//get rand card
            tempCard.gameObject.SetActive(false);
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
