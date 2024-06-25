using UnityEngine;
using UnityEngine.InputSystem;

public class MobClicker : MonoBehaviour
{
    private PlayerController playerController;
    public float clickPower = 1f;
    public ParticleSystem particleSystem; //몬스터 클릭 효과

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();

        // OnLeftClick 이벤트 구독
        if (playerController != null)
        {
            playerController.OnLeftClick += OnClick;
        }
    }

    private void OnDestroy()
    {
        // OnLeftClick 이벤트 구독 해제
        if (playerController != null)
        {
            playerController.OnLeftClick -= OnClick;
        }
    }

    public void OnClick()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Mob"))
            {
                MobCharacter mob = hit.collider.GetComponent<MobCharacter>();
                ClickMob(mob);

                PlayParticleSystem(pos);

                SoundManager.Instance.PlayEffect(SoundManager.Instance.soundDB.clickSound, 0.1f, false); 
            }
        }
    }

    void ClickMob(MobCharacter mob)
    {
        if (mob != null && mob.currentLikability < mob.maxLikabliity)
        {
            mob.currentLikability += clickPower;

            if (mob.currentLikability >= mob.maxLikabliity)
            {
                mob.isPropagated = true;

                // 추종자 표시 활성화
                //민간인을 추종자리스트에 추가하는 메서드 실행
                PromptManager.Instance.SetDetectedCivilian(mob.gameObject);
                PromptManager.Instance.Dominate();
            }
        }
    }

    
    void PlayParticleSystem(Vector2 pos)
    {
        particleSystem.transform.position = pos;

        particleSystem.Play();
    }
}