using System.Collections.Generic;
using UnityEngine;

public enum CardShape
{
    Spade = 0,
    Diamond,
    Heart,
    Club,
}

public class Card
{
    CardShape Shape;
    int Number; // 1~10, 11 12 13 (K Q J), 

    public Card()
    {
        Shape = 0;
        Number = 0;
    }

    override public string ToString()
    {
        string ret;
        ret = Shape + " " + Number;
        return ret;
    }

    public Card Rand()
    {
        Shape = (CardShape)Random.Range(0, 4);
        Number = Random.Range(1, 14);
        return this;
    }

    public CardShape GetShape() { return Shape; }
    public int GetNumber() { return Number; }

    public string ToResourceString()
    {
        string shapeStr = "";
        switch(Shape)
        {
            case CardShape.Spade:
                shapeStr = "Spade";
                break;

            case CardShape.Diamond:
                shapeStr = "Diamond";
                break;

            case CardShape.Heart:
                shapeStr = "Heart";
                break;

            case CardShape.Club:
                shapeStr = "Club";
                break;
        }

        string numStr = Number.ToString("00");

        return shapeStr + numStr;
    }

    public static bool operator ==(Card c1, Card c2) {
        return (c1.Shape == c2.Shape) && (c1.Number == c2.Number);
    }

    public static bool operator !=(Card c1, Card c2)
    {
        return (c1.Number != c2.Number) || (c1.Shape != c2.Shape);
    }
     public override bool Equals(object o)
    {
        return o.ToString() == ToString();
    }

    public override int GetHashCode()
    {
        return 0;
    }
}

public enum DeckMadeType
{
    None = 0,
    OnePair,
    TwoPair,
    ThreeOfAKind,
    Straight,
    BackStraight,
    Mountain,
    Flush,
    FullHouse,
    FourOfAKind,
    StraightFlush,
    BackStraightFlush,
    RoyalStraightFlush,
}
public class CardUtil : Card 
{
    //TODO : 페어나면 앞으로 밀어줘야 함
    public static List<Card> SortCardList(List<Card> cards)
    {
        for(int i=0;i<cards.Count;i++)
        {
            for(int j=0;j<cards.Count -1;j++)
            {
                if(cards[j].GetNumber().CompareTo(cards[j+1].GetNumber()) > 0)
                {
                    (cards[j+1], cards[j]) = (cards[j], cards[j+1]);
                } else if (cards[j].GetNumber().CompareTo(cards[j + 1].GetNumber()) == 0)
                {
                    //같은숫자면 S H D C 순으로 정렬 // 미국에선 SHDC 고 SDHC은 한국 로컬룰인듯;
                    if(cards[j].GetShape().CompareTo(cards[j + 1].GetShape()) > 0)
                    {
                        (cards[j + 1], cards[j]) = (cards[j], cards[j + 1]);
                    }
                }
            }
        }
        return cards;
    }

    
    /**
     * Essential : With Sorted Deck
     */
    public static DeckMadeType Evaluate(List<Card> deck)
    {
        if (deck.Count != 5)
        {
            Debug.LogError("Deck Size is not 5");
            return DeckMadeType.None;
        }
        
        //Check Royal Straight Flush
        if (deck[0].GetNumber() == 1 && deck[1].GetNumber() == 10 && deck[2].GetNumber() == 11 && deck[3].GetNumber() == 12 && deck[4].GetNumber() == 13)
        {
            if (deck[0].GetShape() == deck[1].GetShape() && deck[1].GetShape() == deck[2].GetShape() && deck[2].GetShape() == deck[3].GetShape() && deck[3].GetShape() == deck[4].GetShape())
            {
                return DeckMadeType.RoyalStraightFlush;
            }
        }
        
        //Check Straight Flush
        if (deck[0].GetNumber() + 1 == deck[1].GetNumber() && deck[1].GetNumber() + 1 == deck[2].GetNumber() && deck[2].GetNumber() + 1 == deck[3].GetNumber() && deck[3].GetNumber() + 1 == deck[4].GetNumber())
        {
            if (deck[0].GetShape() == deck[1].GetShape() && deck[1].GetShape() == deck[2].GetShape() && deck[2].GetShape() == deck[3].GetShape() && deck[3].GetShape() == deck[4].GetShape())
            {
                return DeckMadeType.StraightFlush;
            }
        }
        
        //Check Four of a Kind
        if (deck[0].GetNumber() == deck[1].GetNumber() && deck[1].GetNumber() == deck[2].GetNumber() && deck[2].GetNumber() == deck[3].GetNumber())
        {
            return DeckMadeType.FourOfAKind;
        }
        
        //Check Full House
        if (deck[0].GetNumber() == deck[1].GetNumber() && deck[1].GetNumber() == deck[2].GetNumber() && deck[3].GetNumber() == deck[4].GetNumber())
        {
            return DeckMadeType.FullHouse;
        }
        
        //Check Flush
        if (deck[0].GetShape() == deck[1].GetShape() && deck[1].GetShape() == deck[2].GetShape() && deck[2].GetShape() == deck[3].GetShape() && deck[3].GetShape() == deck[4].GetShape())
        {
            return DeckMadeType.Flush;
        }
        
        //Check BackStraight
        if (deck[0].GetNumber() == 1 && deck[1].GetNumber() == 2 && deck[2].GetNumber() == 3 && deck[3].GetNumber() == 4 && deck[4].GetNumber() == 5)
        {
            return DeckMadeType.BackStraight;
        }
        
        //Check Straight
        if (deck[0].GetNumber() + 1 == deck[1].GetNumber() && deck[1].GetNumber() + 1 == deck[2].GetNumber() && deck[2].GetNumber() + 1 == deck[3].GetNumber() && deck[3].GetNumber() + 1 == deck[4].GetNumber())
        {
            return DeckMadeType.Straight;
        }
        
        //Check Three of a Kind
        if (deck[0].GetNumber() == deck[1].GetNumber() && deck[1].GetNumber() == deck[2].GetNumber())
        {
            return DeckMadeType.ThreeOfAKind;
        }
        
        //Check Two Pair
        if (deck[0].GetNumber() == deck[1].GetNumber() && deck[2].GetNumber() == deck[3].GetNumber())
        {
            return DeckMadeType.TwoPair;
        }
        
        //Check One Pair
        if (deck[0].GetNumber() == deck[1].GetNumber())
        {
            return DeckMadeType.OnePair;
        }

        return DeckMadeType.None;
    }

}