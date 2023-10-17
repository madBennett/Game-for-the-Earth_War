using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Inscribed")]

    //Object Refrences
    private Alien alien;
    private User player;
    private Played_Cards played_Cards;

    //Gameplay
    private int deckSize = 52;

    public float waitTime = 2f;
    public float startTime = 999;

    public int defalutSlotNum = 1;
    public bool checkCards = false; //prevents cards being checked mutliple times
    public bool isWar = false;

    public List<Card> deck = new List<Card>();

    //Audio
    public float volume = 1f;

    //Timer
    public bool isTimed = false;
    public TextMeshProUGUI timer;
    public float timeLeft = 300f; //in seconds

    //God Mode
    public bool isGodMode = false;

    void Start()
    {
        alien = FindObjectOfType<Alien>();
        player = FindObjectOfType<User>();
        played_Cards = FindObjectOfType<Played_Cards>();
    }

    void Update()
    {
        if (((!(player.card_Deck_And_Slots.deck.Count == 0) && !(alien.card_Deck_And_Slots.deck.Count == 0))
            && !(isTimed && timeLeft < 0)))
        {
            if (!isWar)
            {
                alien.playCard(defalutSlotNum);
                player.playCard(defalutSlotNum);

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

                    played_Cards.finishWarPlay(player.warScore > alien.warScore);
                }
            }
        }
        else
        {
            //exit screens
            alien.canPlayCard = false;
            player.canPlayCard = false;

            if ((alien.card_Deck_And_Slots.deck.Count == 0) 
                || (isTimed && player.card_Deck_And_Slots.deck.Count > alien.card_Deck_And_Slots.deck.Count))
            {
                overAllPlayerWin();
            }
            else
            {
                overAllAlienWin();
            }
        }

        if (isTimed)
        {
            if (timeLeft > 0)
            {
                
                int minLeft = Mathf.FloorToInt(timeLeft / 60);
                int secLeft = Mathf.FloorToInt(timeLeft % 60);

                timer.text = string.Format("{0:00} : {1:00}", minLeft, secLeft);
                timeLeft -= Time.deltaTime;
            }
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
            tempCard.setVolume(volume);

            playerDeck.Add(tempCard);
            deck.Remove(tempCard);
        }

        return playerDeck;
    }

    public void overAllAlienWin()
    {
        SceneManager.LoadScene("Gameover_Player Loss");
    }

    public void overAllPlayerWin()
    {
        SceneManager.LoadScene("Gameover_Player Win");
    }
}
