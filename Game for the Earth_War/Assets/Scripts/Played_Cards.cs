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

    private int warOccurances = 0;
    private List<Card> warPool = new List<Card>();

    public WinType winType = WinType.ERROR;

    public List<Card> playerDeck = new List<Card>();
    public List<Card> alienDeck = new List<Card>();

    public Transform[] playerCardSlots;
    public Transform[] alienCardSlots;
    
    public bool[] playerAvaibleSlots;
    public bool[] alienAvaibleSlots;

    public int defalutSlotNum = 1;
    public TextMeshProUGUI dialogText;
    public GameObject dialogBox;

    public enum WinType
    {
        ERROR,
        ALIEN_WIN,
        PLAYER_WIN,
        WAR
    }

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
        removeDiaglog();
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    public void addToPlayed(bool isPlayer, Card playedCard, int slotNum)
    {
        if (isPlayer)
        {
            playerDeck.Insert(slotNum, playedCard);
            playedCard.move(playerCardSlots[slotNum].position);
            playerAvaibleSlots[slotNum] = false;
        }
        else
        {
            alienDeck.Insert(slotNum, playedCard);
            playedCard.move(alienCardSlots[slotNum].position);
            alienAvaibleSlots[slotNum] = false;
        }

        //opCardsPlayed(slotNum);
        if (!alienAvaibleSlots[slotNum] && !playerAvaibleSlots[slotNum])
        {
            alienDeck[slotNum].flip();
        }
    }

    public void opCardsPlayed(int slotNum)
    {
        if (!alienAvaibleSlots[slotNum] && !playerAvaibleSlots[slotNum])
        {
            //alienDeck[0].flip();
        }
    }

    public void findWinNormPlay(int slotNum)
    {
        if ((!alienAvaibleSlots[slotNum] && !playerAvaibleSlots[slotNum]) 
            && (alienDeck.Count > 0 && playerDeck.Count > 0))
        {
            Card alienCard = alienDeck[slotNum];
            Card playerCard = playerDeck[slotNum];

            winType = WinType.ERROR;

            if ((alienCard.num == playerCard.num) || (alienCard.suit == playerCard.suit) 
                || (false))
            {
                //war
                winType = WinType.WAR;
                displayDialog("LETS BEGIN BATTLE");
            }
            else if (alienCard.num > playerCard.num)
            {
                //alien win
                winType = WinType.ALIEN_WIN;
                displayDialog("HA! I WIN!");
            }
            else
            {
                //player win
                winType = WinType.PLAYER_WIN;
                displayDialog("Darn, you win");
            }

            gm.checkCards = true;
        }
    }

    public void finishNormPlay(int slotNum)
    {
        if ((!alienAvaibleSlots[slotNum] && !playerAvaibleSlots[slotNum])
            && (alienDeck.Count > 0 && playerDeck.Count > 0))
        {
            Card alienCard = alienDeck[slotNum];
            Card playerCard = playerDeck[slotNum];

            switch (winType)
            {
                case WinType.PLAYER_WIN:
                    //player win
                    player.card_Deck_And_Slots.addToDeck(alienCard, true);
                    alien.card_Deck_And_Slots.deck.Remove(alienCard);
                    break;

                case WinType.ALIEN_WIN:
                    //alien win
                    alien.card_Deck_And_Slots.addToDeck(playerCard, false);
                    player.card_Deck_And_Slots.deck.Remove(playerCard);
                    break;

                case WinType.WAR:
                    //war
                    warPool.Add(alienCard);
                    warPool.Add(playerCard);//save cuurr cards to add to winners decks
                                            //dontforget to remove later on from losers deck
                    //alien.canPlayCard = false;
                    //player.canPlayCard = false;
                    Invoke("beginWar", 0f);
                    break;

                default:
                    Debug.Log("Error in calc of win type");
                    break;
            }

            clearSlot(slotNum);
        }
    }

    public void displayDialog(string dialog)
    {
        dialogText.gameObject.SetActive(true);
        dialogBox.gameObject.SetActive(true);
        dialogText.text = dialog;
    }

    public void removeDiaglog()
    {
        dialogText.gameObject.SetActive(false);
        dialogBox.gameObject.SetActive(false);
    }

    public void beginWar()
    {
        //war behavoir
        warOccurances++;

        alien.currWarSlot = 0;
        player.currWarSlot = 0;

        //alien.canPlayCard = true;
        //player.canPlayCard = true;

        //add dialog to instruct player on rules of war
        if (warOccurances == 1)//first time
        {
            alien.canPlayCard = false;
            player.canPlayCard = false;

            displayDialog("Hows the battle go you ask?\nDont you know its your peoples game.");
            Invoke("removeDiaglog", 1f);

            alien.canPlayCard = true;
            player.canPlayCard = true;
        }


        //Debug.Log("Begin war fin");
    }

    private void clearSlot(int slotNum)
    {//remove cards and reset aviablity
        removeDiaglog();

        alienDeck[slotNum].gameObject.SetActive(false);
        playerDeck[slotNum].gameObject.SetActive(false);

        alienDeck.Remove(alienDeck[slotNum]);
        playerDeck.Remove(playerDeck[slotNum]);

        playerAvaibleSlots[slotNum] = true;
        alienAvaibleSlots[slotNum] = true;

        player.canPlayCard = true;
        alien.canPlayCard = true;
    }
}
