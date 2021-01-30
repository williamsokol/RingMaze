using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextActions : MonoBehaviour
{

    public Animator textAnimator;
    public TextMeshProUGUI m_Text;
    private int part;
    // Start is called before the first frame update
    void Start()
    {
        textAnimator = GetComponent<Animator>();
        m_Text = GetComponent<TextMeshProUGUI>();
        //pos = this.GetComponent<RectTransform>().localPosition;
    }

    // Update is called once per frame
    public void TextP1(int stage){
        
        textAnimator.SetInteger("TutorialPart",stage);
        textAnimator.SetTrigger("ChangePart");
        part = stage;
    }
    public void Text1()
    {
        int textY = -1 ;
        switch(part)
        {
            case 1:
                textY = -150;
                m_Text.text = "Press Space to jump";
            break;
            case 2:
                textY = -100;
                m_Text.text = "Click with mouse to shoot!";
            break;
            case 3:
                textY = -150;
                m_Text.text = "Destroy the mini Pyramids";
            break;
            case 4:
                textY = -200;
                m_Text.text = "Now the boss is vulnerable!";
            break;
            case 5:
                textY = -150;
                m_Text.text = "Well Done";
            break;
        }
        if (textY != -1)
        {
            this.GetComponent<RectTransform>().localPosition = new Vector3(0,textY,0);
        }
        

    }
}
