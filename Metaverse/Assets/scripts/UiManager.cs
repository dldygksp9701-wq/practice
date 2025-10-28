using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public enum UIState
{
    Home,
    Game,
    Score,
   howToPlay
}

public class UIManager : MonoBehaviour
{
    public GameObject howToPlayImg;
    static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    UIState currentState = UIState.Home;

    HomeUI homeUI = null;

    GameUI gameUI = null;

    ScoreUI scoreUI = null;

    TheStack theStack = null;
    
    
    private void Awake()
    {
        instance = this;
        theStack = FindObjectOfType<TheStack>();

        homeUI = GetComponentInChildren<HomeUI>(true); //homUi는 현제의 스크립트에 속한 하위 컴포넌트중에 HomeUi가 있으면 true
        homeUI?.Init(this); //homUi가 참이면 ? 이것에 메서드를 실행하라
        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI?.Init(this);
        scoreUI = GetComponentInChildren<ScoreUI>(true);
        scoreUI?.Init(this);
        

        ChangeState(UIState.Home); //home으로 가라 처음 실행 할 때는 home이다.
    }

    //ui를 불러오기 위해서는 필드 = 필드명에 저장할 주소;
    //필요할 때 부르는 방법 //필드명 ? 메서드 실행(this);

    public void ChangeState(UIState state) 
    {
        currentState = state;
        homeUI?.SetActive(currentState);
        gameUI?.SetActive(currentState);
        scoreUI?.SetActive(currentState);
    }

    public void OnClickStart()
    {
        theStack.Restart();
        ChangeState(UIState.Game); //매개변수가 Game이고 그 값이 메서드로 가서 state가 Game이 되고 그 값이
        //
    }

    public void OnClickExit()
    {
        SceneManager.LoadScene("MainScene");

    }

    public void OnclickHowToPlay()
    {
        homeUI.howToPlayImg.SetActive(true);
        
    }

    public void UpdateScore()
    {
        gameUI.SetUI(theStack.Score, theStack.Combo, theStack.MaxCombo);
    }
    public void SetScoreUI()
    {
        scoreUI.SetUI(theStack.Score, theStack.MaxCombo, theStack.BestScore, theStack.BestCombo);

        ChangeState(UIState.Score);
    }

}
