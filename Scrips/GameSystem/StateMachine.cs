
// 상태 인터페이스
public interface IState
{
    void Enter();
    void Exit();
    void HandleInput();
    void Update();

}

// 상태 추상클래스
public abstract class StateMachine
{
    protected IState currentState;

    public void ChangeState(IState state) //상태 전환 메서드
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }

    public void HandleInput()
    {
        currentState?.HandleInput();
    }

    public void Update()
    {
        currentState?.Update();
    }
}