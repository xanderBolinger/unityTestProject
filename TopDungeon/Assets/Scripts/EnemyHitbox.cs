using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHitbox : Collidable
{
    public int damage; 
    public float pushForce; 


    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag == "Fighter" && coll.name == "player_0") 
        {
            // Create new damage object 
            Damage dmg = new Damage
            {
                damageAmount = damage,
                origin = transform.position, 
                pushForce = pushForce
            };

            coll.SendMessage("ReceiveDamage", dmg);
            UI.instance.UpdateHearts();
        }
    }
}
