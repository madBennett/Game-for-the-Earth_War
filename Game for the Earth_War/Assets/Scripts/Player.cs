using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Inscribed")]

    private GameManager gm;
    private Played_Cards played_Cards;

    public bool canPlayCard = true;
    public Card_Deck_and_Slots card_Deck_And_Slots;
    public Transform[] playedSlots;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        played_Cards = FindObjectOfType<Played_Cards>();

        card_Deck_And_Slots.deck = gm.getStartingDeck(true);
        card_Deck_And_Slots.setUpSlots();
    }

    // Update is called once per frame
    void Update()
    {
        playCard(1);
    }

    public void playCard(int slotNum)
    {
        if (canPlayCard)
        {
            //select card
            if (Input.GetMouseButtonDown(0))
            {
                //check if card clicked
                for (int i = 0; i < card_Deck_And_Slots.playableDeck.Count; i++)
                {
                    Card playedCard = card_Deck_And_Slots.playableDeck[i];
                    if (playedCard.wasMouseOver())
                    {
                        //remove from playable deck and replace card
                        card_Deck_And_Slots.playableDeck.Remove(playedCard);
                        card_Deck_And_Slots.avaibleSlots[i] = true;
                        card_Deck_And_Slots.fillCardSlot(i);

                        //place played card into playedCard deck
                        played_Cards.addToPlayed(true, playedCard, slotNum);
                        card_Deck_And_Slots.deck.Remove(playedCard);
                        canPlayCard = false;
                        break;
                    }
                }
            }

            //logic for war???? maybe goes in game manager???
        }
    }
}
