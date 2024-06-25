using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySight : EnemyDetector
{
    [SerializeField][Range(0f, 1.0f)] private float detactedOpacity = 1.0f;
    [SerializeField] private float detactSpeed = 200.0f;

    public PlayerSO playerData;
    private SpriteRenderer sightRenderer;
    private Transform parentTransform;
    private float opacity;
    private SoundDB soundDB;
    private LifeController _lifeController;
    private int health;
    private int calledDecreaseHealth;

    private void Start()
    {
        soundDB = SoundManager.Instance.soundDB;
        health = StatManager.Instance.health;
        calledDecreaseHealth = StatManager.Instance.calledDecreaseHealth;

        sightRenderer = GetComponent<SpriteRenderer>();
        parentTransform = transform.parent;
        opacity = 0f;
    }

    private void Update()
    {
        ChangeOpacity(opacity);
        transform.RotateAround(parentTransform.position, Vector3.forward, detactSpeed * Time.deltaTime);

        CheckPlayerDead();
    }

    // Player 체력 체크해서 0이 되면 게임을 종료
    // GameManager에 추가할 코드

    // 나중에 PlayerSO Health를 private set으로 바꿔주고
    // StatManager에 추가할 코드
    private void CheckPlayerDead()
    {
        if (health == 0 && StatManager.Instance.loseHeartByEnemy)
        {
            StatManager.Instance.isGameOver = true;
            GameManager.Instance.GameOver("불시검문에 걸렸다!!!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (health > 0 && collision.CompareTag(playerTag)) 
        {
            SoundManager.Instance.PlayEffect(soundDB.enemyAttackSound, 0.5f, false);
            opacity = detactedOpacity;
            health--;
            calledDecreaseHealth++;
            StatManager.Instance.lifeController.ChangeLifeUI(--StatManager.Instance.health);
            StatManager.Instance.loseHeartByEnemy = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        opacity = 0f;
    }

    private void ChangeOpacity(float opacity)
    {
        Color color = sightRenderer.color;
        color.a = opacity;
        sightRenderer.color = color;
    }
}
