using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip[] songs;
    public AudioSource musicSource;
    public AudioMixer mixer;
    public MusicPlayer instance;
    void Start()
    {
        if(instance != null){
            Destroy(this.gameObject);
        }else{
            instance  = this;
        }
        DontDestroyOnLoad(this.gameObject);

        musicSource =  GetComponent<AudioSource>();
        musicSource.Play();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void SetVolume(float sliderValue){
        mixer.SetFloat("vol",Mathf.Log10 (sliderValue) *20);
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        musicSource.Stop();
        switch (scene.name)
        {
            case "MainMenu":
                musicSource.clip = songs[0];
                break;
            case "tutorial":
                musicSource.clip = songs[1];
                break;
            case "gameplay":
                musicSource.clip = songs[2];
                break;
            case "GameOver":
                musicSource.clip = songs[1];
                break;
        }
        musicSource.Play();
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
