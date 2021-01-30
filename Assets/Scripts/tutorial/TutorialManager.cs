using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private int _tutorialPart;
    public int tutorialPart{
        get{return _tutorialPart;}
        set{
            _tutorialPart = value;
            DoThings();
        }
    }
    public void DoThings()
    {
        switch(tutorialPart)
        {
            case 1:
                
                textActions.TextP1(1);
                break;
            case 2:
                StartCoroutine(bossActions.liftPyramid());
                textActions.TextP1(2);
                break;
            case 3:
                bossActions.StartShooting();
                textActions.TextP1(3);
                break;
            case 4:
                
                //bossActions.StartShooting();
                textActions.TextP1(4);
            break;
            case 5:
                
                //bossActions.StartShooting();
                textActions.TextP1(5);
                LevelLoader.instance.Invoke("NextScene",2f);
            break;
        }
        
    }
    bool movedLeft,movedRight;
    public BossActions bossActions;
    public TextActions textActions;
    void Start()
    {
        
    }
   

    // Update is called once per frame
    void Update()
    {
        if(tutorialPart == 0 && Input.GetAxisRaw("Horizontal") !=0 ){
            if(Input.GetAxisRaw("Horizontal") <0){
                movedLeft = true;
            }else{movedRight = true;}
            if(movedLeft && movedRight){
                tutorialPart =1;
            }
        }
        if(tutorialPart == 1 && Input.GetButton("Jump")){
            tutorialPart =2;
        }
        if(tutorialPart == 2 &&Input.GetButtonDown("Fire1")){
            tutorialPart =3;
        }
        if(tutorialPart == 3 && Boss.bossVulnerable)
        {
            tutorialPart =4;
        }
        if(tutorialPart == 4 && Boss.bossDead){
            tutorialPart =5;
        }
    }
}
