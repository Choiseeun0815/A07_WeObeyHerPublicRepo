using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    // detact Player

    public Transform ClosestTarget { get; private set; }
    public EnemyData enemyData;

    [SerializeField] public string playerTag = "Player";

    [field: SerializeField] public AnimationData AnimationData { get; private set; }
    public Animator Animator {  get; private set; }

    private void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        enemyData = GetComponent<EnemyData>();
        ClosestTarget = GameObject.FindGameObjectWithTag(playerTag).transform;
    }

    public float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, ClosestTarget.position);
    }

    public Vector2 DirectionToTarget()
    {
        return (ClosestTarget.position - transform.position).normalized;
    }

    public void StartAnimation(int animatorHash)
    {
        Animator.SetBool(animatorHash, true);
    }

    public void SetAnimation(int animatorHashX, int animatorHashY, float DirX, float DirY)
    {
        Animator.SetFloat(animatorHashX, DirX);
        Animator.SetFloat(animatorHashY, DirY);
    }

    public void StopAnimation(int animatorHash)
    {
        Animator.SetBool(animatorHash, false);
    }
}