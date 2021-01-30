using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource musicSource;
    public AudioMixer mixer;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        musicSource =  GetComponent<AudioSource>();
    }

    public void SetVolume(float sliderValue){
        mixer.SetFloat("vol",Mathf.Log10 (sliderValue) *20);
    }

    public IEnumerator FadeMusic ( float fadeTime, float volumeGoal) {
        float startVolume = musicSource.volume;
 
        //float velocity = 0f;
        if(volumeGoal ==0){
            while (musicSource.volume > volumeGoal) {
                musicSource.volume -= (startVolume * Time.deltaTime / fadeTime);
                //musicSource.volume = Mathf.SmoothDamp(musicSource.volume, volumeGoal, ref velocity, fadeTime);

                yield return null;
            } 
        }else{
            while (musicSource.volume < volumeGoal-.0001f) {
                musicSource.volume += (volumeGoal * Time.deltaTime / fadeTime);
                //musicSource.volume = Mathf.SmoothDamp(musicSource.volume, volumeGoal, ref velocity, fadeTime);

                yield return null;
            } 
        }
    }
}
