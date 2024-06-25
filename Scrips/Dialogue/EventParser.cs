using System.Collections.Generic;
using UnityEngine;

public class EventParser : MonoBehaviour
{
    public SelectDialogue[] Parse(string _CSVFileName)
    {
        List<SelectDialogue> eventList = new List<SelectDialogue>();
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);

        if (csvData == null)
        {
            return eventList.ToArray();
        }

        string[] data = csvData.text.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

        int currentID = -1; // 현재 이벤트 ID
        for (int i = 1; i < data.Length; i++)
        {
            string[] row = data[i].Split(',');
            if (row.Length < 4) continue;

            if (!string.IsNullOrWhiteSpace(row[0]))
            {
                currentID = int.Parse(row[0]);
            }

            // 현재 ID가 설정되지 않은 경우 스킵
            if (currentID == -1)
            {
                continue;
            }

            SelectDialogue selectDialogue = new SelectDialogue
            {
                ID = currentID,
                Option = row[1].Trim(' ', '"', '\''), // 따옴표와 공백, 작은 따옴표 제거
                NextLine = int.Parse(row[2]),
                IsCorrect = bool.Parse(row[3]) // 정답 여부 파싱
            };

            eventList.Add(selectDialogue);
            
        }

        return eventList.ToArray();
    }
}
