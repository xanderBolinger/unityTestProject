using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Weapon : Collidable
{
   
    // Damage Structure 
    public int damagePoint = 1; 
    public float pushForce = 2.0f; 

    // Upgrade
    public int weaponLevel = 1; 

    private SpriteRenderer spriteRenderer;

    // Swimg 
    private Animator anim; 

    private float cooldown = 0.75f; 
    private float lastSwing; 

    protected override void Start() 
    {
        base.Start(); 

        if(spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        
        anim = GetComponent<Animator>();
    }


    protected override void Update() 
    {
        base.Update(); 


        // Check facing 
        // GameObject.Find("Player").transform.localScale.x < 0 // left facing 
        // GameObject.Find("Player").transform.localScale.x > 0 // right facing 

        bool facingRight = GameObject.Find("player_0").transform.localScale.x > 0;
        //Debug.Log("FacingRight: "+facingRight);
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(Time.time - lastSwing > cooldown)
            {
                if(facingRight)
                {
                    Swing(); 
                } else
                {
                    LeftSwing(); 
                } 
                
            }
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            if(Time.time - lastSwing > cooldown)
            {
                if(facingRight)
                {
                    LeftSwing(); 
                } else
                {
                    Swing(); 
                } 
            }
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            if(Time.time - lastSwing > cooldown)
            {
               DownSwing();
            }
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            if(Time.time - lastSwing > cooldown)
            {
               UpThrust();
            }
        }
        

    }

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag == "Fighter") {

            if(coll.name == "player_0") 
                return; 
            
            Damage dmg = new Damage
            {
                damageAmount = damagePoint,
                origin = transform.position, 
                pushForce = pushForce
            };

            coll.SendMessage("ReceiveDamage", dmg);
            

        }
        
    }

    private void Swing() 
    {
        anim.SetTrigger("Swing");
        lastSwing = Time.time; 
    }

    private void LeftSwing() 
    {
        anim.SetTrigger("LeftSwing");
        lastSwing = Time.time; 
    }

    private void UpThrust() 
    {
        anim.SetTrigger("TopThrust");
        lastSwing = Time.time; 
    }

    private void DownSwing() 
    {
        anim.SetTrigger("BottomSwing");
        lastSwing = Time.time; 
    }

    // Upgrades weapon level 
    public void UpgradeWeapon()
    {
        //Debug.Log(newWeaponSprite);
        weaponLevel++; 
        GameManager.instance.gp -= GameManager.instance.weaponPrices[weaponLevel];
        setWeaponLevel(weaponLevel, GameManager.instance.weaponSprites[weaponLevel]);
        UI.instance.UpdateGold();
        GameObject.Find("Menu").GetComponent<Menu>().UpdateMenu();

        damagePoint = weaponLevel+1; 
        pushForce += 0.2f * weaponLevel; 

    }

    // Sets level 
    // Sets sprite 
    // TODO: check level, set stats equal to level 
    public void setWeaponLevel(int level, Sprite weaponSprite) 
    {
        if(spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        
        weaponLevel = level; 
        //Debug.Log(weaponSprite);
        spriteRenderer.sprite = weaponSprite;
        GameObject.Find("WeaponImage").GetComponent<Image>().sprite = spriteRenderer.sprite;
    }

}
