using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    protected UIManager uiManager;

    public virtual void Init(UIManager uiManager) //virtual인 경우는 다른 클래스에도 같은 이름이 존재하기 때문이다
    {
        this.uiManager = uiManager;  //새로운 uiManager은 기존의 uimanager에 저장한다.
    }

    protected abstract UIState GetUIState(); //다른 클래스에 있는 ui클래스를 반환한다
    public void SetActive(UIState state) 
    {
        gameObject.SetActive(GetUIState() == state); //반환한UIState가 state와 같다면 표시해준다
    }
}




