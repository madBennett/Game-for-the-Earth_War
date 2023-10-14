using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//largely as base for player and alien objects

public class Card_Deck_and_Slots : MonoBehaviour
{
    [Header("Inscribed")]
    private int playableDeckSize = 5;
    private Played_Cards played_Cards;

    public List<Card> deck = new List<Card>();
    public List<Card> playableDeck = new List<Card>();
    public Transform[] cardSlots;
    public bool[] avaibleSlots;
    public TextMeshProUGUI deckSizeText;

    // Start is called before the first frame update
    void Start()
    {
        played_Cards = FindObjectOfType<Played_Cards>();
    }

    // Update is called once per frame
    void Update()
    {
        deckSizeText.text = deck.Count.ToString("#,0");
    }

    public void setUpSlots(bool isPlayer)
    {
        for (int i = 0; i < cardSlots.Length; i++)
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
        }
    }

    //fill card slots functionality at index
    public void fillCardSlot(int slotNum, bool isPlayer)
    {
        if (deck.Count > playableDeckSize + 1)//account for playable deck and played card
        {//DOESNT FULLY WORK CAUSES WIN COND NOT TO WORK SINCE LAT CARD IS NOT BEING PUT OUT
            if (avaibleSlots[slotNum])
            {
                //insure card is not already choosen or played
                Card randCard;
                do//causes infinate loop when deck too small
                {
                    randCard = deck[Random.Range(0, deck.Count - 1)];
                } while ((playableDeck.Contains(randCard)) || (played_Cards.playerDeck.Contains(randCard)) 
                || (played_Cards.alienDeck.Contains(randCard)) || (played_Cards.warPool.Contains(randCard)));

                int chosenSlot = slotNum;
                /*  Test later
                if (slotNum >= 1 && slotNum > avaibleSlots.Length/2)
                {
                    chosenSlot = (avaibleSlots[slotNum - 1]) ? slotNum - 1 : slotNum;
                }
                else if (slotNum < avaibleSlots.Length - 1 && slotNum < avaibleSlots.Length/2)
                {
                    chosenSlot = (avaibleSlots[slotNum + 1]) ? slotNum + 1 : slotNum;
                }
                */
                if (!isPlayer)
                {
                    randCard.flip();
                }
                randCard.move(cardSlots[chosenSlot].position);
                randCard.gameObject.SetActive(true);
                playableDeck.Insert(chosenSlot, randCard);
                avaibleSlots[chosenSlot] = false;
                return;
            }
        }
    }

    //functionality for adding card to deck
    public void addToDeck(Card inCard, bool isPlayer)
    {
        inCard.isPlayableCard = isPlayer;

        deck.Add(inCard);
    }
}
