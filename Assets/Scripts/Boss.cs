using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private float stayPoint;
    public GameObject player;
    float scaleAmount;
    // Start is called before the first frame update
    void Start()
    {
       stayPoint = gameObject.transform.position.y;
       scaleAmount = RingGenerator.mapHeight;
       scaleAmount = scaleAmount/(scaleAmount-8f); //10 is how much lower the boss should be if ur at map height;
//       print(scaleAmount);
    }

   
    void FixedUpdate()
    {
        transform.position = new Vector3 (0,stayPoint + (player.transform.position.y/scaleAmount),0);
    }
}
