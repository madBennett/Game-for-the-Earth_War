using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    [Header("Inscribed")]

    private GameManager gm;
    private Played_Cards played_Cards;

    public Card_Deck_and_Slots card_Deck_And_Slots;
    public Transform[] playedSlots;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        played_Cards = FindObjectOfType<Played_Cards>();

        card_Deck_And_Slots.deck = gm.getStartingDeck(false);
        card_Deck_And_Slots.setUpSlots();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playCard()
    {
        //select card

        //move card to play area

        //logic for war???? maybe goes in game manager???
    }
}
