using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Popup
{
    enum Buttons
    {
        PointButton
        
    }

    enum Texts
    {
        ScoreText
    }

    enum GameObjects
    {
        
    }

    enum Images
    {
        Image

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
        //Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        GetButton((int)Buttons.PointButton).gameObject.AddUIEvent(OnButtonClicked);

        GameObject go = GetImage((int)Images.Image).gameObject;
        AddUIEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);
    }

    int _score = 0;
    private void OnButtonClicked(PointerEventData data)
    {

        // if (_score != null)
        //     //GetTMP_Text((int)Texts.ScoreText).text = $"Score: {_score++}";
        // else
        //     Debug.Log("ScoreText is Null");

    }
}
