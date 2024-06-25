using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : Singleton<DatabaseManager>
{
    [SerializeField] string dialogueCSVFileName;
    [SerializeField] string eventCSVFileName;

    Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>();
    Dictionary<int, List<SelectDialogue>> eventDic = new Dictionary<int, List<SelectDialogue>>();

    public static bool isFinish = false;

    protected override void Awake()
    {
        base.Awake();

        if (Instance == this)
        {
            SetDialogueDictionary();
            SetEventDictionary();
        }
    }

    void SetDialogueDictionary()
    {
        DialogueParser parser = GetComponent<DialogueParser>();
        Dialogue[] dialogues = parser.Parse(dialogueCSVFileName);

        for (int i = 0; i < dialogues.Length; i++)
        {
            dialogueDic.Add(i + 1, dialogues[i]);
        }

        isFinish = true;
    }

    void SetEventDictionary()
    {
        EventParser parser = GetComponent<EventParser>();
        if (parser == null)
        {
            return;
        }

        SelectDialogue[] selectDialogues = parser.Parse(eventCSVFileName);

        foreach (var selectDialogue in selectDialogues)
        {
            if (!eventDic.ContainsKey(selectDialogue.ID))
            {
                eventDic[selectDialogue.ID] = new List<SelectDialogue>();
            }
            eventDic[selectDialogue.ID].Add(selectDialogue);
        }

    }

    public Dialogue[] GetDialogue(int startNum, int endNum)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();

        for (int i = startNum; i <= endNum; i++)
        {
            if (dialogueDic.ContainsKey(i))
            {
                dialogueList.Add(dialogueDic[i]);
            }
            
        }

        return dialogueList.ToArray();
    }

    public SelectDialogue[] GetSelectDialogues(int eventNumber)
    {
        if (eventDic.ContainsKey(eventNumber))
        {
            List<SelectDialogue> dialogues = eventDic[eventNumber];
            for (int i = 0; i < dialogues.Count; i++)
            {
                string replaceText = dialogues[i].Option;
                replaceText = replaceText.Replace("'", ",");
                replaceText = replaceText.Replace("-", "\n");
                dialogues[i].Option = replaceText;
            }
            return dialogues.ToArray();
        }
        return new SelectDialogue[0];
    }
}
