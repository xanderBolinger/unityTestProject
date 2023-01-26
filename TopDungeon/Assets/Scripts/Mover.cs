using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{

    private BoxCollider2D boxCollider; 
    private Vector3 moveDelta;
    private RaycastHit2D hit; 
    protected float ySpeed = 0.75f; 
    protected float xSpeed = 1.0f; 

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }


    protected virtual void UpdateMotor(Vector3 input) 
    {
        // Reset move delta 
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0); 



        // Swap sprite direction 
        if(moveDelta.x > 0)
        {
            transform.localScale = new Vector3(1,1,1);
        } 
        else if(moveDelta.x < 0) 
        {
            transform.localScale = new Vector3(-1,1,1);
        }

        // Add push vector if any 
        moveDelta += pushDirection;

        // Push recover
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);


        // Check collider X
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0,moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor","Blocking"));

        // Move
        if(hit.collider == null) {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);            
        } 

         // Check collider Y
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor","Blocking"));

        // Move
        if(hit.collider == null) {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);            
        } 


    }

}
