using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AnimationData
{
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string walkParameterName = "Walk";
    [SerializeField] private string dirXParameterName = "DirX";
    [SerializeField] private string dirYParameterName = "DirY";
    
    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int DirXParameterHash { get; private set; }
    public int DirYParameterHash { get; private set; }

    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        WalkParameterHash = Animator.StringToHash(walkParameterName);
        DirXParameterHash = Animator.StringToHash(dirXParameterName);
        DirYParameterHash = Animator.StringToHash(dirYParameterName);
    }
}
