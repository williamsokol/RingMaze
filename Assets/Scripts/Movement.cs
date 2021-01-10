using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    public int speed;
    public int jumpPow;

    public GameObject moveCenter;
 

    
    [SerializeField]private bool isGrounded;
    private float dir;
    private int wall;
    private Rigidbody rb;
    private BoxCollider col;
    

   GameObject currentGround;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
//        print(col);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") && isGrounded){
            rb.AddForce(transform.up*jumpPow*10);
        }

        //left and right movement
        dir = -Input.GetAxisRaw("Horizontal");
        
    }
    void FixedUpdate(){
        if(wall == 0 || dir != -wall){
            transform.RotateAround(moveCenter.transform.position, Vector3.up, dir * speed * Time.deltaTime);
            wall=0;
        }

    }
    void OnCollisionEnter(Collision collision){
        Vector3 hit = collision.contacts[0].normal;
//             Debug.Log(hit);
             float angle = Vector3.Angle(hit, Vector3.up);
 
             if (Mathf.Approximately(angle, 0))
             {
                 //Down
                 isGrounded = true;
                 //print(gamePoints[0].contacts.Length);
                 currentGround = collision.gameObject;
                 Debug.Log("Down");
             }
             if(Mathf.Approximately(angle, 180))
             {
                 //Up
                 Debug.Log("Up");
             }
             if(Mathf.Approximately(angle, 90)){
                 // Sides
                 Vector3 cross = Vector3.Cross(transform.forward, hit);
                 if (cross.y > 0)
                 { // left side of the player
                     Debug.Log("Left");
                     wall = -1;
                 }
                 else
                 { // right side of the player
                     Debug.Log("Right");
                     wall = 1;
                 }
             }
       
    }

    void OnCollisionExit(Collision collision){
        if (currentGround  == collision.gameObject){
            isGrounded = false;
        }
        
        // float angle = Vector3.Angle(hit, Vector3.up);

        // if (Mathf.Approximately(angle, 0))
        // {
        //     //Down
            
        //     Debug.Log("!Down");
        // }
        //  if(Mathf.Approximately(angle, 180))
        //  {
        //      //Up
        //      Debug.Log("Up");
        //  }
        //  if(Mathf.Approximately(angle, 90)){
        //      // Sides
        //      Vector3 cross = Vector3.Cross(Vector3.forward, hit);
        //      if (cross.y > 0)
        //      { // left side of the player
        //          Debug.Log("Left");
        //          wall = -1;
        //      }
        //      else
        //      { // right side of the player
        //          Debug.Log("Right");
        //          wall = 1;
        //      }
        //  }
       
    }
}

