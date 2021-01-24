using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidMats : MonoBehaviour
{
    public Material top,bottom;
    public Color safeColor,attackColor,color;
    // Start is called before the first frame update
    void Start()
    {
        
        top = GetComponent<SkinnedMeshRenderer>().materials[1];
        
        top.SetColor("_EmissionColor", color);
    }

    // Update is called once per frame
    bool ColorsAreClose(Color a, Color z, float threshold = .01f)
    {
        float r = (a.r - z.r),
            g = (a.g - z.g),
            b = (a.b - z.b);
           
        return (r*r + g*g + b*b) <= threshold;
    }
    public IEnumerator TurnRed(Boss boss){
        boss.shootStage = 2;
        WaitForSeconds gap = new WaitForSeconds(.05f);
        
        while(ColorsAreClose(color,attackColor) == false){
            color =  Color.Lerp(color, attackColor,.1f);
            top.SetColor("_EmissionColor", color);
            
            yield return gap;
            
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(boss.Stage3());

        print("test2");
    }
    public IEnumerator TurnYellow(Boss boss){
        boss.shootStage = 4;
        WaitForSeconds gap = new WaitForSeconds(.05f);
        print("test3");
        while(ColorsAreClose(color,safeColor) == false){
            color =  Color.Lerp(color, safeColor,.1f);
            top.SetColor("_EmissionColor", color);
            
            yield return gap;
           
        }
         
        boss.shootStage = 0;
        

    }
}
