
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Enforce : UI_Popup
{
    enum Buttons
    {
        CloseBtn,
        CtrlUpBtn,
        FasterBtn,
        LifeUpBtn
    }

    enum Texts
    {
        Tittle,
        CtrlUpText,
        FasterText,
        LifeUpText,
        CtrlStat2,
        FasterStat2,
        LifeStat2,
        LifePrice2,
        FasterPrice2,
        CtrlPrice2,
    }

    enum GameObjects
    {
        EnforceBtns,
        FasterPrice,
        LifePrice,
        CtrlPrice
    }

    enum Images
    {
        Blocker,
        Panul,
        CloseIcon,
        CtrlUpIcon,
        FasterIcon,
        LifeUpIcon
    }

    private Reinforce _reinforce;


    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        _reinforce = StatManager.Instance._reinforce;
        
        Bind<Button>(typeof(Buttons));
        Bind<TMP_Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));
        
        //지배력 강화
        GetText((int)Texts.CtrlStat2).text = $"Lv. {_reinforce._friendlyLevel}";
        GetText((int)Texts.CtrlPrice2).text = $"Lv. {_reinforce._friendlyPrice}";
        
        //돈 강화
        GetText((int)Texts.FasterStat2).text = $"Lv. {_reinforce._moneyLevel}";
        GetText((int)Texts.FasterPrice2).text = $"Lv. {_reinforce._moneyPrice}";
        
        //생명력 강화
        GetText((int)Texts.LifeStat2).text = $"충전: {_reinforce._lifeCharge}";
        GetText((int)Texts.LifePrice2).text = $"Lv. {_reinforce._lifePrice}";

        //강화버튼
        GetButton((int)Buttons.CtrlUpBtn).gameObject.AddUIEvent(UpdateFriendlyUI);
        GetButton((int)Buttons.FasterBtn).gameObject.AddUIEvent(UpdateMoneyUI);
        GetButton((int)Buttons.LifeUpBtn).gameObject.AddUIEvent(UpdateLifeUI);
        
        //닫기
        GetButton((int)Buttons.CloseBtn).gameObject.AddUIEvent(OnButtonClicked);
        
        
    }

    private void OnButtonClicked(PointerEventData data)
    {
        UIManager.Instance.ClosePopupUI(this);
    }

    public void UpdateFriendlyUI(PointerEventData data)
    {
        _reinforce.FriendlyReinforce();
        
        GetText((int)Texts.CtrlStat2).text = $"Lv. {_reinforce._friendlyLevel}";
        GetText((int)Texts.CtrlPrice2).text = $"Lv. {_reinforce._friendlyPrice}";
    }

    public void UpdateMoneyUI(PointerEventData data)
    {
       _reinforce.MoneyReinforce();
       
       GetText((int)Texts.FasterStat2).text = $"Lv. {_reinforce._moneyLevel}";
       GetText((int)Texts.FasterPrice2).text = $"Lv. {_reinforce._moneyPrice}";
    }
    
    public void UpdateLifeUI(PointerEventData data)
    {
        _reinforce.LifeCharge();
        
        GetText((int)Texts.LifeStat2).text = $"충전: {_reinforce._lifeCharge}";
        GetText((int)Texts.LifePrice2).text = $"Lv. {_reinforce._lifePrice}";
    }

            
    // TMP_Text fasterStatText = Get<TMP_Text>((int)Texts.FasterStat2);
    // TMP_Text fasterPriceText = Get<TMP_Text>((int)Texts.FasterPrice2);
    //
    // if (fasterStatText != null)
    //     fasterStatText.text = "Lv: " + friendlyLevel.ToString();
    //
    // if (fasterPriceText != null)
    //     fasterPriceText.text = "가격: " + friendlyPrice.ToString();
}
