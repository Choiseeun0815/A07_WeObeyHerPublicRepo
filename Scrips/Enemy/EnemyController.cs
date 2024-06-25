using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EnemyDetector
{
    private Vector2 targetPos;

    // follow Player

    [Header("Detact")]
    [SerializeField][Range(0f, 2.0f)] private float stopRange = 2.0f;
    [SerializeField][Range(0f, 100f)] private float followRange = 50f;

    [SerializeField] private SpriteRenderer characterRenderer;

    private bool wasChasing = false; // 이전 프레임의 추격 상태 체크
    public GameObject enemySightObj;
    public int lostCount = 0; // 플레이어를 놓친 횟수

    private void FixedUpdate()
    {
        Vector2 direction = Vector2.zero;

        if (DistanceToTarget() < stopRange)
        {
            direction = Vector2.zero;
            enemySightObj.SetActive(true);
        }
        else if (DistanceToTarget() < followRange)
        {
            direction = DirectionToTarget();
            wasChasing = true;
            enemySightObj.SetActive(false);
            targetPos = ClosestTarget.transform.position;
            UpdateAnimation();
        }
        else
        {
            if (!Achievement.Instance.isAchievementComplete[3])
            {
                if (wasChasing)
                {
                    lostCount++;
                    wasChasing = false;
                    StopAnimation(AnimationData.WalkParameterHash);
                    StartAnimation(AnimationData.IdleParameterHash);
                }
            }
        }
        enemyData.Move(direction);
    }

    private void UpdateAnimation()
    {
        StopAnimation(AnimationData.IdleParameterHash);
        float dirX = targetPos.x - enemyData.transform.position.x;
        float dirY = targetPos.y - enemyData.transform.position.y;

        StartAnimation(AnimationData.WalkParameterHash);
        SetAnimation(AnimationData.DirXParameterHash, AnimationData.DirYParameterHash, dirX, dirY);
    }
}