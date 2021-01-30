using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyManager : MonoBehaviour
{
   public static DifficultyManager instance;

   public int BossHp;
   //public float 

    // Start is called before the first frame update
    void Start()
    {
        
        if(instance != null){
            Destroy(this.gameObject);
        }else{
            instance  = this;
        }
        DontDestroyOnLoad(this.gameObject);
        
        SceneManager.sceneLoaded += OnSceneLoaded;

        Boss boss = GameObject.Find("Pyramid").GetComponent<Boss>();
        BossHp = (int)boss.HP;
    }

    // Update is called once per frame
    public void Win()
    {
        BossHp++;
        CoinCounter.coinTotal++;
        
        LevelLoader.instance.GotoScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Boss boss = GameObject.Find("Pyramid").GetComponent<Boss>();
        boss.HP = (float)BossHp;
    }

}
