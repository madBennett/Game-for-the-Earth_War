using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
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

        card_Deck_And_Slots.deck = gm.getStartingDeck(true);
        card_Deck_And_Slots.setUpSlots(true);
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


                        //place played card into playedCard deck
                        canPlayCard = false;
                        played_Cards.addToPlayed(true, playedCard, slotNum);
                        //card_Deck_And_Slots.deck.Remove(playedCard);
                        playedCard.isPlayableCard = false;

                        //add delay
                        //Invoke("findWinnerNormPlay", 3f);
                        gm.startTime = Time.time;
                        
                        //played_Cards.opCardsPlayed(slotNum);

                        card_Deck_And_Slots.fillCardSlot(i, true);

                        return true;
                    }
                }
            }
        }
        return false;
    }
}
