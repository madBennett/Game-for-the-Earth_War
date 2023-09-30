using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Inscribed")]

    private int deckSize = 52;
    private Alien alien;
    private User player;
    private Played_Cards played_Cards;

    public int defalutSlotNum = 1;
    public List<Card> deck = new List<Card>();

    // Start is called before the first frame update
    void Start()
    {
        alien = FindObjectOfType<Alien>();
        player = FindObjectOfType<User>();
        
        played_Cards = FindObjectOfType<Played_Cards>();
    }

    // Update is called once per frame
    void Update()
    {
        alien.playCard(defalutSlotNum);
        player.playCard(defalutSlotNum);

        played_Cards.findWinnerNormPlay(defalutSlotNum);//rn causes a bunch of timing issues inclusing not moving player card or flipping alien card
    }

    public List<Card> getStartingDeck(bool isPlayer)
    {
        List<Card> playerDeck = new List<Card>();

        for (int i = 0; i < deckSize / 2; i++)
        {
            Card tempCard = deck[Random.Range(0, deck.Count - 1)];//get rand card
            tempCard.gameObject.SetActive(false);
            tempCard.faceUp = isPlayer;
            tempCard.isPlayableCard = isPlayer;

            if (!isPlayer)
            {
                tempCard.flip();
            }

            playerDeck.Add(tempCard);
            deck.Remove(tempCard);
        }

        return playerDeck;
    }

    public IEnumerator wait(int secs)//doesnt work
    {
        yield return new WaitForSeconds(secs);
    }
}
