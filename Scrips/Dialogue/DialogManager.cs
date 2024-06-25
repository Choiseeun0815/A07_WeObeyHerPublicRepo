using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : Singleton<DialogManager>
{
    [SerializeField] GameObject dialogueUI;
    [SerializeField] GameObject promptUI;
    [SerializeField] GameObject choiceUI;

    [SerializeField] TextMeshProUGUI textDialogue;
    [SerializeField] TextMeshProUGUI nameDialogue;

    [SerializeField] TextMeshProUGUI[] choiceTexts;
    [SerializeField] Button[] choiceButtons;

    [SerializeField] float textDelay;

    Dialogue[] dialogues;
    SelectDialogue[] selectDialogues;

    bool isDialogue = false; // 현재 대화 중인 경우 조건식 true
    bool isNext = false; // 특정 키 입력 대기 -> 다음 대사로 넘어가기 위한 키
    bool isTyping = false; // TypeDialogue 코루틴이 실행 중인지 여부

    int lineCount = 0; // 주고 받는 대화의 수 
    int contextCount = 0; // 현재 말하고 있는 캐릭터의 대사 수

    public bool isAllClear = false;

    public SpecialCharacter sc;
    public bool timeStopOnce = false;

    private SoundDB soundDB;

    private void Start()
    {
        dialogueUI.SetActive(false);
        choiceUI.SetActive(false);
        soundDB = SoundManager.Instance.soundDB;
    }

    private void Update()
    {
        if (isDialogue)
        {
            if (!timeStopOnce)
            {
                Time.timeScale = 0;
                timeStopOnce = true;
            }
            if (isNext && Input.GetKeyDown(KeyCode.Space))
            {
                isNext = false;
                textDialogue.text = "";

                if (++contextCount < dialogues[lineCount].contexts.Length)
                {
                    StartCoroutine(TypeDialogue());
                }
                else
                {
                    contextCount = 0;

                    if (++lineCount < dialogues.Length)
                    {
                        StartCoroutine(TypeDialogue());
                    }
                    else
                    {
                        if (dialogues[lineCount - 1].eventNumber > 0)
                        {
                            ShowChoices(dialogues[lineCount - 1].eventNumber);
                        }
                        else
                        {
                            EndDialogue();
                        }
                    }
                }
            }
        }
        
    }

    public void ShowDialogue()
    {
        if (isTyping) return; // 중복 실행 방지

        textDialogue.text = "";
        nameDialogue.text = "";

        isDialogue = true;
        PromptManager.Instance.ClosePromptPanel();

        StartCoroutine(TypeDialogue());
    }

    private void EndDialogue()
    {
        isDialogue = false;
        contextCount = 0;
        lineCount = 0;
        PromptManager.Instance.OpenPromptPanel();
        isNext = false;
        SettingDialogueUI(false);
    }

    void SettingDialogueUI(bool toggle)
    {
        dialogueUI.SetActive(toggle);
    }

    public void GetDialogues(Dialogue[] dialogues)
    {
        this.dialogues = dialogues;
    }

    IEnumerator TypeDialogue()
    {
        if (isTyping) yield break; // 중복 실행 방지

        isTyping = true;
        SettingDialogueUI(true);

        if (lineCount >= dialogues.Length)
        {
            yield break;
        }

        if (contextCount >= dialogues[lineCount].contexts.Length)
        {
            yield break;
        }

        string replaceText = dialogues[lineCount].contexts[contextCount];
        replaceText = replaceText.Replace("'", ",");
        replaceText = replaceText.Replace("-", "\n");

        string name = dialogues[lineCount].name;
        name = name.Replace("ⓦ", "<color=#ffffff>");
        name = name.Replace("ⓨ", "<color=#FFFF00>");
        name = name + "</color>"; // 색상 태그 닫기
        nameDialogue.text = name;

        for (int i = 0; i < replaceText.Length; i++)
        {
            SoundManager.Instance.PlayEffect(soundDB.cancelSound,0.08f,false);
            textDialogue.text += replaceText[i];
            yield return new WaitForSecondsRealtime(textDelay);
        }

        PromptManager.Instance.ClosePromptPanel();
        isNext = true;
        isTyping = false;
    }

    void ShowChoices(int eventNumber)
    {
        selectDialogues = DatabaseManager.Instance.GetSelectDialogues(eventNumber);
        sc.dialogCNT++;

        if (selectDialogues.Length == 0)
        {
            return;
        }

        choiceUI.SetActive(true);

        int numChoices = Mathf.Min(choiceTexts.Length, selectDialogues.Length);

        // 모든 선택지를 비활성화
        for (int i = 0; i < choiceTexts.Length; i++)
        {
            choiceTexts[i].transform.parent.gameObject.SetActive(false);
            choiceButtons[i].gameObject.SetActive(false);
        }

        // 필요한 선택지만 활성화하고 설정
        for (int i = 0; i < numChoices; i++)
        {
            choiceTexts[i].transform.parent.gameObject.SetActive(true);
            choiceTexts[i].text = selectDialogues[i].Option;
            choiceButtons[i].gameObject.SetActive(true);

            choiceButtons[i].onClick.RemoveAllListeners();
            choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(i));
        }

    }

    public void OnChoiceSelected(int choiceIndex)
    {
        choiceUI.SetActive(false);

        if (choiceIndex >= 0 && choiceIndex < selectDialogues.Length)
        {
            int nextLine = selectDialogues[choiceIndex].NextLine;
            bool isCorrect = selectDialogues[choiceIndex].IsCorrect; // 정답 여부 확인
            dialogues = DatabaseManager.Instance.GetDialogue(nextLine, nextLine);

            if (dialogues.Length == 0 || dialogues[0].contexts.Length == 0)
            {
                ResetToInitialDialogue();
            }
            else
            {
                lineCount = 0;
                contextCount = 0;
                ShowDialogue();

                if (isCorrect)
                {
                    SoundManager.Instance.PlayEffect(soundDB.correctSound, 0.8f, false);
                    sc.isCorrectAns = true;
                    PromptManager.Instance.dominateBtnColor.color = Color.white;
                }
                else
                {
                    //SoundManager.Instance.PlayEffect(soundDB.failSound, 0.8f, false);
                    sc.isCorrectAns = false;
                    isAllClear = false;
                    PromptManager.Instance.dominateBtnColor.color = Color.gray;
                }
            }
        }
        else
        {
            ResetToInitialDialogue();
        }
    }

    public void ResetToInitialDialogue()
    {
        lineCount = 0;
        contextCount = 0;
        ShowDialogue();
    }

}