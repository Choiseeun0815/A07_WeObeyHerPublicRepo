using UnityEngine;
using System.Collections;

public class MobCharacterMoveController : MonoBehaviour
{
    public MobCharacter mobCharacter;
    Vector3 targetPos;

    private void Awake()
    {
        mobCharacter = GetComponent<MobCharacter>();    
    }
    private void Start()
    {
        targetPos = Vector3.zero;
        StartCoroutine(MoveInterval());
    }
    private void Update()
    {
        if (mobCharacter.isPropagated)
        {
            StopAllCoroutines();
        }
    }
    public IEnumerator MoveInterval()
    {
        float moveTime = Random.Range(3, 6);
        while (true)
        {
            RandomTargetPos();
            while (transform.position != targetPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, 3f * Time.deltaTime);
                UpdateAnimation();
                yield return null;
            }

            mobCharacter.StopAnimation(mobCharacter.AnimationData.WalkParameterHash);
            mobCharacter.StartAnimation(mobCharacter.AnimationData.IdleParameterHash);
            yield return new WaitForSeconds(moveTime);
        }
    }
    void RandomTargetPos()
    {
        float[] randomMove = { 0, 1f, -1f };

        float X = randomMove[Random.Range(0, 3)];
        float Y = randomMove[Random.Range(0, 3)];

        float moveX = transform.position.x + X;
        float moveY = transform.position.y + Y;

        moveX = Mathf.Clamp(moveX, -25.5f, 26f);
        moveY = Mathf.Clamp(moveY, -18.5f, 18.5f);

        targetPos = new Vector3(moveX, moveY);
    }

    private void UpdateAnimation()
    {
        mobCharacter.StopAnimation(mobCharacter.AnimationData.IdleParameterHash);
        float dirX = targetPos.x - transform.position.x;
        float dirY = targetPos.y - transform.position.y;

        mobCharacter.StartAnimation(mobCharacter.AnimationData.WalkParameterHash);
        mobCharacter.SetAnimation(mobCharacter.AnimationData.DirXParameterHash, mobCharacter.AnimationData.DirYParameterHash, dirX, dirY);
    }
}
