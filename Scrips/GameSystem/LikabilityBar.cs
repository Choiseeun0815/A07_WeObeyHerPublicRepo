using System;
using UnityEngine;
using UnityEngine.UI;

public class LikabilityBar : MonoBehaviour
{
    private Slider sliderBar;
    private Image silderColor;

    int cnt = 0;
    private void Awake()
    {
        sliderBar = GetComponentInChildren<Slider>();

        if(sliderBar !=null)
        {
            silderColor = sliderBar.fillRect.GetComponent<Image>();
            silderColor.color = Color.green;
        }
    }

    public void UpdateSliderBar(float curState, float maxState)
    {
        sliderBar.value = curState/maxState;
    }

    public void SetSliderValueInit()
    {
        sliderBar.value = 0;
        cnt++;
    }

    public void ChangeSliderColor()
    {
        switch (cnt % 3)
        {
            case 0:
                silderColor.color = Color.green; break;
            case 1:
                silderColor.color = Color.yellow; break;
            case 2:
                silderColor.color = Color.red; break;
            default: break;
        }
    }
}
