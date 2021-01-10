using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{

    public Transform playerPos;
    public GameObject moveCenter;
    public Transform delayedPlayerPos;
    private Vector3 offsetP,offsetR;

    public float smoothTime;
    private Vector3 vel= Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        delayedPlayerPos.rotation = playerPos.rotation;
        delayedPlayerPos.position = playerPos.position;

        print(playerPos.rotation);
        offsetP = playerPos.position -transform.position;
        offsetR = playerPos.rotation.eulerAngles - transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        //left and right camera following
        float angle = Vector3.Angle(playerPos.forward, delayedPlayerPos.forward);
        Vector3 cross = Vector3.Cross(playerPos.forward, delayedPlayerPos.forward);
        
        int dir =0;
        if(cross.y >0 ){
            print("right");
            dir=-1;
        }else{
            print("left");
            dir=1;
        }
        
        delayedPlayerPos.RotateAround(moveCenter.transform.position, Vector3.up, dir*smoothTime* (Mathf.Lerp(0,angle,.99f)) * Time.deltaTime);

        //up and down camera following
        
        delayedPlayerPos.position = new Vector3(0,Mathf.Lerp(playerPos.position.y,delayedPlayerPos.position.y,.3f),0) ; 

        // // get where the camera's position should be to follow player position
        // transform.position = Quaternion.Euler(delayedPlayerPos.rotation.eulerAngles) * ((delayedPlayerPos.position - offsetP) - delayedPlayerPos.position) + delayedPlayerPos.position;

        // // follow player rotation
        // transform.rotation = Quaternion.Euler(delayedPlayerPos.rotation.eulerAngles - offsetR);
    }
}
