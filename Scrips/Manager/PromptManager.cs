using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptManager : Singleton<PromptManager>
{
    [Header("Prompt")]
    public GameObject promptPanel; // 프롬프트 패널
    public bool isCivilianDetected = false;
    public event Action OnPromptClosed;
    public Image dominateBtnColor;

    private GameObject detectedCivilian; 

    public List<GameObject> FollowerList = new List<GameObject>(); 

    private void Start()
    {
        ClosePromptPanel();
        dominateBtnColor.color = Color.gray;
    }

    public void SetDetectedCivilian(GameObject civilian)
    {
        detectedCivilian = civilian;
    }

    public void ClosePromptPanel()
    {
        if (promptPanel.activeSelf)
        {
            promptPanel.SetActive(false);
            OnPromptClosed?.Invoke();
            DialogManager.Instance.timeStopOnce = false;
            Time.timeScale = 1;
        }
    }
    public void OpenPromptPanel()
    {
        if (!promptPanel.activeSelf)
        {
            Time.timeScale = 0;
            promptPanel.SetActive(true);
        }
    }

    public void Dominate()
    {
        if (detectedCivilian == null) return;

        StatManager.Instance.PropagateMob(detectedCivilian.tag);
        FollowerList.Add(detectedCivilian);
        detectedCivilian.tag = "Follower";

        SoundManager.Instance.PlayEffect(SoundManager.Instance.soundDB.propagatSound, 0.1f, false);

        // 1초 뒤 민간인 오브젝트 비활성화 코루틴 실행
        StartCoroutine(DeactivateAfterTime(1f));
        ClosePromptPanel();
    }
    public void DominateSpecial()
    {
        if (detectedCivilian == null) return;

        SpecialCharacter sc = detectedCivilian.GetComponent<SpecialCharacter>();
        string name = sc.name;
        if (sc.isCorrectAns)
        {
            SoundManager.Instance.playSoundByname(name);
            StatManager.Instance.PropagateMob(detectedCivilian.tag);
            FollowerList.Add(detectedCivilian);

            detectedCivilian.tag = "Follower";

            // 1초 뒤 민간인 오브젝트 비활성화 코루틴 실행
            StartCoroutine(DeactivateAfterTime(1f));
            dominateBtnColor.color = Color.gray;
            ClosePromptPanel();
        }
        else
        {
            dominateBtnColor.color = Color.gray;
        }
    }

    // 민간인 오브젝트 비활성화 코루틴
    private IEnumerator DeactivateAfterTime(float time)
    {
        Transform follower = detectedCivilian.transform.Find("Follower");
        Transform slider = detectedCivilian.transform.Find("LikeBar");
        if (follower != null)
        {
            if(slider != null)
            {
                slider.gameObject.SetActive(false);
            }
            follower.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(time);

        if (follower != null)
        {
            follower.gameObject.SetActive(false);
        }
        if (detectedCivilian != null)
        {
            detectedCivilian.SetActive(false);
        }
        StatManager.Instance.isFollowerAdded = true; // 팔로워 수 업데이트 시작
        if(slider != null)
        {
            slider.gameObject.SetActive(true);
        }
    }
}