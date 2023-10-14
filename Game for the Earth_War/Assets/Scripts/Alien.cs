using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    [Header("Inscribed")]

    private GameManager gm;
    private Played_Cards played_Cards;

    public bool canPlayCard = true;
    public int currWarSlot = 0;
    public int warScore = 0;
    public Card_Deck_and_Slots card_Deck_And_Slots;
    public Transform[] playedSlots;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        played_Cards = FindObjectOfType<Played_Cards>();

        card_Deck_And_Slots.deck = gm.getStartingDeck(false);
        card_Deck_And_Slots.setUpSlots(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool playCard(int slotNum)
    {
        if (canPlayCard || played_Cards.alienAvaibleSlots[slotNum])
        {
            //select card
            int i = Random.Range(0, card_Deck_And_Slots.playableDeck.Count - 1);
            Card playedCard = card_Deck_And_Slots.playableDeck[i];

            //remove from playable deck and replace card
            card_Deck_And_Slots.playableDeck.Remove(playedCard);
            card_Deck_And_Slots.avaibleSlots[i] = true;


            //place played card into playedCard deck
            played_Cards.addToPlayed(false, playedCard, slotNum);
            //card_Deck_And_Slots.deck.Remove(playedCard);
            canPlayCard = false;

            card_Deck_And_Slots.fillCardSlot(i, false);

            return true;
        }
        return false;
    }
}
