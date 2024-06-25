using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBtn : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    public void OpenButtonOnClick()
    {
        if (Time.timeScale == 1f)
        {
            StopAllCoroutines();
            _panel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void CloseButtonOnClick()
    {
        if (Time.timeScale == 0f)
        {
            _panel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void QuitButton()
    {
        StopAllCoroutines();
        Application.Quit();
    }
}
