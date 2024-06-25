using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameOver : UI_Popup
{
    enum Buttons
    {
        RetryBtn
    }

    enum Texts
    {
        Tittle,
        FText,
        FNumber,
        EText,
        EContents,
        RetryText,
    }

    enum GameObjects
    {
        List
    }

    enum Images
    {
        Panul,
        FollowerImage,
        FImage,
        EndingImage,
        EImage
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

        GetButton((int)Buttons.RetryBtn).gameObject.AddUIEvent(Retry);

        // Ensure StatManager instance is initialized
        if (StatManager.Instance == null)
        {
            Debug.LogError("StatManager instance is null. Make sure StatManager script is attached to an active GameObject in the scene.");
            return;
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        var statManager = StatManager.Instance;

        if (statManager == null)
        {
            Debug.LogError("StatManager instance is null. Cannot update UI.");
            return;
        }

        Get<TMP_Text>((int)Texts.FNumber).text = statManager.followerCount.ToString();
        Get<TMP_Text>((int)Texts.EContents).text = statManager.EndingReason;
    }

    private void Retry(PointerEventData data)
    {
        GameManager.Instance.RetryGame();
    }
}