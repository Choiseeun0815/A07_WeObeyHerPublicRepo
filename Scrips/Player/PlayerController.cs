using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInputs playerInputs { get; private set; } // 플레이어 입력 시스템
    public PlayerInputs.PlayerActions playerActions { get; private set; }

    public event Action OnLeftClick; // 왼쪽 클릭 이벤트

    private void Awake()
    {
        // PlayerInputs 인스턴스 생성
        playerInputs = new PlayerInputs();
        playerActions = playerInputs.Player;

        // LeftClick 액션에 대한 콜백 추가
        playerActions.LeftClick.performed += context => OnLeftClick?.Invoke();
    }

    private void OnEnable()
    {
        // 입력 시스템 활성화
        playerInputs.Enable();
    }

    private void OnDisable()
    {
        // 입력 시스템 비활성화
        playerInputs.Disable();
    }

    // 마우스 입력을 받는 함수 추가
    public Vector2 GetMouseDelta()
    {   //플레이어의 마우스 움직임 변화(델타)량을 Vector2 형태로 반환
        return playerActions.MouseDelta.ReadValue<Vector2>();
    }

    // 마우스 왼쪽 버튼 상태 확인 함수 추가
    public bool IsLeftMouseButtonPressed()
    {
        return playerActions.LeftClick.IsPressed();
    }
}