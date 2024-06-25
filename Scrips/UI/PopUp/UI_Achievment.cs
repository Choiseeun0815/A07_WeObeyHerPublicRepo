using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Achievment : UI_Popup
{
    enum Buttons
    {
        CloseBtn,
    }

    enum Texts
    {
        Tittle,
    }

    enum GameObjects
    {
        List
    }

    enum Images
    {
        Icon1,
        Icon2,
        Icon3,
        Icon4,
        Icon5,
        Icon6,
        Icon7,
        Icon8,
        Icon9,
    }

    private void Start()
    {
        Init();
    }
    private void Update()
    {
        SetIconsColor();
    }
    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<TMP_Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        GetButton((int)Buttons.CloseBtn).gameObject.AddUIEvent((PointerEventData data) => OnButtonClicked(Buttons.CloseBtn, data));
        
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

    private void SetIconsColor()
    {
        int numOfIcons = Enum.GetNames(typeof(Images)).Length;

        for (int i = 0; i < numOfIcons; i++)
        {
            Image icon = GetImage(i);

            if (Achievement.Instance.isAchievementComplete[i])
            {
                icon.material = null;
            }
            else
            {
                icon.material = GameManager.Instance.grayscaleMaterial;
            }
        }
    }
}
 