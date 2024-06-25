using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public void StartBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void RetryBtn()
    {
        SceneManager.LoadScene(1);
    }
    public void GameExit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; // 유니티 에디터 플레이 모드 종료
#else
        Application.Quit(); //빌드된 게임을 종료
#endif
    }
     public void ClearBtn()
     {
        SceneManager.LoadScene(2);
     }
    

}
