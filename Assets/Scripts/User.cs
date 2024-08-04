using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    private List<Card> hand;
    public int remainChance = 10;

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

    public void OnNewPhase()
    {
        remainChance = 10;
    }

    public List<Card> BatchDraw(int batchSize)
    {
        if (remainChance <= 0)
        {
            return hand;
        }
        remainChance--;

        if (batchSize > 5)
        {
            //�����α�
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

    public List<Card> ChangeOne(List<Card> retCards, int index)
    {
        if (remainChance <= 0)
        {
            return hand;
        }
        remainChance--;

        int BATCH_SIZE = 5;
        if (retCards.Count != BATCH_SIZE)
        {
            Debug.LogError("User::ChangeOne retCards length is not 5");
            return null;
        }

        retCards.RemoveAt(index);
        
        while (true)
        {
            Card newCard = new Card().Rand();

            if (retCards.Contains(newCard))
            {
                continue;
            }

            retCards.Insert(index, newCard);
            break;
        }

        UpdateHand(retCards);
        return retCards;
    }
}
