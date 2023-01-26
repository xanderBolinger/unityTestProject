using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{

    public Sprite emptyChest; 
    public int gpAmount = 5; 

    protected override void OnCollect() 
    {
        
        if(!collected) {
            collected = true; 
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.ShowText("+5 GP", 25, Color.yellow, transform.position, Vector3.up * 35, 1.5f);
            GameManager.instance.gp += 5; 
            UI.instance.UpdateGold();
        }


    }

}
