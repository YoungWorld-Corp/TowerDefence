using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    private Card[] hand;

    public User()
    {
        hand = null;
    }


    public Card[] GetHand()
    {
        return hand;
    }

    public Card[] UpdateHand(Card[] newHand)
    {
        hand = newHand;
        return hand;
    }


    public Card Draw()
    {
        return drawCard();
    }

    public Card[] BatchDraw(int batchSize)
    {
        if (batchSize > 5)
        {
            //�����α�
            Debug.LogError("Draw Count Over Max Hand size " + 5);
            return null;
        }

        Card[] retCards = new Card[batchSize];
        for (int i = 0; i < batchSize; i++)
        {
            retCards[i] = drawCard();
        }

        UpdateHand(retCards);


        return retCards;
    }

    Card drawCard()
    {
        Card[] hand = GetHand();

        while (true)
        {
            Card newCard = new Card().Rand();


            //ó���� ���� �� �ִ�.
            if (hand != null)
            {
                foreach (Card handsCard in hand)
                {
                    if (handsCard == newCard)
                    {
                        newCard.Rand();
                        continue;
                    }
                }
            }

            return newCard;
        }
    }
}
