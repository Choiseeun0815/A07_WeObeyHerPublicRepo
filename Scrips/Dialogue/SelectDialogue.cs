using UnityEngine;
[System.Serializable]
public class SelectDialogue
{
    public int ID; // 선택지 ID
    public string Option; // 선택지 텍스트
    public int NextLine; // 다음 대화 라인 번호
    public bool IsCorrect; // 선택지가 정답인지에 대한 정보
}

[System.Serializable]
public class SelectEvent
{
    public SelectDialogue[] Selecter { get; internal set; }
}