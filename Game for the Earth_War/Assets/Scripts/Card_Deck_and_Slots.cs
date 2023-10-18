using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//largely as base for player and alien objects

public class Card_Deck_and_Slots : MonoBehaviour
{
    [Header("Inscribed")]

    //Object References
    private Played_Cards played_Cards;

    //Gameplay
    private int playableDeckSize = 5;

    public int deckSize;
    public float suffleChance = 0.01f;

    public Transform[] cardSlots;
    public bool[] avaibleSlots;

    public List<Card> deck = new List<Card>();
    public List<Card> playableDeck = new List<Card>();

    //Text
    public TextMeshProUGUI deckSizeText;

    // Start is called before the first frame update
    void Start()
    {
        played_Cards = FindObjectOfType<Played_Cards>();
    }

    // Update is called once per frame
    void Update()
    {
        deckSizeText.text = getTotalDeckCount().ToString("#,0");
    }

    public void setUpSlots(bool isPlayer)
    {
        for (int i = cardSlots.Length - 1; i >= 0; i--)
        {
            Card newCard = deck[i];
            if (!isPlayer)
            {
                newCard.flip();
            }
            newCard.move(cardSlots[i].position);
            newCard.gameObject.SetActive(true);
            playableDeck.Add(newCard);
            avaibleSlots[i] = false;
            deck.Remove(newCard);
        }
    }

    public void fillCardSlots(bool isPlayer)
    {
        for (int i = 0; i < avaibleSlots.Length; i++)
        {
            if ((deck.Count > 0)) //account for playable deck and played card
            {
                Card newCard = deck[0];

                for (int slotNum = 0; slotNum < avaibleSlots.Length; slotNum++)
                {
                    if (avaibleSlots[slotNum])
                    {
                        if (!isPlayer)
                        {
                            newCard.flip();
                        }
                        newCard.move(cardSlots[slotNum].position);
                        newCard.gameObject.SetActive(true);
                        newCard.isPlayableCard = isPlayer;
                        playableDeck.Insert(slotNum, newCard);
                        avaibleSlots[slotNum] = false;
                        deck.Remove(newCard);//testing

                        if ((Random.Range(0, 100) / 100f) <= suffleChance)
                        {
                            suffle();
                        }
                        break;
                    }
                }
            }
        }
    }

    //fill card slots functionality at index
    public void fillCardSlot(int slotNum, bool isPlayer)
    {
        if ((deck.Count >= playableDeckSize)) //account for playable deck and played card
        {//DOESNT FULLY WORK CAUSES WIN COND NOT TO WORK SINCE LAT CARD IS NOT BEING PUT OUT
            if (avaibleSlots[slotNum])
            {
                //insure card is not already choosen or played
                Card newCard;
                int i = 0;
                do
                {
                    newCard = deck[i];
                    i++;
                } while ((i < deck.Count) && 
                ((playableDeck.Contains(newCard)) 
                || (played_Cards.playerDeck.Contains(newCard))
                || (played_Cards.alienDeck.Contains(newCard)) //add way to check with is player
                || (played_Cards.warPool.Contains(newCard))));

                if (!isPlayer)
                {
                    newCard.flip();
                }
                newCard.move(cardSlots[slotNum].position);
                newCard.gameObject.SetActive(true);
                newCard.isPlayableCard = isPlayer;
                playableDeck.Insert(slotNum, newCard);
                avaibleSlots[slotNum] = false;
                playableDeckSize = playableDeck.Count;
                
                if ((Random.Range(0,100)/100f) <= suffleChance)
                {
                    suffle();
                }
            }
        }
    }

    //functionality for adding card to deck
    public void addToDeck(Card inCard, bool isPlayer)
    {
        inCard.isPlayableCard = isPlayer;

        deck.Add(inCard);
    }

    public void suffle()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Card tempCard = deck[i];
            int randIndex = Random.Range(0, deck.Count - 1);
            deck[i] = deck[randIndex];
            deck[randIndex] = tempCard;

        }
    }

    public int getTotalDeckCount()
    {
        return (deck.Count + playableDeck.Count);
    }
}
