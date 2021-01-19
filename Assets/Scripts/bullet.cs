using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bullet : MonoBehaviour
{
    
    public float bulletSpeed;
    public Vector3 direction;
    private Vector3 lastPosition;
    RaycastHit hit;
    void Start()
    {
       lastPosition = Vector3.zero;
       Ray dir = new Ray(transform.position, transform.forward);
       if(Physics.Raycast(dir,out hit, 100f)){
           direction = hit.point;
       }
       

    }

    void FixedUpdate()
    {
        transform.Translate( Vector3.forward * bulletSpeed,Space.Self);
        //once bullet reaches target
        if(Vector3.Distance(direction,transform.position) < .5){
            Hit();
        }
       
    }
    void Hit(){
        Destroy(gameObject);
    }
    
}
