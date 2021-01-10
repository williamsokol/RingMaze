using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    // Start is called before the first frame updateprivate void OnTriggerEnter(Collider other)
    public LayerMask ground;
    private float groundLayer;
    public bool grounded = true; 
    void Start(){
        groundLayer =  Mathf.Log(ground.value, 2);
    }
    private void OnTriggerStay(Collider other)
    {
        print("enter " + other.gameObject.layer +" | " + groundLayer);
        if(other.gameObject.layer == groundLayer){
             grounded = true;
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        print("exit "+other.gameObject.layer +" | "+ other);
         if(other.gameObject.layer == groundLayer){
             grounded = false;
        }
    }
   public bool IsGrounded(){
        
        return grounded;
    }
}
