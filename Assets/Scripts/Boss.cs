using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("set up")]
    public GameObject player;
    public LineRenderer lazer;
    public GameObject lazerEffect;
    public PyramidMats pyramidMats;
    public GameObject AimObject;

    [Header("atributes")]
    public float maxHeight;
    public float attackRate;
    public float attackDuration = 3f;
    public float lazerSpeed = .03f;
    
    GameObject pyramidTop;
    private float stayPoint;
    private float lazerPos;
    private float scaleAmount;
    private float timer;

    // lazer traits
    Vector3 firePoint;
    Vector3 fireAngle;

    [HideInInspector]
    public bool shootLazer;
    public int shootStage =0;
    
    // Start is called before the first frame update
    void Start()
    {
       stayPoint = gameObject.transform.position.y;
       scaleAmount = RingGenerator.mapHeight;
       scaleAmount = scaleAmount/(scaleAmount-maxHeight); //maxHeight  is how much lower the boss should be if ur at the top map height;
       pyramidTop = transform.GetChild(0).GetChild(0).gameObject;
       lazer = GetComponent<LineRenderer>();

        
       lazerPos = lazer.GetPosition(0).y-pyramidTop.transform.position.y;

       //StartCoroutine(pyramidMats.TurnRed(this));
    }
    void Update(){
        if(timer > attackRate  && shootStage == 0){
            shootStage = 1;
            StartCoroutine(pyramidMats.TurnRed(this));
            timer =0;
            print("test");
            
        }else if(shootStage  != 3){
            timer += Time.deltaTime;
        }
        
    }

   
    void FixedUpdate()
    {
        AimLazer();
        if(shootStage == 3){
            ShootingLazer();
            
        
        }else{
            lazer.enabled = false;
            lazerEffect.SetActive(false);
        }
        
        transform.position = new Vector3 (0,stayPoint + (Player.instance.transform.position.y/scaleAmount),0);
    }

    public IEnumerator Stage3(){
        shootStage = 3;
        WaitForSeconds duration = new WaitForSeconds(attackDuration);
       
        yield return duration;
        StartCoroutine(pyramidMats.TurnYellow(this));
    }
    void ShootingLazer(){
    
        lazer.enabled = true;
        lazerEffect.SetActive(true);
        RaycastHit hit;
        Vector3 down = AimObject.transform.position;
        bool worked = false;
        do {
            if(worked = Physics.Raycast(firePoint,fireAngle, out hit, 100f)){
                lazer.SetPosition(1,hit.point);
                lazerEffect.transform.position = hit.point;
            }else{
              
                down.y --;
                fireAngle = Quaternion.LookRotation(down-firePoint) * Vector3.forward;
            }
        }while(worked == false);

    }
    void AimLazer(){
        AimObject.transform.position -= (AimObject.transform.position-player.transform.position)/(30);
        AimObject.transform.position = Vector3.MoveTowards(AimObject.transform.position, player.transform.position, lazerSpeed);
        firePoint = new Vector3(transform.position.x,pyramidTop.transform.position.y+lazerPos,transform.position.z);
        fireAngle = Quaternion.LookRotation(AimObject.transform.position-firePoint) * Vector3.forward;

        lazer.SetPosition(0,firePoint);
        // Ray lazerRay = new Ray();
        

        lazerEffect.transform.position = AimObject.transform.position;
    }
}
