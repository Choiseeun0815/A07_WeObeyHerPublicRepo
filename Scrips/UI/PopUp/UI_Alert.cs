using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class UI_Alert : UI_Popup
{
    enum Buttons
    {
        
    }

    enum Texts
    {
        InfoText
    }

    enum GameObjects
    {
        
    }

    enum Images
    {
        Blocker,
        Box,
    }

    private SpecialCharacter _specialCharacter;
    
    public float timeLimit = 1.5f; // 타이머 제한 시간
    private float elapsedTime = 0f; // 경과 시간
    
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

        //GetText((int)Texts.InfoText).text = $"맵 어딘가에 '{GameManager.Instance.specialCharacters[GameManager.Instance.specialCharacters.Count - 1].name}'가 나타났다!";
    }

    public void Update()
    {
        elapsedTime += Time.deltaTime; // 경과 시간 누적
        if (elapsedTime >= timeLimit)
        {
            ClosePopupUI();
            elapsedTime = 0f; // 타이머 재설정
        }
    }
}
