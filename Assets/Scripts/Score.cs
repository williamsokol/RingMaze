using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        scoreText  = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    public void ChangeScore(int scoreChange)
    {
        score  +=  scoreChange;
        scoreText.text = "Score: " + score;
    }
}
