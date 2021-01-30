using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverButtons : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void PlayAgain()
    {
        LevelLoader.instance.GotoScene(2);
    }

    // Update is called once per frame
    public void MainMenu()
    {
        LevelLoader.instance.GotoScene(0);
    }
}
