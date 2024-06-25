using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = playerSO.WalkSpeedModifier;
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.WalkParameterHash);
        SetAnimation(stateMachine.Player.AnimationData.DirXParameterHash, stateMachine.Player.AnimationData.DirYParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.WalkParameterHash);
    }

    public override void HandleInput()
    {
        base.HandleInput();
        // 왼쪽 마우스 버튼이 눌리지 않은 경우
        if (!stateMachine.Player.Input.IsLeftMouseButtonPressed())
        {   // IdleState로 전환
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}
