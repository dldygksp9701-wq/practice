using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    public GameObject score;
    public GameObject close;
    private TheStack theStack;
    private int bestScore;
    private int bestCombo;
    public TMP_Text bestsc;
    public TMP_Text bestcb;
    void Start()
    {
        
        
    }

    
    void Update()
    {
        
    }

    public void UiClick()
    {
        theStack = GetComponent<TheStack>();
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestCombo = PlayerPrefs.GetInt("BestCombo", 0);

        score.SetActive(true);
        bestsc.text = bestScore.ToString();
        bestcb.text = bestScore.ToString();
        
    }
    public void CloseClick()
    {
        score.SetActive(false);
    }
}
