using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    protected UIManager uiManager;

    public virtual void Init(UIManager uiManager) //virtual�� ���� �ٸ� Ŭ�������� ���� �̸��� �����ϱ� �����̴�
    {
        this.uiManager = uiManager;  //���ο� uiManager�� ������ uimanager�� �����Ѵ�.
    }

    protected abstract UIState GetUIState(); //�ٸ� Ŭ������ �ִ� uiŬ������ ��ȯ�Ѵ�
    public void SetActive(UIState state) 
    {
        gameObject.SetActive(GetUIState() == state); //��ȯ��UIState�� state�� ���ٸ� ǥ�����ش�
    }
}




