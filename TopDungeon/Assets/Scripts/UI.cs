using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UI : MonoBehaviour
{
    
    public static UI instance; 
    public Player player; 
    public Sprite fullHeartSprite;
    public Sprite halfHeartSprite;
    public Sprite emptyHeartSprite;
    public Text gpText;

    private void Awake() 
    {
        
        instance = this; 
    }

    public void UpdateGold() 
    {
        gpText.text = GameManager.instance.gp.ToString(); 
    }

    public void UpdateHearts() 
    {
        int hitpoint = player.hitpoint; 
       

        double hearts = (double) hitpoint / 2; 

        //Debug.Log("Hearts: "+hearts);

        if(Math.Floor(hearts) == hearts) {
            for(int i = 0; i < 5; i++) 
            {   
                if(i < hearts) 
                    GameObject.Find("Heart_"+i).GetComponent<Image>().sprite = fullHeartSprite;
                else 
                    GameObject.Find("Heart_"+i).GetComponent<Image>().sprite = emptyHeartSprite;
                
            }
        } 
        else 
        {

            
            for(int i = 0; i < 5; i++) 
            {   
                if(i < hearts)
                    GameObject.Find("Heart_"+i).GetComponent<Image>().sprite = fullHeartSprite;
                else 
                    GameObject.Find("Heart_"+i).GetComponent<Image>().sprite = emptyHeartSprite;
            }

            hearts -= 0.5; 
            //Debug.Log(hearts);
            GameObject.Find("Heart_"+hearts).GetComponent<Image>().sprite = halfHeartSprite;
        }





    }


}
