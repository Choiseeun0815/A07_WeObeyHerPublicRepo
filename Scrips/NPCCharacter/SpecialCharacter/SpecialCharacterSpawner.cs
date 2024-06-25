using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCharacterSpawner : MonoBehaviour
{
    public List<SpecialCharacter> specialCharacters;

    int currentFollowers = 0;
    public int nextCharacterIDX = 0; // 생성할 네임드 신도의 인덱스

    // 네임드 신도를 스폰하기 위한 팔로워 기준
    int[] spawnCondition = new int[] { 10, 25, 40, 55, 70 };

    bool showPrompt = true;

    private void Update()
    {
        currentFollowers = StatManager.Instance.followerCount;
        SpawnSpecialCharacter();
    }

    void SpawnSpecialCharacter()
    {
        if (nextCharacterIDX >= specialCharacters.Count)
            return;

        if(currentFollowers >= spawnCondition[nextCharacterIDX])
        {
            SpecialCharacter sc = specialCharacters[nextCharacterIDX];
            sc.transform.position = GameManager.Instance.mobSpawner.GetRandomPos();
            GameManager.Instance.specialCharacters[nextCharacterIDX] = Instantiate(sc);

            nextCharacterIDX++;

            showPrompt = false;
        }

        if (!showPrompt)
        {
            ShowAlertPopup();
            showPrompt = true;
        }
    }

    void ShowAlertPopup()
    {
        UIManager.Instance.ShowPopupUI<UI_Alert>();
        
        // StartCoroutine(HidePromptAfterDelay(3f)); // 3초 뒤에 팝업을 닫는 코루틴 시작
    }
}