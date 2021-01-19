using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public Camera cam;
    public GameObject bulletPrefab;
    public float attackRate;
    public float attackSpeed;
    public float fieldOfRange = 90;
    public float aimDistance = 100f;
    float timer =0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Input.GetButtonDown("Fire1") && timer>attackRate){
            Vector3 aim;
            if(Physics.Raycast(mouseRay,out hit, aimDistance) && hit.transform.gameObject != Player.instance.gameObject)
            {                       
                aim = hit.point;
            }else{
                aim = mouseRay.origin+mouseRay.direction* (9f);
                //print("test "+ aim);
            }
            Shoot(aim);
        }else{
            timer += Time.deltaTime;
        }
        
        
    }
    void fixAngle(){
        Vector3 newAngle = transform.localRotation.eulerAngles;
        if(transform.localRotation .eulerAngles.y > fieldOfRange+90 &&  transform.localRotation .eulerAngles.y < 170){
            newAngle.y =  90;
        }else
        if(transform.localRotation .eulerAngles.y < 270-fieldOfRange &&  transform.localRotation .eulerAngles.y > 190){
            newAngle.y =  -90;
        }else{
            newAngle.y = transform.localRotation .eulerAngles.y;

        }
        
            
//        print(transform.localRotation .eulerAngles.y);
        transform.localRotation =  Quaternion.Euler(newAngle);
        
    }
    
    void Shoot(Vector3 aim)
    {
        transform.LookAt(aim);
        fixAngle(); 

        GameObject bullet = Instantiate(bulletPrefab,transform.position+(transform.forward*1),transform.rotation);
        Destroy(bullet,2f);
        timer = 0;
        bullet.GetComponent<bullet>().direction = aim;

    }
}
