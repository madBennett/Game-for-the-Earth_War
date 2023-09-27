using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//largely as base for player and alien objects

public class Card_Deck_and_Slots : MonoBehaviour
{
    [Header("Inscribed")]


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
            newCard.transform.position = cardSlots[i].position;
            newCard.setPos(cardSlots[i].position);
            playableDeck.Add(newCard);
            avaibleSlots[i] = false;
        }
    }

    //fill card slots functionality
    public void fillCardSlot()
    {
        if(deck.Count >= 1)
        {
            Card randCard = deck[Random.Range(0, deck.Count-1)];
            for (int i = 0; i < avaibleSlots.Length; i++)
            {
                if (avaibleSlots[i])
                {
                    randCard.transform.position = cardSlots[i].position;
                    randCard.setPos(cardSlots[i].position);
                    playableDeck.Add(randCard);
                    avaibleSlots[i] = false;
                    return;
                }
            }
        }
    }

    //fill card slots functionality at index
    public void fillCardSlot(int slotNum)
    {
        if (deck.Count >= 1)
        {
            Card randCard = deck[Random.Range(0, deck.Count - 1)];
            if (avaibleSlots[slotNum])
            {
                randCard.transform.position = cardSlots[slotNum].position;
                randCard.setPos(cardSlots[slotNum].position);
                playableDeck.Insert(slotNum, randCard);
                avaibleSlots[slotNum] = false;
                return;
            }
        }
    }
}
