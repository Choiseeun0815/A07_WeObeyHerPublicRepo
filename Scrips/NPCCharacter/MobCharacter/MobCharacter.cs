using UnityEngine;
using System.Collections;

public class MobCharacter : MonoBehaviour
{
    [Header("Common Info")]
    // 포교 여부를 나타내는 변수 
    public bool isPropagated = false;

    [Header("Respawn Time")]
    // 
    [SerializeField] protected float respawnTime;
    [SerializeField] protected float currentTime;
    [SerializeField] protected float decreaseRate = 1f;

    [Header("Likability")]
    // 호감도 변수 
    public float currentLikability = 0;
    public float maxLikabliity = 10;
    public float decreaseRateLikability = 1.5f;
    protected LikabilityBar likabilityBar;

    // 이동 관련 변수
    protected MobCharacterMoveController moveController;

    [field: Header("Animations")]
    [field: SerializeField] public AnimationData AnimationData { get; private set; }

    public Animator Animator { get; private set; }

    private void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Start()
    {
        currentTime = respawnTime;
        GameManager.Instance.AddMobCharacter(this);
        moveController = GetComponent<MobCharacterMoveController>(); // MoveController 참조
        likabilityBar = GetComponent<LikabilityBar>();

    }
    void Update()
    {
        UpdateLikability();
    }
    protected void UpdateLikability()
    {
        currentLikability -= decreaseRateLikability * Time.deltaTime;

        if (currentLikability < 0)
            currentLikability = 0;

        likabilityBar.UpdateSliderBar(currentLikability, maxLikabliity);
    }

    public void CheckHunger()
    {
        if (isPropagated)
        {
            currentTime -= decreaseRate * Time.deltaTime;
        }

        if (isPropagated && currentTime <= 0)
        {
            isPropagated = false;
        }
        // 신도에서 민간인으로 돌아온 자들은 다시 리셋
        if (currentTime <= 0 && gameObject.activeSelf)
        {
            currentTime = respawnTime;

            currentLikability = 0;
            maxLikabliity *= 1.1f;
            likabilityBar.SetSliderValueInit();
            likabilityBar.ChangeSliderColor();

            isPropagated = false;

            moveController.StartCoroutine(moveController.MoveInterval());
        }
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
