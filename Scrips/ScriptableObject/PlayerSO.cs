using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Player", menuName = "NewPlayer")]

public class PlayerSO : ScriptableObject
{
    [field: SerializeField][field: Range(0f, 25f)] public float BaseSpeed { get; private set; } = 3f;
    [field: SerializeField][field: Range(0f, 25f)] public float BaseRotationDamping { get; private set; } = 1f;


    [field: Header("IdleData")]

    [field: Header("WalkData")]
    [field: SerializeField][field: Range(0f, 2f)] public float WalkSpeedModifier { get; set; } = 0.8f;

    [field:Header("StatsData")]
    [field: SerializeField] public int Health { get; set; } = 3;
    
}
