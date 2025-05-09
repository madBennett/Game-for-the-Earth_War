using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//largely as base for player and alien objects

public class Played_Cards : MonoBehaviour
{
    [Header("Inscribed")]

    //Object References
    private Alien alien;
    private User player;
    private GameManager gm;

    public AudioSource audioSource;

    //GamePlay
    public int defalutSlotNum = 1;
    public WinType winType = WinType.ERROR;

    public Transform[] playerCardSlots;
    public Transform[] alienCardSlots;
    
    public bool[] playerAvaibleSlots;
    public bool[] alienAvaibleSlots;

    public List<Card> playerDeck = new List<Card>();
    public List<Card> alienDeck = new List<Card>();
    public List<Card> warPool = new List<Card>();

    //Text
    public TextMeshProUGUI dialogText;
    public GameObject dialogBox;
    public TextMeshProUGUI godModeAlienCard;

    //Audio
    public AudioClip happyAlien;
    public AudioClip warAudio;
    public List<AudioClip> AngryAlien = new List<AudioClip>();

    public enum WinType
    {
        ERROR,
        ALIEN_WIN,
        PLAYER_WIN,
        WAR
    }

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
            if (gm.isGodMode)
            {
                godModeAlienCard.text += "\n" + playedCard;
            }
        }

        if (gm.isWar)
        {
            findWinWarPlay(slotNum);
        }
        else
        {
            findWinNormPlay(slotNum);
        }
    }

    public void beginWar()
    {
        if (player.card_Deck_And_Slots.getPlayableSize() < playerCardSlots.Length)
        {
            gm.overAllAlienWin();
        }
        else if (alien.card_Deck_And_Slots.getPlayableSize() < alienCardSlots.Length)
        {
            gm.overAllPlayerWin();
        }
        else
        {
            alien.currWarSlot = 0;
            player.currWarSlot = 0;

            alien.warScore = 0;
            player.warScore = 0;

            gm.checkCards = true;
        }
    }

    public void findWinNormPlay(int slotNum)
    {
        if ((!alienAvaibleSlots[slotNum] && !playerAvaibleSlots[slotNum]) 
            && (alienDeck.Count > 0 && playerDeck.Count > 0))
        {
            Card alienCard = alienDeck[slotNum];
            Card playerCard = playerDeck[slotNum];

            alienCard.flip();

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
                playHappyAlienDialog();
            }
            else
            {
                //player win
                winType = WinType.PLAYER_WIN;
                displayDialog("Darn, you win");
                playAngryAlienAudio();
            }

            gm.checkCards = true;
        }
    }

    public void findWinWarPlay(int slotNum)
    {
        WinType winType = WinType.ERROR;
        
        if ((!alienAvaibleSlots[slotNum] && !playerAvaibleSlots[slotNum])
            && (alienDeck.Count > 0 && playerDeck.Count > 0))
        {
            Card alienCard = alienDeck[slotNum];
            Card playerCard = playerDeck[slotNum];

            alienCard.flip();

            if (alienCard.num == playerCard.num)
            {
                //check suit type
                if (alienCard.suit > playerCard.suit)
                {
                    //alien win
                    winType = WinType.ALIEN_WIN;
                }
                else
                {
                    //player win
                    winType = WinType.PLAYER_WIN;
                }
            }
            else if (alienCard.num > playerCard.num)
            {
                //alien win
                winType = WinType.ALIEN_WIN;
            }
            else
            {
                //player win
                winType = WinType.PLAYER_WIN;
            }

            if (winType == WinType.ALIEN_WIN)
            {
                alien.warScore++;
            }
            else
            {
                player.warScore++;
            }
            displayDialog("Its\n" + player.warScore + "to " + alien.warScore);
            if (player.warScore == 3)
            {
                playAngryAlienAudio();
            }
            else
            {
                playHappyAlienDialog();
            }
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
                    if (!gm.isGodMode)
                    {
                        player.card_Deck_And_Slots.addToDeck(alienCard, true);
                        player.card_Deck_And_Slots.addToDeck(playerCard, true);
                    }
                    winType = WinType.ERROR;
                    break;

                case WinType.ALIEN_WIN:
                    //alien win
                    if (!gm.isGodMode)
                    {
                        alien.card_Deck_And_Slots.addToDeck(playerCard, false);
                        alien.card_Deck_And_Slots.addToDeck(alienCard, false);
                    }
                    winType = WinType.ERROR;
                    break;

                case WinType.WAR:
                    //war
                    warPool.Add(alienCard);
                    warPool.Add(playerCard);
                    alien.card_Deck_And_Slots.deck.Remove(warPool[0]);
                    player.card_Deck_And_Slots.deck.Remove(warPool[1]);
                    playWarDialog();
                    //move cards
                    beginWar();
                    break;

                default:
                    Debug.Log("Error in calc of win type");
                    break;
            }

            clearSlot(slotNum);

            alien.card_Deck_And_Slots.fillCardSlots(false);
            player.card_Deck_And_Slots.fillCardSlots(true);
        }
    }

    public void finishWarPlay(bool playerWin)
    {
        if (!gm.isGodMode)
        {
            if (playerWin)
            {

                for (int i = warPool.Count - 1; i >= 0; i--)
                {
                    player.card_Deck_And_Slots.addToDeck(warPool[i], true);
                }

                for (int i = 0; i < alienAvaibleSlots.Length; i++)
                {
                    player.card_Deck_And_Slots.addToDeck(alienDeck[i], true);
                    player.card_Deck_And_Slots.addToDeck(playerDeck[i], true);
                }
            }
            else
            {
                for (int i = warPool.Count - 1; i >= 0; i--)
                {
                    alien.card_Deck_And_Slots.addToDeck(warPool[i], false);
                }

                for (int i = 0; i < playerAvaibleSlots.Length; i++)
                {
                    alien.card_Deck_And_Slots.addToDeck(playerDeck[i], false);
                    alien.card_Deck_And_Slots.addToDeck(alienDeck[i], false);
                }
            }
        }

        Invoke("clearPostWar", 1f);
        winType = WinType.ERROR;
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

    public void playAngryAlienAudio()
    {
        audioSource.PlayOneShot(AngryAlien[Random.Range(0, AngryAlien.Count - 1)], gm.volume);
    }

    public void playHappyAlienDialog()
    {
        audioSource.PlayOneShot(happyAlien, gm.volume);
    }

    public void playWarDialog()
    {
        audioSource.PlayOneShot(warAudio, gm.volume);
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

        if (gm.isGodMode)
        {
            godModeAlienCard.text = "Alien Played: ";
        }
    }

    private void clearPostWar()
    {
        for (int i = playerAvaibleSlots.Length - 1; i >= 0; i--)
        {
            clearSlot(i);
        }

        for (int i = warPool.Count - 1; i >= 0; i--)
        {
            warPool.RemoveAt(i);
        }
        gm.isWar = false;

        removeDiaglog();

        alien.card_Deck_And_Slots.fillCardSlots(false);
        player.card_Deck_And_Slots.fillCardSlots(true);
    }
}
