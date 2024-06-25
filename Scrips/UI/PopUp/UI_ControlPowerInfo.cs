using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ControlPowerInfo : UI_Popup
{
    enum Buttons
    {
        CloseBtn,
    }

    enum Texts
    {

        CurrnetPowerText,

    }

    enum GameObjects
    {
        Lv1
    }

    enum Images
    {
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<TMP_Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        GetButton((int)Buttons.CloseBtn).gameObject.AddUIEvent((PointerEventData data) => OnButtonClicked(Buttons.CloseBtn, data));

        UpdateCurrentPowerText();
    }

    private void OnButtonClicked(Buttons buttonType, PointerEventData data)
    {
        switch (buttonType)
        {
            case Buttons.CloseBtn:
                UIManager.Instance.ClosePopupUI(this);
                break;
        }
    }

    private void UpdateCurrentPowerText()
    {
        // StatManager에서 현재 지배력 상태를 가져와 CurrnetPowerText에 반영
        string currentControlLevel = StatManager.Instance.GetCurrentControlLevel();
        //GetTMP_Text((int)Texts.CurrnetPowerText).text = currentControlLevel;
    }
}
