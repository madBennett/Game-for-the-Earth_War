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
    public int deckSize;
    public float suffleChance = 0.01f;

    public Transform[] cardSlots;
    public bool[] avaibleSlots;

    public List<Card> deck = new List<Card>();
    public Card[] playableDeck;

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
            playableDeck[i] = newCard;
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
                        newCard.isPlayableCard = isPlayer;
                        playableDeck[slotNum] = newCard;
                        avaibleSlots[slotNum] = false;
                        deck.Remove(newCard);

                        newCard.gameObject.SetActive(true);

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

    public void playableRemoveAt(int index)
    {
        playableDeck[index] = null;
    }

    public void playableRemove(Card card)
    {
        for (int i = 0; i < playableDeck.Length; i++)
        {
            if (System.Object.ReferenceEquals(playableDeck[i], card))
            {
                playableDeck[i] = null;
                return;
            }
        }
    }

    public int getPlayableSize()
    {
        int count = 0;
        for (int i = 0; i < playableDeck.Length; i++)
        {
            if (!System.Object.ReferenceEquals(playableDeck[i], null))
            {
                count++;
            }
        }

        return count;
    }

    public int getTotalDeckCount()
    {
        return (deck.Count + getPlayableSize());//account for playable deck size
    }
}
