using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //Game Resources Stored 
    public List<Sprite> playerSprites;
    public List<Sprite> weapons;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public Animator deathMenu;
    public RectTransform hitPointBar;
    public GameObject hud;
    public GameObject menu;

    //Logic
    public int euros;
    public int experience;
    public int currentPlayerSprite = 0;

    public void ShowText(string message, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(message, fontSize, color, position, motion, duration);
    }

    private void Awake()
    {
        Debug.Log("Awake!");
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //Upgrade Weapon
    public bool TryUpgradeWeapon()
    {
        if (weaponPrices.Count <= weapon.weaponLevel)
            return false;

        if (euros >= weaponPrices[weapon.weaponLevel])
        {
            euros -= weaponPrices[weapon.weaponLevel];
            return true;
        }

        if (weapon.weaponLevel == weapons.Count-1 && weapon.weaponLevel != 0)
        {
            return false;
        }

        return false;
    }

    //Change the HP bar
    public void OnHPChanged()
    {
        float ratio = (float)player.hitPoint / (float)player.maxHitPoint;
        hitPointBar.localScale = new Vector3(ratio, 1, 1);
    }
    //Experience System
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while (experience >= add)
        {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count)
                return r;
        }

        return r;
    }
    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }

        return xp;
    }
    public void GrantXP(int xp)
    {
        int currLevel = GetCurrentLevel();
        experience += xp;

        if (currLevel < GetCurrentLevel())
            LevelUp();
    }
    public void LevelUp()
    {
        player.OnLevelUp();
        OnHPChanged();
    }

    //On Scene Loaded
    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("Player Spawn").transform.position;
    }

    //Death Menu and Respawn
    public void Respawn()
    {
        deathMenu.SetTrigger("Hide");
        SceneManager.LoadScene(0);
        player.Respawn();
    }

    //Toggle State
    /*Save State
     *INT preferredSkin
     *INT euros
     *INT experience
     *INT weaponLevel
     */
    //Called Everytime a value is updated
    public void SaveState()
    {
        string data = "";

        data += currentPlayerSprite + "|";
        data += euros.ToString() + "|";
        data += experience.ToString() + "|";
        data += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("Data", data);
        menu.GetComponent<CharacterMenu>().loadMenu();
    }

    //Called everytime a scene loads
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("Data"))
        {
            PlayerPrefs.SetString("Data", "0|0|0|0");
        }

        string[] data = PlayerPrefs.GetString("Data").Split('|');

        //Change skin
        player.GetComponent<SpriteRenderer>().sprite = playerSprites[int.Parse(data[0])];

        //Change euros
        euros = int.Parse(data[1]);

        //Change experience
        experience = int.Parse(data[2]);
        if (GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());

        //Change weaponLevel
        weapon.weaponLevel = int.Parse(data[3]);
        weapon.GetComponent<SpriteRenderer>().sprite = weapons[weapon.weaponLevel];
        menu.GetComponent<CharacterMenu>().loadMenu();
    }
}
