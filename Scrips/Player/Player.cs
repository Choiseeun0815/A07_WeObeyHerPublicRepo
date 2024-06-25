using UnityEngine;

public class Player : MonoBehaviour
{
    [field: Header("References")]
    [field: SerializeField] public PlayerSO Data { get; private set; }

    public PlayerController Input { get; private set; }

    private PlayerStateMachine stateMachine;

    [field: Header("Animations")]
    [field: SerializeField] public AnimationData AnimationData { get; private set; }

    public Animator Animator { get; private set; }

    private void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerController>();
        stateMachine = new PlayerStateMachine(this);
    }
    void Start()
    {
        stateMachine.ChangeState(stateMachine.IdleState); 
    }

    private void Update()
    {
        stateMachine.HandleInput(); // 입력
        stateMachine.Update(); // 상태머신
    }
}
