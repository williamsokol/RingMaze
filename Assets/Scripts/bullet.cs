using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bullet : MonoBehaviour
{
    public GameObject explosion;
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
       
        if(hit.transform != null ){
            if(hit.transform.gameObject.GetComponent<Enemy>() != null){
                hit.transform.gameObject.GetComponent<Enemy>().HP -= 1;
                GameObject thing  = Instantiate(explosion,hit.point, Quaternion.identity);
                Destroy(thing,.5f);
            }else if(hit.transform.root.gameObject.GetComponent<Boss>() != null){
                if(Boss.bossVulnerable){
                    hit.transform.root.gameObject.GetComponent<Boss>().HP -= 1;
                    GameObject thing  = Instantiate(explosion,hit.point, Quaternion.identity);
                    Destroy(thing,.5f);
                }
            }
        }else
        {
//            print ("non-enemy hit: ");
        }

        

        Destroy(gameObject);
        
    }
    
}
