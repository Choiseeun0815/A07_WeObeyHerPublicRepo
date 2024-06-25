using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Achievement : Singleton<Achievement>
{
    // 9개의 업적에 대한 달성 여부를 저장할 변수
    public bool[] isAchievementComplete = new bool[9];

    // 총 달성한 업적의 수 → 게임 종료 화면에서 필요시 넣을 수 있을 것
    public int completeCnt = 0;

    [SerializeField] Button clearBtn;
    private void Start()
    {
        clearBtn.gameObject.SetActive(false);
        SetAchievmentInit();
    }

    private void Update()
    {
        if (!isAchievementComplete[0])
            CheckAchievement01();

        if (!isAchievementComplete[1])
            CheckAchievement02();

        if (!isAchievementComplete[2])
            CheckAchievement03();

        if (!isAchievementComplete[3])
            CheckAchievement04();

        if (!isAchievementComplete[4])
                CheckAchievement05();

        if (!isAchievementComplete[7]) // 업적 6,7,8
            CheckAchievement08();

        if (!isAchievementComplete[8])
            CheckAchievement09();
        else
        {
            // 모든 네임드를 모으면 게임 클리어하기 버튼 활성화
            clearBtn.gameObject.SetActive(true);
        }
    }

   

    // 업적[01] - 플러팅의 신
    // 네임드 신도들과의 대화에서 한 번도 선택지를 틀리지 않았을 경우 clear
    void CheckAchievement01()
    {
        int cnt = 0;

        for (int i = 0; i < GameManager.Instance.specialCharacters.Count; i++)
        {
            SpecialCharacter sc = GameManager.Instance.specialCharacters[i];
            if(sc.isCorrectAns && sc.dialogCNT == 1)
            {
                cnt++;
            }
        }

        if (cnt == 5)
        {
            completeCnt++;
            isAchievementComplete[0] = true;
        }
    }

    // 업적[02] - 부자 꿈나무
    // 현재 GOLD가 3000원 이상일 때 clear
    void CheckAchievement02()
    {
        if(StatManager.Instance.CurrentCoin() >= 3000)
        {
            completeCnt++;
            isAchievementComplete[1] = true;
        }
    }

    // 업적[03] - 불사신
    // 라이프가 3번 이상 깎였음에도 생존중이라면 clear
    void CheckAchievement03()
    {
        // 플레이어가 사망하지 않은 상태에서, 라이프가 감소한 횟수가 3번을 넘는다면
        if(!StatManager.Instance.isGameOver && StatManager.Instance.calledDecreaseHealth >=3)
        {
            completeCnt++;
            isAchievementComplete[2] = true;
        }
    }

    // 업적[04] - 미꾸라지
    // 적의 추적 상태에서 5회 벗어난다
    void CheckAchievement04()
    {
        int cnt = 0;

        // 맵에 존재하는 모든 enemy를 가져옴
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        for(int i=0;i<enemies.Length;i++)
        {
            cnt += enemies[i].lostCount;
        }

        if (cnt >= 5)
        {
            completeCnt++;
            isAchievementComplete[3] = true;
        }
    }

    // 업적[05] - 시간은 금
    // 경고 카운트를 지켜 신도 유지비를 연달아 n회 지불
    void CheckAchievement05()
    {
        int cnt = StatManager.Instance.consecutivePay; //연달아서 지불한 수를 저장

        if (cnt >= 5)
        {
            completeCnt++;
            isAchievementComplete[4] = true;
        }
    }

    // 업적[06~08] - 지배자
    // 주교 단계의 지배력을 달성하면 clear(광신도, 사이비에 대한 충족도 또한 이곳에서 판단)
    void CheckAchievement08()
    {
        if (StatManager.Instance.GetCurrentControlLevel() == "주교")
        {
            completeCnt++;
            isAchievementComplete[7] = true;
        }
        else if (StatManager.Instance.GetCurrentControlLevel() == "사이비")
        {
            completeCnt++;
            isAchievementComplete[6] = true;
        }
        else if(StatManager.Instance.GetCurrentControlLevel() == "광신도")
        {
            completeCnt++;
            isAchievementComplete[5] = true;
        }
    }

    // 업적[09] - WE OBEY U
    // 모든 네임드 신도들을 수집하면 clear
    void CheckAchievement09()
    {
        int cnt = 0;
        for (int i = 0; i < GameManager.Instance.specialCharacters.Count; i++)
        {
            SpecialCharacter sc = GameManager.Instance.specialCharacters[i];
            if (sc.isCorrectAns)
            {
                cnt++;
            }
        }

        if (cnt == 5)
        {
            completeCnt++;
            isAchievementComplete[8] = true;
        }
    }

    void SetAchievmentInit()
    {
        for(int i=0;i<isAchievementComplete.Length;i++)
        {
            isAchievementComplete[i] = false;
        }
    }
}