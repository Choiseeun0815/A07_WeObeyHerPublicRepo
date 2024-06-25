using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : Singleton<StatManager>
{
    [Header("TextPro")]
    public TextMeshProUGUI followerNumTxt;
    public TextMeshProUGUI controlLevelTxt;
    public TextMeshProUGUI coinTxt;
    public TextMeshProUGUI collectTimerTxt;
    public TextMeshProUGUI payTimerTxt;
    public TextMeshProUGUI limitedTxt;

    [Header("Money")]
    public int moneyFromMob;
    public int coinAmount = 100; // 코인 초기값
    private bool isMobFollower;
    private int collectableMoney; // 수금가능한 코인양
    private bool isCollectable = false; // 수금 가능 여부
    private int mustPayMoney; // 지불해야하는 코인양
    private int fixedPayAmount; // 고정된 지불 코인양
    private bool isPaytime = false;
    public int moneyReinforce = 2; //강화수치

    [Header("CollectTimer")]
    public Image collectImage;
    private float collectDuration = 5f; // 수금 간격(수금하기 버튼)
    private float currentCollectTimer; // 현재 타이머 시간
    private bool isCollectTimeRunning = false; // 타이머 실행 여부

    [Header("PayTimer")]
    public Image payImage;
    public Image limitedTimeImage;
    private float payDuration = 7f; // 지불 간격(지불하기 버튼)
    private float currentPayTimer; // 현재 타이머 시간
    private bool isPayTimeRunning = false; // 타이머 실행 여부
    private bool isPayComplete = false;
    private int limitedTimeDuration = 10;
    private int currentLimitedTimer; // 현재 경고 타이머 시간

    [Header("Life")]
    public int health = 3;
    public bool loseHeartByEnemy = false;

    [Header("SkillTime")]
    public Image collectTimeImage;
    public Image payTimeImage;

    [HideInInspector]
    public bool isFollowerAdded;
    [HideInInspector]
    public bool isFollowerout;
    [HideInInspector]
    public int followerCount;
    private string controlLevel;

    public Reinforce _reinforce;
    private AudioSource audioSource;
    private Coroutine limitedCoroutine; // 제한 시간 코루틴을 추적할 변수
    public LifeController lifeController; // LifeController 참조 변수


    // 체력 감소가 몇 번 이루어졌는지 저장하기 위한 변수 -> enemy 피격에도 추가해서 합산해야함
    public int calledDecreaseHealth = 0;
    public bool isGameOver = false;

    public int consecutivePay = 0; // 업적 달성 조건 파악을 위한 돈 연속 지불 여부


    protected override void Awake()
    {
        base.Awake();

        UpdateFollowerText();
        moneyFromMob = 10;
    }

    private void Start()
    {
        if (limitedTimeImage.gameObject.activeSelf)
        {
            limitedTimeImage.gameObject.SetActive(false);
        }

        lifeController = GetComponent<LifeController>();
        _reinforce = gameObject.GetComponent<Reinforce>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(isFollowerAdded)
        {
            UpdateFollowerText(); // 현재 총 팔로워
            isFollowerAdded = false; // 초기화
        }
        // 신도 수 1명 이상 이면, 타이머 실행
        if(followerCount > 0)
        {
            TimerBeforeGetMoney();
        }
    }
    
    public string EndingReason { get; set; } 
    
    // FollowerList 정보를 받아오는 메서드
    private List<GameObject> GetFollowerList()
    {
        return PromptManager.Instance.FollowerList;
    }

    // 현재 Follower 숫자 계산 메서드
    public void UpdateFollowerText()
    {
        followerCount = GetFollowerList().Count;
        followerNumTxt.text = $"{followerCount}명";
        UpdateControlLevelText();
        UpdateCoinText();
    }

    // 현재 지배력 상태 메서드
    private void UpdateControlLevelText()
    {   
        // 딕셔너리<최소 신도 수, 최소 신도 수 이상일 때 지배력 상태>
        Dictionary<int, string> controlLevels = new Dictionary<int, string>
        {
            { 0, "일반인" }, // 신도 수가 0 이상 10보다 적으면 "일반인"
            { 10, "광신도" }, // 신도 수가 10 이상 50보다 적으면 "광신도"
            { 50, "사이비" },
            { 100, "주교" }
        };

        foreach (var level in controlLevels)
        {   // 현재 신도 수가 최소 신도 수보다 적으면 반복 종료
            if (followerCount < level.Key)
            {
                break;
            }
            // 현재 신도 수가 최소 신도 수보다 크거나 같으면 해당 지배력 상태로 설정
            controlLevel = level.Value;

        }
        controlLevelTxt.text = controlLevel;
    }

    //세은님 필요하시면 가져다 쓰세요 메서드(현재 지배력 반환)
    public string GetCurrentControlLevel()
    {
        return controlLevel;
    }

    // 코인 텍스트 업데이트 및 표시 메서드
    public void UpdateCoinText()
    {
        coinTxt.text = $"{coinAmount}G";
    }

    // 포교 성공시, 코인 획득 메서드
    private int GetMoneyThroughPropagation()
    {
        if (isMobFollower)
            return moneyFromMob;
        else
            return moneyFromMob * 3;
    }

    // 포교한 대상 확인 메서드
    public bool PropagateMob(string tag)
    {
        bool isMob = tag == "Mob";
        isMobFollower = isMob;

        // 기존 코인 + 포교성공비
        coinAmount += GetMoneyThroughPropagation();

        return isMob;
    }

    // 코인 사용 메서드(신도유지비 차감 목적, 강화 목적)
    public void UseCoin(int useAmount)
    {
        if (coinAmount >= useAmount)
        {
            coinAmount -= useAmount;
            UpdateCoinText();
            isPayComplete = true;
            consecutivePay++;

            // 지불이 완료되면 제한 시간 코루틴 중지
            if (limitedCoroutine != null)
            {
                StopCoroutine(limitedCoroutine);
                limitedTimeImage.gameObject.SetActive(false); // 시계(경고용)이미지 비활성화
                audioSource.Stop();
            }
        }
        else
            Debug.Log("골드가 부족합니다.");
    }

    // 코인 수금 메서드(신도들로부터 코인 수금 목적)
    public void GetMoneyFromFollowers()
    {
        // 타이머 0일 때, isCollectable = true -> 수금 가능 
        if (isCollectable)
        {
            coinAmount += collectableMoney;
            UpdateCoinText();
            isCollectTimeRunning = false;
            isCollectable = false;
        }  
    }

    // 수금 가능한 코인량 반환 메서드
    private int CollectableAmount()
    {
        collectableMoney = followerCount * moneyFromMob * moneyReinforce;
        return collectableMoney;
    }

    // 코인 지불 메서드(신도유지비 지불 목적)
    public void PayExpense()
    {
        if (isPaytime)
        {
            UseCoin(fixedPayAmount); // 코인 사용 메서드
            if (isPayComplete) // 코인 지불 완료 시
            {
                UpdateCoinText();
                isPayTimeRunning = false;
                isPaytime = false;
                isPayComplete = false;
            }
        }
    }

    // 지불해야하는 코인량 반환 메서드
    private int PayAmount()
    {
        mustPayMoney = followerCount * moneyFromMob * 3;
        return mustPayMoney;
    }

    // 수금 또는 지불 간격 타이머 메서드
    private void TimerBeforeGetMoney() 
    {
        if (!isCollectTimeRunning)
        {
            currentCollectTimer = collectDuration;
            isCollectTimeRunning = true;
            StartCoroutine(TimerCoroutine(currentCollectTimer, collectTimerTxt, collectImage, false));
        }
        if (!isPayTimeRunning)
        {
            currentPayTimer = payDuration;
            isPayTimeRunning = true;
            limitedTimeImage.gameObject.SetActive(false);
            StartCoroutine(TimerCoroutine(currentPayTimer, payTimerTxt, payImage, true));
        }
    }

    private IEnumerator TimerCoroutine(float currentTimer, TextMeshProUGUI timerTxt, Image image, bool ispay)
    {
        while (currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(currentTimer / 60);
            int seconds = Mathf.FloorToInt(currentTimer % 60);
            timerTxt.text = string.Format("{1}", minutes, seconds);
            if (ispay)
            {
                payTimeImage.fillAmount = currentTimer / payDuration;
            }
            else
            {
                collectTimeImage.fillAmount = currentTimer / collectDuration;
            }


            yield return null;
        }
        timerTxt.text = "0";

        if (isCollectTimeRunning)
        {
            CollectableAmount();
            collectTimerTxt.text = $"{collectableMoney}G";
            isCollectable = true;
        }
        if (image.gameObject.tag == "Pay")
        {
            if (!isPaytime)
            {
                PayAmount();
            }
            fixedPayAmount = mustPayMoney; // 추가: 고정된 지불 코인 양 설정
            payTimerTxt.text = $"{fixedPayAmount}G";
            isPaytime = true;
            // 제한 시간 지나면 패널티
            LimitedTime();

        }
    }

    // 현재 코인값 반환 메서드
    public int CurrentCoin()
    {
        return coinAmount;
    }

    // 지불하기 제한시간 메서드
    private void LimitedTime()
    {
        currentLimitedTimer = limitedTimeDuration;
        limitedCoroutine = StartCoroutine(LimitedCoroutine(currentLimitedTimer));
        limitedTimeImage.gameObject.SetActive(true); // 시계(경고용)이미지 활성화
        audioSource.Play();
        limitedTxt.text = $"{currentLimitedTimer}";
    }

    private IEnumerator LimitedCoroutine(int currentLimitedTimer)
    {
        while (currentLimitedTimer > 0)
        {
            yield return new WaitForSeconds(1f); // 1초 대기
            currentLimitedTimer -= 1; // 1초 감소
            limitedTxt.text = $"{currentLimitedTimer}";
            yield return null;
        }
        if (limitedTimeImage.gameObject.activeSelf)
        {
            health--;
            calledDecreaseHealth++;
            loseHeartByEnemy = false;
            lifeController.ChangeLifeUI(health);
            LimitedTime();
        }
    }

    // StatManager를 통해 다른 스크립트에서 StatsData를 읽거나 수정
    //[데이터 불러오기]
    //int currentHealth = StatManager.Instance.Health; 
    //[데이터 수정]
    //StatManager.Instance.Health = currentHealth + 10;
}

