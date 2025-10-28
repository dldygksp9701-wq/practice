using UnityEngine;
using UnityEngine.UI;

public class HomeUI : BaseUI
{
    Button startButton;
    Button exitButton;
    Button howtoplay;
    public GameObject closeButton;
    public GameObject howToPlayImg;
    
    protected override UIState GetUIState()
    {
        return UIState.Home;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        startButton = transform.Find("startButton").GetComponent<Button>();
        exitButton = transform.Find("exitButton").GetComponent<Button>();
        howtoplay = transform.Find("HowToPlay").GetComponent<Button>();
        
        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
        howtoplay.onClick.AddListener(OnClicHowToButton);
        //CloseButton();
    }
    private void Update()
    {
        CloseButton();
    }


    void OnClickStartButton()
    {
        uiManager.OnClickStart();
    }

    void OnClickExitButton()
    {
        uiManager.OnClickExit(); //UiManager가 UiManager에 속해 있기 때문에
    }

    void OnClicHowToButton()
    {
        uiManager.OnclickHowToPlay();
    }
    public void CloseButton()
    {

        if(Input.GetMouseButtonDown(0))
        {
            howToPlayImg.SetActive(false);
        }
        
    }
}