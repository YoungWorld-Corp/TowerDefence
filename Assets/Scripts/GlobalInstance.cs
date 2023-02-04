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

