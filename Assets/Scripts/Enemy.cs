using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _HP;
    public GameObject explosion;
    
    public float HP{
        get{return _HP;}

        set{
            _HP = value;
            HpUpdate();
        }
    }
    void HpUpdate()
    {
        if(HP <= 0){
            
            Expload();
        }
    }

    public CoinCounter coinCounter;
    // Start is called before the first frame update
    void Start()
    {
        coinCounter = transform.parent.gameObject.GetComponent<CoinCounter>();

        // look at center of map
        Vector3 vectorToTarget = transform.position - Vector3.zero; 
        float angle = Mathf.Atan2(-vectorToTarget.z, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.up);
        transform.rotation = q;

    }
    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player"){
            Expload();
        }
    }

    // Update is called once per frame
    void Expload()
    {
        coinCounter.CheckCoins();
        Score.instance?.ChangeScore(11);
        GameObject exploadEffect  = Instantiate(explosion,transform.position, Quaternion.identity);
        Destroy(exploadEffect,.5f);
        Destroy(gameObject);
    }
}
