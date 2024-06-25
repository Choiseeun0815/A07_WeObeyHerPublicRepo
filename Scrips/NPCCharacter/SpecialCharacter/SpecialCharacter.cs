using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCharacter : MobCharacter
{
    public bool isCorrectAns = false;
    public int dialogCNT = 0;
    public string name;
    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if(isCorrectAns)
        {
            UpdateLikability();
        }

    }
}
