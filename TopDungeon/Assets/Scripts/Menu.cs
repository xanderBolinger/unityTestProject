using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
   
    public Text levelText, gpText, upgradeCostText; 
    
    // Logic 
    private int currentCharacterSelection = 0; 
    public Image currentCharacterSelectionSprite; 
    public Image weaponSprite; 
    public RectTransform xpBar; 

    // Character Selection 
    public void OnArrowClick(bool right)
    {
        if(right)
        {
            currentCharacterSelection++;

            if(currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0; 
            
            OnSelectionChange();

        } else 
        {
            currentCharacterSelection--;

            if(currentCharacterSelection < 0) {
                currentCharacterSelection = GameManager.instance.playerSprites.Count; 
                
            }
            
            OnSelectionChange();
        }


    }

    private void OnSelectionChange()
    {
        currentCharacterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
    }

    public void onUpgradeClick()
    {
        
    }

    // Update Character Information 
    public void UpdateMenu() 
    {
        // Weapon 
        int wepLevel = GameManager.instance.weapon.weaponLevel; 

        if(wepLevel <= 4)
        {
            weaponSprite.sprite = GameManager.instance.weaponSprites[wepLevel+1];
            upgradeCostText.text = GameManager.instance.weaponPrices[wepLevel+1].ToString();
        }
        

        // Meta 
        levelText.text = GameManager.instance.level.ToString();
        gpText.text = GameManager.instance.gp.ToString();
       

    } 

}
