using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 

    private void Awake() {

        if(GameManager.instance != null)
        {
            Destroy(gameObject);
            return; 
        }

        instance = this; 
        SceneManager.sceneLoaded += LoadState; 
        DontDestroyOnLoad(gameObject);
    }

    // Resources 
    public List<Sprite> playerSprites; 
    public List<Sprite> weaponSprites; 
    public List<int> weaponPrices; 
    public List<int> xpTable; 

    // Weapon Level 
    public Weapon weapon; 

    // References  
    public Player player; 
    // Public Weapon weapon... 

    public FloatingTextManager floatingTextManager;
    
    // Logic 
    public int gp; 
    public int xp; 
    public int level; 

    // Floating Text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration) 
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    // Save state 
    /*
        INT preferedSkin 
        INT gp 
        INT xp 
        INT weaponLevel
    */

    public void TryUpgradeWeapon() 
    {
        if(weaponPrices.Count <= weapon.weaponLevel)
            return; 
        
        
        if (gp >= weaponPrices[weapon.weaponLevel + 1])
        {
            weapon.UpgradeWeapon();
        }

        
    }

    public void SaveState() {
        string s = ""; 
        
        s += "0" +"|";
        s += gp.ToString()+"|";
        s += xp.ToString()+"|";
        s += weapon.weaponLevel.ToString()+"|";
        s += "0";

        
        
        PlayerPrefs.SetString("SaveState", s);

    } 

    public void LoadState(Scene s, LoadSceneMode mode) {

        if(!PlayerPrefs.HasKey("SaveState"))
            return; 

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        // Change Player Skin 

        // Set GP 
        gp = int.Parse(data[1]);
        xp = int.Parse(data[2]);
        weapon.weaponLevel = int.Parse(data[3]);

        // Change Weapon Level
        /*Debug.Log(weapon);
        Debug.Log(weapon.weaponLevel);
        Debug.Log(weaponSprites[weapon.weaponLevel]);*/
        weapon.setWeaponLevel(weapon.weaponLevel, weaponSprites[weapon.weaponLevel]);
        weapon.damagePoint = weapon.weaponLevel + 1; 
        
        // Sets player hearts 
        UI.instance.UpdateHearts();
        UI.instance.UpdateGold();

    }

}
