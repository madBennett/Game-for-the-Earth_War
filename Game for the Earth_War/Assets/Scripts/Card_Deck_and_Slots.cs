using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//largely as base for player and alien objects

public class Card_Deck_and_Slots : MonoBehaviour
{
    [Header("Inscribed")]
    private int playableDeckSize = 5;

    public List<Card> deck = new List<Card>();
    public List<Card> playableDeck = new List<Card>();
    public Transform[] cardSlots;
    public bool[] avaibleSlots;
    public TextMeshProUGUI deckSizeText;

    // Start is called before the first frame update
    void Start()
    {
        //deckSizeText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        deckSizeText.text = deck.Count.ToString("#,0");
    }

    public void setUpSlots()
    {
        for (int i = 0; i < cardSlots.Length; i++)
        {
            Card newCard = deck[i];
            newCard.move(cardSlots[i].position);
            newCard.gameObject.SetActive(true);
            playableDeck.Add(newCard);
            avaibleSlots[i] = false;
        }
    }

    //fill card slots functionality
    public void fillCardSlot()
    {
        if(deck.Count >= playableDeckSize)
        {
            Card randCard = deck[Random.Range(0, deck.Count-1)];
            for (int i = 0; i < avaibleSlots.Length; i++)
            {
                if (avaibleSlots[i])
                {
                    randCard.move(cardSlots[i].position);
                    randCard.gameObject.SetActive(true);
                    playableDeck.Add(randCard);
                    avaibleSlots[i] = false;
                    //return;  //since after war this could be the best option
                }
            }
        }
    }

    //fill card slots functionality at index
    public void fillCardSlot(int slotNum)
    {
        if (deck.Count > playableDeckSize)
        {
            if (avaibleSlots[slotNum])
            {
                //insure card is not already choosen
                Card randCard = deck[Random.Range(0, deck.Count - 1)];
                while(playableDeck.Contains(randCard))
                {
                    randCard = deck[Random.Range(0, deck.Count - 1)];
                }

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
        if (!isPlayer)
        {
            inCard.flip();
        }
        inCard.moveToOgPos();
        inCard.isPlayableCard = isPlayer;
        inCard.faceUp = isPlayer;

        deck.Add(inCard);
    }
}
