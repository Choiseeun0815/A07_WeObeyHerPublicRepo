using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Test : UI_Popup
{
    enum Buttons
    {
        HelloBtn
    }

    enum Texts
    {
        HelloText
    }

    enum GameObjects
    {

    }

    enum Images
    {
        HEllo
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



    }
}
