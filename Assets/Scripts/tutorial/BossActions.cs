using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActions : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        transform.Translate(0,-10,0);
        //tutorialPart = 0;
    }

    // Update is called once per frame
    public void StartShooting(){
        GetComponent<Animation>().enabled = true;
        GetComponent<Boss>().enabled = true;
        //GetComponent<LineRenderer>().enabled = true;
    }
    public IEnumerator liftPyramid(){
        yield return new WaitForSeconds(.5f);
        WaitForSeconds gap = new WaitForSeconds(Time.deltaTime);
        while (transform.position.y < 2 && !Mathf.Approximately(transform.position.y, 2)){
            transform.position = new Vector3(0,Mathf.Lerp(transform.position.y,2,.1f),0);

//            print("test3");
            yield return gap;
        }
    }
}
