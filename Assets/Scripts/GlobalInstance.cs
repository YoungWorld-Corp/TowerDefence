using UnityEngine;

public class GlobalInstance
{
    private static GlobalInstance instance;
    private static User user;

    private GlobalInstance()
    {
        user = new User();
    }

    // public static method to access private instance from anywhere!
    public static GlobalInstance GetInstance()
    {
        if (instance == null)
        {
            instance = new GlobalInstance();
        }
        return instance;
    }

    public User GetUser()
    {
        return user;
    }

}

public class User
{
    private Card[] hand;

    public User()
    {
        hand = new Card[5];
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
}