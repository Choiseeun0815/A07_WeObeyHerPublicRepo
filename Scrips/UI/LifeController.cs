using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{
    public Image[] lifeImage;
    
    public void ChangeLifeUI(int life)
    {
        if (StatManager.Instance.health == 0)
        {
            Debug.Log($"GameOver");
            StatManager.Instance.isGameOver = true;
            GameManager.Instance.GameOver("마음을 모두 소모한 그녀");
        }

        for (int i = 0; i < lifeImage.Length; i++)
        {
            if (i < life)
            {
                lifeImage[i].color = new Color(1, 0.5f, 0.5f, 1);
            }
            else
            {
                lifeImage[i].color = new Color(1, 1, 1, 0);
            }
        }
    }
}