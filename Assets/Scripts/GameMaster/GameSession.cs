using UnityEngine;
using System.Collections;

public class GameSession
{
    static string HAS_SAVE_DATA = "HAS_SAVE_DATA";
    static string PRIMARY_WEAPON = "PRIMARY_WEAPON";
    static string SECONDARY_WEAPON = "SECONDARY_WEAPON";

    static GameSession session;

    private PlayerProgress player;

    public static GameSession GetSession()
    {
        if (session == null)
        {
            session = new GameSession();
            session.InitialLoadData();
        }
        return session;
    }

    public PlayerProgress GetPlayer()
    {
        return player;
    }

    public void InitialLoadData()
    {
        player = new PlayerProgress();
        if (PlayerPrefs.HasKey(HAS_SAVE_DATA))
        {
            LoadEquipment();
        }
        else
        {
            CreateDefault();
        }
    }

    public void SaveGameSession()
    {

    }

    public void LoadGameSession()
    {

    }

    public void SaveEquipment()
    {
        PlayerPrefs.SetInt(PRIMARY_WEAPON, player.GetPrimaryWeapon());
        PlayerPrefs.SetInt(SECONDARY_WEAPON, player.GetSecondaryWeapon());
        PlayerPrefs.Save();
    }

    public void LoadEquipment()
    {
        player.SetPrimary(PlayerPrefs.GetInt(PRIMARY_WEAPON, 0));
        player.SetSecondary(PlayerPrefs.GetInt(SECONDARY_WEAPON, 0));
    }

    private void CreateDefault()
    {
        player.SetPrimary(0);
        player.SetSecondary(0);

        PlayerPrefs.SetInt(HAS_SAVE_DATA, 1);
        SaveEquipment();
    }

}
