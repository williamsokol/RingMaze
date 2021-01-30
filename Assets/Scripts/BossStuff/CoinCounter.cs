using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    public GameObject coinUi;
    public GameObject coinUIPrefab;
    public int coins;
    public static int coinTotal = 3;
    // Start is called before the first frame update
    void Awake(){
        coinTotal = coins;
    }
    void Start()
    {
        coinTotal = transform.childCount; // there is a little bug w ring generator this fixes
        if(transform.childCount >0){
            Boss.bossVulnerable = false;
        }
        
        for(int i=0;i<coinTotal;i++){
            Vector3 pos = new Vector3(coinUi.GetComponent<RectTransform>().position.x+(-40*(i+1)),coinUi.GetComponent<RectTransform>().position.y-30,0f);
            Instantiate(coinUIPrefab,pos,Quaternion.identity,coinUi.transform);

        }
    }

    // Update is called once per frame
    public void CheckCoins()
    {
        if(coinUi != null){
            Destroy(coinUi.transform.GetChild(coinUi.transform.childCount-1).gameObject);
            if (transform.childCount == 1){
                print("boss vulnerable");
                Boss.bossVulnerable = true;
            }
        }else{
            Debug.LogError("no referance to the pyramid UI, retard");
        }
    }
}
