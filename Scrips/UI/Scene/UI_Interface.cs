using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Interface : UI_Scene
{
    enum Buttons
    {
        EnforceBtn,
        CollectBtn,
        LevelBtn,
        AchievementBtn,
        PayBtn
    }

    enum Texts
    {
        ControlText,
        CollectText,
        PayText,
        LevelText,
        FollowerText,
        NumberText,
        TimerText,
    }

    enum GameObjects
    {
        Coin,
        Stats,
        ControlPower,
        Interface_Play,
        Follower,
        PushBtn
    }

    enum Images
    {
        Background1,
        Background2,
        Background3,
        CoinIcon,
        EnforceIcon,
        ControlIcon,
        Number,
        AchievementIcon,
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

        GetButton((int)Buttons.EnforceBtn).gameObject.AddUIEvent((PointerEventData data) => OnButtonClicked(Buttons.EnforceBtn, data));
        GetButton((int)Buttons.CollectBtn).gameObject.AddUIEvent((PointerEventData data) => OnButtonClicked(Buttons.CollectBtn, data));
        GetButton((int)Buttons.PayBtn).gameObject.AddUIEvent((PointerEventData data) => OnButtonClicked(Buttons.PayBtn, data));
        GetButton((int)Buttons.LevelBtn).gameObject.AddUIEvent((PointerEventData data) => OnButtonClicked(Buttons.LevelBtn, data));
        GetButton((int)Buttons.AchievementBtn).gameObject.AddUIEvent((PointerEventData data) => OnButtonClicked(Buttons.AchievementBtn, data));
    }

    private void OnButtonClicked(Buttons buttonType, PointerEventData data)
    {
        switch (buttonType)
        {
            case Buttons.EnforceBtn:
                UIManager.Instance.ShowPopupUI<UI_Enforce>();
                break;
            case Buttons.CollectBtn:
                StatManager.Instance.GetMoneyFromFollowers();
                break;
            case Buttons.LevelBtn:
                UIManager.Instance.ShowPopupUI<UI_ControlPowerInfo>();
                break;
            case Buttons.AchievementBtn:
                UIManager.Instance.ShowPopupUI<UI_Achievment>();
                break;
            case Buttons.PayBtn:
                StatManager.Instance.PayExpense();
                break;
        }
    }
}
