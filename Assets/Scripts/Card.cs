using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    public CardShape Shape { get; set; }
    public int Number { get; set; } // 1~10, 11 12 13 (K Q J), 

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
        Number = Random.Range(6, 14);
        if (Number == 6) Number = 1;
        return this;
    }

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
    None = -1,
    Top = 0,
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

public struct DeckMade
{
    public DeckMadeType deckMadeType;
    public int highestNumber;
    public DeckMade(DeckMadeType InDeckMadeType, int InHighestNumber)
    {
        deckMadeType = InDeckMadeType;
        highestNumber = InHighestNumber;
    }

    public string GetHighestNumberText()
    {
        if (highestNumber == 1) return "A";
        else if (highestNumber == 11) return "J";
        else if (highestNumber == 12) return "Q";
        else if (highestNumber == 13) return "K";
        else return highestNumber.ToString();
    }
}

public class CardUtil : Card 
{
    //TODO : 페어나면 앞으로 밀어줘야 함
    public static List<Card> SortCardList(List<Card> cards)
    {
        // Group the cards by number and count
        var groups = cards.GroupBy(x => x.Number)
            .Select(x => new { Number = x.Key, Count = x.Count() });

        // Sort the groups by the count in descending order and then by the number in ascending order
        var sortedGroups = groups.OrderByDescending(x => x.Count)
            .ThenBy(x => x.Number);

        // Create a new list of cards by expanding the sorted groups
        List<Card> sortedHand = new List<Card>();
        foreach (var group in sortedGroups)
        {
            var hands = cards.Where(x => x.Number == group.Number);
            sortedHand.AddRange(hands);
        }

        return sortedHand;  
    }

    
    /**
     * Essential : With Sorted Deck
     */
    public static DeckMade Evaluate(ref List<Card> InDeck)
    {
        if (InDeck.Count != 5)
        {
            Debug.LogError("Deck Size is not 5");
            return new DeckMade(DeckMadeType.None, 0);
        }

        List<Card> deck = SortCardList(InDeck);
        
        //Check Royal Straight Flush
        if (deck[0].Number == 1 && deck[1].Number == 10 && deck[2].Number == 11 && deck[3].Number == 12 && deck[4].Number == 13)
        {
            if (deck[0].Shape == deck[1].Shape && deck[1].Shape == deck[2].Shape && deck[2].Shape == deck[3].Shape && deck[3].Shape == deck[4].Shape)
            {
                return new DeckMade(DeckMadeType.RoyalStraightFlush, 1);
            }
        }
        
        //Check Back Straight Flush
        if (deck[0].Number == 1 && deck[1].Number == 2 && deck[2].Number == 3 && deck[3].Number == 4 && deck[4].Number == 5)
        {
            if (deck[0].Shape == deck[1].Shape && deck[1].Shape == deck[2].Shape && deck[2].Shape == deck[3].Shape && deck[3].Shape == deck[4].Shape)
            {
                return new DeckMade(DeckMadeType.BackStraightFlush, 1);
            }
        }
        
        //Check Straight Flush
        if (deck[0].Number + 1 == deck[1].Number && deck[1].Number + 1 == deck[2].Number && deck[2].Number + 1 == deck[3].Number && deck[3].Number + 1 == deck[4].Number)
        {
            if (deck[0].Shape == deck[1].Shape && deck[1].Shape == deck[2].Shape && deck[2].Shape == deck[3].Shape && deck[3].Shape == deck[4].Shape)
            {
                return new DeckMade(DeckMadeType.StraightFlush, deck[4].Number);
            }
        }
        
        //Check Four of a Kind
        if (deck[0].Number == deck[1].Number && deck[1].Number == deck[2].Number && deck[2].Number == deck[3].Number)
        {
            return new DeckMade(DeckMadeType.FourOfAKind, deck[0].Number);
        }
        
        //Check Full House
        if (deck[0].Number == deck[1].Number && deck[1].Number == deck[2].Number && deck[3].Number == deck[4].Number)
        {
            return new DeckMade(DeckMadeType.FullHouse, deck[0].Number);
        }
        
        //Check Flush
        if (deck[0].Shape == deck[1].Shape && deck[1].Shape == deck[2].Shape && deck[2].Shape == deck[3].Shape && deck[3].Shape == deck[4].Shape)
        {
            return new DeckMade(DeckMadeType.Flush, deck[4].Number);
        }
        
        //Check Mountain
        if (deck[0].Number == 10 && deck[1].Number == 11 && deck[2].Number == 12 && deck[3].Number == 13 && deck[4].Number == 1)
        {
            return new DeckMade(DeckMadeType.Mountain, 1);
        }
        
        //Check BackStraight
        if (deck[0].Number == 1 && deck[1].Number == 2 && deck[2].Number == 3 && deck[3].Number == 4 && deck[4].Number == 5)
        {
            return new DeckMade(DeckMadeType.BackStraight, 1);
        }
        
        //Check Straight
        if (deck[0].Number + 1 == deck[1].Number && deck[1].Number + 1 == deck[2].Number && deck[2].Number + 1 == deck[3].Number && deck[3].Number + 1 == deck[4].Number)
        {
            return new DeckMade(DeckMadeType.Straight, deck[4].Number);
        }
        
        //Check Three of a Kind
        if (deck[0].Number == deck[1].Number && deck[1].Number == deck[2].Number)
        {
            return new DeckMade(DeckMadeType.ThreeOfAKind, deck[0].Number);
        }
        
        //Check Two Pair
        if (deck[0].Number == deck[1].Number && deck[2].Number == deck[3].Number)
        {
            return new DeckMade(DeckMadeType.TwoPair, Mathf.Max(deck[0].Number, deck[2].Number));
        }
        
        //Check One Pair
        if (deck[0].Number == deck[1].Number)
        {
            return new DeckMade(DeckMadeType.OnePair, deck[0].Number);
        }

        return new DeckMade(DeckMadeType.Top, deck[0].Number == 1 ? 1 : deck[4].Number);
    }
}