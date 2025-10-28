using TMPro;
using UnityEngine;

public class GameUI : BaseUI
{
    TextMeshProUGUI scoreText;
    TextMeshProUGUI comboText;
    TextMeshProUGUI maxComboText;

    protected override UIState GetUIState()   //
    {
        return UIState.Game; //UiState.Game 을 반환한다
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        scoreText = transform.Find("scoreText").GetComponent<TextMeshProUGUI>();
        comboText = transform.Find("comboText").GetComponent<TextMeshProUGUI>();
        maxComboText = transform.Find("MaxComboText").GetComponent<TextMeshProUGUI>();
    }

    public void SetUI(int score, int combo, int maxCombo)
    {
        scoreText.text = score.ToString();
        comboText.text = combo.ToString();
        maxComboText.text = maxCombo.ToString();
    }
}
