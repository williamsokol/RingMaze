using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score;
    public static Score instance;
    // Start is called before the first frame update
    void Start()
    {
        
        if(instance != null){
            Destroy(this.gameObject);
        }else{
            instance  = this;
        }
        DontDestroyOnLoad(this.gameObject.transform.root.gameObject);

        
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    // Update is called once per frame
    public void ChangeScore(int scoreChange)
    {
        score  +=  scoreChange;
        if(scoreText != null){
            scoreText.text = "Score: " + score;
        }
        
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        scoreText  = GameObject.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();
        if(scoreText != null){
            scoreText.text = "Score: " + score;
        }
    }
}
