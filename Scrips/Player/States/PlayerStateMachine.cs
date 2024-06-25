using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }

    // States
    public PlayerIdleState IdleState { get; }
    public PlayerWalkState WalkState { get; }

    // 입력 관련 변수
    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        // 상태 초기화
        IdleState = new PlayerIdleState(this);
        WalkState = new PlayerWalkState(this);

        MovementSpeed = player.Data.BaseSpeed;
        RotationDamping = player.Data.BaseRotationDamping;
    }

    // 상태 초기 설정
    private void Start()
    {
        ChangeState(IdleState);
    }
}
