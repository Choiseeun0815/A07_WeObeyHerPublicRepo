using UnityEngine;

public class SoundDB : MonoBehaviour
{
    [Header("Bgm Clip")] // 반복 재생한다
    public AudioClip mainSceneClip;
    public AudioClip eventClip1;
    public AudioClip eventClip2;

    [Header("System Clip")] // 시스템 소리, 한번만 재생한다
    public AudioClip clickSound; // DM-CGS-21
    public AudioClip cancelSound; // DM-CGS-03
    public AudioClip closeSound; // DM-CGS-22
    public AudioClip PopupSound; // DM-CGS-16
    public AudioClip UpgradeSound; // DM-CGS-18

    [Header("Effect Clip")] // 효과음, 한번만 재생한다
    public AudioClip useMoneySound; // DM-CGS-20
    public AudioClip fullLikabilSound; // DM-CGS-08
    public AudioClip enemyAttackSound; // DM-CGS-47

    [Header("Object Clip")] // 효과음, 반복 재생한다
    public AudioClip walkingSound; // DM-CGS-40
    public AudioClip tickingSound; // DM-CGS-04
    public AudioClip coundDownSound; // DM-CGS-05
    public AudioClip AlramSound; // DM-CGS-07

    [Header("Selection Clip")] // 선택지의 정답, 실패에 대한 효과음. 한 번만 재생
    public AudioClip correctSound;
    public AudioClip failSound;

    [Header("Propagated Clip")] // 포교될 때 재생
    public AudioClip propagatSound; // 일반 신도 사운드
    public AudioClip obeyABY; 
    public AudioClip obeyKYR; 
    public AudioClip obeyCSE; 
    public AudioClip obeyYSN; 
    public AudioClip obeyLYS; 
}