using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _HP;
    private bool dead;
    
    public float HP{
        get{return _HP;}

        set{
            _HP = value;
            HpUpdate();
        }
    }
    void HpUpdate()
    {
        HpSlider.GetComponent<Slider>().value = HP;
        if(HP <= 0 && !dead){
            dead = true;
//            print("you lost!");
            Score.instance.score =0;
            LevelLoader.instance.GotoScene(3);
        }
    }
    
    // Start is called before the first frame update
    public GameObject HpSlider;

    public static Player instance;
    void Start()
    {
        HpSlider = GameObject.Find("Hp");
        HpSlider.GetComponent<Slider>().maxValue = HP;
        HpSlider.GetComponent<Slider>().value = HP;
        if(instance == null){
            instance = this;
        }
    }

}
