using UnityEngine;

enum CardShape
{
    Spade = 0,
    Diamond,
    Heart,
    Clover,
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
        ret = Shape.ToString() + " " + Number;
        return ret;
    }

    public Card Rand()
    {
        Shape = (CardShape)Random.Range(0, 4);
        Number = Random.Range(1, 14);
        return this;
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