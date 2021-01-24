using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    public int speed;
    public int jumpPow;
    public float fallMultiplier;

    public GameObject moveCenter;
 

    
    [SerializeField]private bool isGrounded;
    private float dir;

    
    private int wall;
    private Rigidbody rb;
    private BoxCollider col;
    
    private GameObject currentWall;
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
        // detct for jumping
        if(Input.GetButton("Jump") && isGrounded){
            rb.AddForce(transform.up*jumpPow*10);
            isGrounded = false;
        }

        //left and right movement
        dir = -Input.GetAxisRaw("Horizontal");

        //make it so you fall faster down than up
        if(rb.velocity.y < 0){
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier-1) * Time.deltaTime;
        }
    }
    void FixedUpdate(){
        if(wall == 0 || dir == -wall){
            transform.RotateAround(moveCenter.transform.position, Vector3.up, dir * speed * Time.deltaTime);
            wall=0;
            
        }
        else {
            if(dir == wall&& !isGrounded){
                transform.Translate(Vector3.up * .04f, Space.World);
            }
        }
//        print(wall);

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
                 //Debug.Log("Down");
             }
             if(Mathf.Approximately(angle, 180))
             {
                 //Up
                // Debug.Log("Up");
             }
             if(Mathf.Approximately(angle, 90)){
                 // Sides
                 currentWall = collision.gameObject;
                 Vector3 cross = Vector3.Cross(transform.forward, hit);
                 if (cross.y > 0)
                 { // left side of the player
                     //Debug.Log("Left");
                     wall = 1;
                 }
                 else
                 { // right side of the player
                     //Debug.Log("Right");
                     wall = -1;
                 }
             }
       
    }

    void OnCollisionExit(Collision collision){
        if (currentGround  == collision.gameObject){
            isGrounded = false;
        }
        if(currentWall == collision.gameObject){
            wall=0;
        }
        // realign the player if he twisted a little
       
        Vector3 vectorToTarget = moveCenter.transform.position - transform.position;
        float angle = Mathf.Atan2(-vectorToTarget.z, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle+90, Vector3.up);
        transform.rotation = q;
                                   
    }
}

