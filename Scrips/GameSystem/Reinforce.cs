using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Reinforce : MonoBehaviour
{
    public int _getMoneyFromMob { get; set; } //StatManager moneyFrommob 연결완료
    public int _getMoneyFromCollect; //StatManager moneyReinforce
    public int _moneyLevel = 1;
    public int _moneyPrice = 100;

    [Header("Friendly")]
    [SerializeField] private MobClicker _mobClicker; //Player 오브젝트 연결

    public int _friendlyLevel = 1;
    public int _friendlyPrice = 100;

    public int _lifeCharge = 1;
    public int _lifePrice = 100;

    private UI_Enforce _uiEnforce;
    private LifeController _lifeController;

    private void Start()
    {
        _getMoneyFromMob = StatManager.Instance.moneyFromMob;
        _getMoneyFromCollect = StatManager.Instance.moneyReinforce;
    }
    public void UseMoney(int price, int level)
    {
        if (StatManager.Instance.coinAmount >= price)
        {
            StatManager.Instance.coinAmount -= price;
            Debug.Log($"coin: {StatManager.Instance.coinAmount}");
            StatManager.Instance.UpdateCoinText();
        }
        else
        {
            Debug.Log("골드가 부족합니다.");
        }
    }
    public void MoneyReinforce()
    {
        if (StatManager.Instance.coinAmount >= _moneyPrice)
        {
            UseMoney(_moneyPrice, _moneyLevel);
            _getMoneyFromMob *= 2;
            StatManager.Instance.moneyReinforce ++;
            _moneyLevel++;
            Debug.Log($"Money Level: {_moneyLevel}");
            _moneyPrice += 100;
        }
        else Debug.Log("골드가 부족합니다.");
    }

    public void FriendlyReinforce()
    {
        if (StatManager.Instance.coinAmount >= _friendlyPrice)
        {
            UseMoney(_friendlyPrice, _friendlyLevel);
            _mobClicker.clickPower += 0.2f;
            _friendlyLevel++;
            _friendlyPrice += 100;
        }
        else Debug.Log("골드가 부족합니다.");
    }

    public void LifeCharge()
    {
        if (StatManager.Instance.health < 3)
        {
            if (StatManager.Instance.coinAmount >= _lifePrice)
            {
                UseMoney(_lifePrice, _lifeCharge);
                _lifeCharge++;
                _lifePrice *= 2;
                StatManager.Instance.lifeController.ChangeLifeUI(++StatManager.Instance.health);
            }
            else Debug.Log("골드가 부족합니다.");
        }
        else Debug.Log("최대 Life 입니다.");
    }
}
