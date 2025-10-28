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

        homeUI = GetComponentInChildren<HomeUI>(true); //homUi�� ������ ��ũ��Ʈ�� ���� ���� ������Ʈ�߿� HomeUi�� ������ true
        homeUI?.Init(this); //homUi�� ���̸� ? �̰Ϳ� �޼��带 �����϶�
        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI?.Init(this);
        scoreUI = GetComponentInChildren<ScoreUI>(true);
        scoreUI?.Init(this);
        

        ChangeState(UIState.Home); //home���� ���� ó�� ���� �� ���� home�̴�.
    }

    //ui�� �ҷ����� ���ؼ��� �ʵ� = �ʵ�� ������ �ּ�;
    //�ʿ��� �� �θ��� ��� //�ʵ�� ? �޼��� ����(this);

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
        ChangeState(UIState.Game); //�Ű������� Game�̰� �� ���� �޼���� ���� state�� Game�� �ǰ� �� ����
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
