using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    [Header("Inscribed")]

    //Object References
    private GameManager gm;
    private Played_Cards played_Cards;

    public Card_Deck_and_Slots card_Deck_And_Slots;

    //Gameplay
    public bool canPlayCard = true;
    public int currWarSlot = 0;
    public int warScore = 0;
    public List<int> indexes = new List<int>();

    public Transform[] playedSlots;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        played_Cards = FindObjectOfType<Played_Cards>();

        card_Deck_And_Slots.deck = gm.getStartingDeck(false);
        card_Deck_And_Slots.setUpSlots(false);

        for (int i = 0; i< card_Deck_And_Slots.playableDeck.Length; i++)
        {
            indexes.Add(i);
        }
    }

    public bool playCard(int slotNum)
    {
        if ((canPlayCard && played_Cards.alienAvaibleSlots[slotNum]) && (card_Deck_And_Slots.getTotalDeckCount() > 0))
        {
            //select card
            int i = indexes[Random.Range(0, indexes.Count - 1)];
            Card playedCard = card_Deck_And_Slots.playableDeck[i];

            if (!System.Object.ReferenceEquals(playedCard, null))
            {

                indexes.Remove(i);

                //remove from playable deck and replace card
                card_Deck_And_Slots.playableRemove(playedCard);
                card_Deck_And_Slots.avaibleSlots[i] = true;


                //place played card into playedCard deck
                played_Cards.addToPlayed(false, playedCard, slotNum);
                canPlayCard = false;

                if (!gm.isWar || currWarSlot >= played_Cards.alienAvaibleSlots.Length - 1)
                {
                    //reset indexes
                    indexes.Clear();
                    for (int j = 0; j < card_Deck_And_Slots.avaibleSlots.Length; j++)
                    {
                        if (!card_Deck_And_Slots.avaibleSlots[j])
                        {
                            indexes.Add(j);
                        }
                    }
                }
                return true;
            }
        }
        return false;
    }
}
