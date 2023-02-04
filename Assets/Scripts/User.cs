using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    private List<Card> hand;

    public User()
    {
        hand = new List<Card>();
    }


    public List<Card> GetHand()
    {
        return hand;
    }

    public List<Card> UpdateHand(List<Card> newHand)
    {
        hand = newHand;
        return hand;
    }


    public Card Draw()
    {
        return new Card().Rand();
    }

    public List<Card> BatchDraw(int batchSize)
    {
        if (batchSize > 5)
        {
            //에러로그
            Debug.LogError("Draw Count Over Max Hand size " + 5);
            return null;
        }

        List<Card> retCards = new List<Card>();

        while (true)
        {
            Card newCard = new Card().Rand();

            if (retCards.Contains(newCard))
            {
                continue;
            }

            retCards.Add(newCard);

            if (retCards.Count == batchSize)
            {
                break;
            }
        }

        UpdateHand(retCards);


        return retCards;
    }
}
