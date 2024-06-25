using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerSO playerSO;

    private Transform civilianTransform;
    private GameObject detectedCivilian; // 플레이어와 충돌한 민간인

    private Vector2 targetDirection; // 목표 방향
    private bool isMoving = false; // 이동 여부 확인

    private float mouseButtonDownTime = 0f; // 마우스 버튼이 눌린 시간 추적
    private const float requiredHoldTime = 0.2f; // 마우스 버튼을 눌러야 하는 최소 시간

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        playerSO = stateMachine.Player.Data;
    }


    public virtual void Enter()
    {
        AddInputActionsCallbacks(); // 입력 액션 콜백 추가
        PromptManager.Instance.OnPromptClosed += HandlePromptClosed;
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks(); // 입력 액션 콜백 제거
        PromptManager.Instance.OnPromptClosed -= HandlePromptClosed;
    }

    protected void StartAnimation(int animatorHash)
    {
        stateMachine.Player.Animator.SetBool(animatorHash, true);
    }

    protected void SetAnimation(int animatorHashX, int animatorHashY)
    {
        //마우스의 월드좌표를 받아오는 대신, targetDirection 값 사용(update문에서 실시간 값)
        stateMachine.Player.Animator.SetFloat(animatorHashX, targetDirection.x);
        stateMachine.Player.Animator.SetFloat(animatorHashY, targetDirection.y);
    }

    protected void StopAnimation(int animatorHash)
    {
        stateMachine.Player.Animator.SetBool(animatorHash, false);
    }

    public virtual void HandleInput()
    {
        // 이동 입력 읽기
        ReadMovementInput();

        if (stateMachine.Player.Input.IsLeftMouseButtonPressed()) // 왼쪽 마우스 버튼이 눌린 상태 확인
        {
            mouseButtonDownTime += Time.deltaTime; // 마우스 버튼이 눌린 시간 증가

            if (!isMoving && mouseButtonDownTime >= requiredHoldTime) // 목표 방향 설정
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 위치 가져오기
                targetDirection = (mousePosition - (Vector2)stateMachine.Player.transform.position).normalized; // 목표 방향 계산
                isMoving = true; // 이동 시작
            }
        }
        else
        {
            mouseButtonDownTime = 0f; // 마우스 버튼이 떼어지면 시간 초기화
            isMoving = false; // 이동 중지
        }
    }

    public virtual void Update()
    {
        // 프롬프트 패널 비활성화 시, 이동
        if (!PromptManager.Instance.promptPanel.activeSelf && isMoving)
        {
            UpdateTargetDirection(); // 실시간으로 목표 방향 업데이트
            Move(); // 이동 로직 실행
        }
        // 민간인과 충돌하지 않았으면, Ray로 충돌 체크
        if (!PromptManager.Instance.isCivilianDetected)
        {
            CheckForCollisionWithCivilian();
        }
        else
        {   // 민간인과 충돌했으면, 플레이어와 민간인 사이 거리 체크
            CheckCivilianDistance();
        }
    }

    protected virtual void AddInputActionsCallbacks()
    {
        PlayerController input = stateMachine.Player.Input; // 입력 액션 콜백 추가
        input.playerActions.MouseDelta.canceled += OnMovementCanceled;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerController input = stateMachine.Player.Input; // 입력 액션 콜백 제거
        input.playerActions.MouseDelta.canceled -= OnMovementCanceled;
    }

    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {
        if (stateMachine.MovementInput == Vector2.zero) return; // 이동 입력이 없으면 Idle 상태로 전환

        stateMachine.ChangeState(stateMachine.IdleState);
    }

    private void ReadMovementInput()
    {
        stateMachine.MovementInput = stateMachine.Player.Input.GetMouseDelta(); // 이동 입력 읽기
    }

    // 마우스 위치 업데이트 메서드
    private void UpdateTargetDirection()
    {
        // 마우스 위치 가져오기
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        targetDirection = (mousePosition - (Vector2)stateMachine.Player.transform.position).normalized; // 목표 방향 계산
    }


    private void Move()
    {
        if (!stateMachine.Player.Input.IsLeftMouseButtonPressed() || mouseButtonDownTime < requiredHoldTime)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
        // 목표 방향으로 플레이어 이동
        Transform playerTransform = stateMachine.Player.transform;
        playerTransform.position += (Vector3)targetDirection * GetMovementSpeed() * Time.deltaTime;
    }

    private float GetMovementSpeed()
    {   // 이동 속도 계산
        return stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier; 
    }

    private void CheckForCollisionWithCivilian()
    {
        // 플레이어의 CapsuleCollider2D 컴포넌트
        CapsuleCollider2D capsuleCollider = stateMachine.Player.GetComponent<CapsuleCollider2D>();
        if (capsuleCollider == null)
        {
            Debug.LogError("CapsuleCollider2D 컴포넌트를 찾을 수 없습니다!");
            return;
        }

        // 플레이어의 현재 위치
        Vector2 playerPosition = stateMachine.Player.transform.position;

        // CapsuleCollider2D의 크기 정보 가져오기
        float radius = capsuleCollider.size.y / 2f; // CapsuleCollider2D의 세로 길이를 반지름으로 사용

        // 원형 영역 내에서 모든 충돌체 검출
        RaycastHit2D[] hits = Physics2D.CircleCastAll(playerPosition, radius, Vector2.zero);

        foreach (var hit in hits)
        {
            // 충돌한 객체의 태그가 "Civilian"인지 확인
            if (hit.collider.CompareTag("Civilian"))
            {
                PromptManager.Instance.OpenPromptPanel(); // 프롬프트 활성화
                stateMachine.ChangeState(stateMachine.IdleState); // PlayerIdleState로 상태 전환

                // 민간인 충돌 체크 확인(true이면, Update구문에서 충돌여부체크 메서드 중지)
                PromptManager.Instance.isCivilianDetected = true; 

                civilianTransform = hit.collider.transform;

                detectedCivilian = hit.collider.gameObject;
                // PromptManager에 민간인 오브젝트 설정
                PromptManager.Instance.SetDetectedCivilian(detectedCivilian);

                DialogManager.Instance.sc = hit.collider.gameObject.GetComponent<SpecialCharacter>();
                if (!DialogManager.Instance.sc.isCorrectAns) PromptManager.Instance.dominateBtnColor.color = Color.gray;
                else PromptManager.Instance.dominateBtnColor.color = Color.white;

                InteractionEvent intercationEvent = detectedCivilian.transform.GetComponent<InteractionEvent>();
                Dialogue[] dialogues = intercationEvent.GetDialogue();
                DialogManager.Instance.GetDialogues(dialogues);

                break; // 충돌한 한 개만 처리하면 됨
            }
        }
    }

    private void CheckCivilianDistance()
    {
        // 플레이어의 CapsuleCollider2D 컴포넌트
        CapsuleCollider2D capsuleCollider = stateMachine.Player.GetComponent<CapsuleCollider2D>();
        if (capsuleCollider == null)
        {
            Debug.LogError("CapsuleCollider2D 컴포넌트를 찾을 수 없습니다!");
            return;
        }

        // CapsuleCollider2D의 크기 정보 가져오기
        float radius = capsuleCollider.size.x * 2;

        if (civilianTransform == null) return;
        // 플레이어와 민간인 사이 거리 체크
        float distance = Vector3.Distance(stateMachine.Player.transform.position, civilianTransform.position);
        // 거리 초과 시 민간인 감지 상태 해제
        if (distance > radius)
        {
            PromptManager.Instance.isCivilianDetected = false;
        }
    }

    private void HandlePromptClosed()
    {   // 프롬프트 종료 시 Walk 상태로 전환
        if (!PromptManager.Instance.isCivilianDetected)
        {
            stateMachine.ChangeState(stateMachine.WalkState);
        }
    }
    
}