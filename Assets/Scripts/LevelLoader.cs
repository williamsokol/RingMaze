using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    public Animator fadeAnimator;
    public GameObject musicPlayer;
    public static LevelLoader instance;
    // Start is called before the first frame update
    void Start()
    {
        
        if(instance != null){
            Destroy(this.gameObject);
        }else{
            instance  = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    public void GotoScene(int sceneNum)
    {
         
        StartCoroutine( SceneChanger(sceneNum));        
    }
    public void NextScene()
    {
        StartCoroutine( SceneChanger(SceneManager.GetActiveScene().buildIndex+1));  
             
    }
    public IEnumerator SceneChanger(int scene){
        fadeAnimator.SetTrigger("FadeOut"); 
        StartCoroutine(musicPlayer.GetComponent<MusicPlayer>().FadeMusic(fadeAnimator.GetCurrentAnimatorStateInfo(0).length,0f));
        
        yield return new WaitForSeconds(fadeAnimator.GetCurrentAnimatorStateInfo(0).length );
        SceneManager.LoadScene(scene);  
        
        fadeAnimator.SetTrigger("FadeIn"); 
        StartCoroutine(musicPlayer.GetComponent<MusicPlayer>().FadeMusic(fadeAnimator.GetCurrentAnimatorStateInfo(0).length,1f));
        
    }
}
