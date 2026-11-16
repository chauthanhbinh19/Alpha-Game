using System.Collections;
using System.Collections.Generic;
public class User
{
    // public GameObject signInPanel;
    // public GameObject namePanel;
    public string Id;
    public string Name;
    public string Image;
    public string Border;
    public int Level;
    public double Experiment;
    public int Vip;
    public double Power;
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public int LikeCount { get; set; } = 0;
    public static string CurrentUserId { get; set; }
    public static string CurrentUserName { get; set; }
    public static int CurrentUserLevel { get; set; }
    public static string CurrentUserAvatar { get; set; }
    public static string CurrentUserBorder { get; set; }
    public static double CurrentUserPower { get; set; }
    public static string SavedUsername;
    public static string SavedPassword;
    public List<Currencies> Currencies { get; set; }
    public static UserPowers UserPower { get; set; } = new UserPowers();
    public static List<CardAdmirals> CardAdmirals { get; set; } = new List<CardAdmirals>();
    public static List<CardCaptains> CardCaptains { get; set; } = new List<CardCaptains>();
    public static List<CardColonels> CardColonels { get; set; } = new List<CardColonels>();
    public static List<CardGenerals> CardGenerals { get; set; } = new List<CardGenerals>();
    public static List<CardHeroes> CardHeroes { get; set; } = new List<CardHeroes>();
    public static List<CardMonsters> CardMonsters { get; set; } = new List<CardMonsters>();
    public static List<CardMilitaries> CardMilitaries { get; set; } = new List<CardMilitaries>();
    public static List<CardSoldiers> CardSoldiers { get; set; } = new List<CardSoldiers>();
    public static List<CardSpells> CardSpells { get; set; } = new List<CardSpells>();
    public User(){
        Currencies = new List<Currencies>();
        Power = 0;
    }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }
}
