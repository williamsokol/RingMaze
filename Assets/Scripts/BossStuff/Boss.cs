using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [Header("set up")]
    public GameObject player;
    public LineRenderer lazer;
    public GameObject lazerEffect;
    public PyramidMats pyramidMats;
    public GameObject AimObject;
    public GameObject hpSlider;

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
    [HideInInspector]
    public int shootStage =0;
    
    [Header("info")]
    // this is the hp system for boss
    public static bool bossDead = false;
    public static bool bossVulnerable = true;
    [SerializeField]
    private float _HP;
    
    public float HP{
        get{return _HP;}

        set{
            _HP = value;
            HpUpdate();
        }
    }
    void HpUpdate(){
        hpSlider.GetComponent<Slider>().value = HP;
        Score.instance?.ChangeScore(5);

        if(HP <= 0){
            print("you won");
            bossDead = true;
            lazer.enabled = false;
            lazerEffect.SetActive(false);
           
            Destroy(gameObject); 

            DifficultyManager.instance?.Win();
        }
    }
    
    void Start()
    {
        // stuff for the scaling up and down
        scaleAmount = RingGenerator.mapHeight;
        scaleAmount = scaleAmount/(scaleAmount-maxHeight); //maxHeight  is how much lower the boss should be if ur at the top map height;
       
        // stuff for the lazer positioning
        stayPoint = gameObject.transform.position.y;
        pyramidTop = transform.GetChild(0).GetChild(0).gameObject;
        lazer = GetComponent<LineRenderer>();

        lazerPos =.5f;

        // stuff for the bosses hp:
        hpSlider = GameObject.Find("BossHp");
        print(hpSlider);
        hpSlider.GetComponent<Slider>().maxValue = HP;
        hpSlider.GetComponent<Slider>().value = HP;
    
    }
    void Update(){
        if(timer > attackRate  && shootStage == 0 && Movement.moved){
            shootStage = 1;
            StartCoroutine(pyramidMats.TurnRed(this));
            timer =0;
//            print("test");
            
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
        
        //      paralax effect on the boss:
        if(scaleAmount >0){
            transform.position = new Vector3 (0,stayPoint + (Player.instance.transform.position.y/scaleAmount),0);
        }
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
        if(hit.transform.gameObject == Player.instance.gameObject){
            Player.instance.HP -= .1f;
        }

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
