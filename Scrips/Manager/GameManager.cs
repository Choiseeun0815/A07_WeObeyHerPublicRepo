using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : Singleton<GameManager>
{
    public ObjectPool pool;
    public MobCharacterSpawner mobSpawner;

    List<MobCharacter> mobCharacters = new List<MobCharacter>(); // 민간인 리스트
    public List<SpecialCharacter> specialCharacters = new List<SpecialCharacter>();

    public Material grayscaleMaterial;

    private bool isGameOver = false;

    protected override void Awake()
    {
        base.Awake();
        pool = GetComponent<ObjectPool>();
        mobSpawner = GetComponent<MobCharacterSpawner>();
    }

    private void Start()
    {
        mobSpawner.SpawnMob();
        UpdateFollowerNumber();
    }

    // 오브젝트 풀에서 생성된 민간인을 추가하는 메서드
    public void AddMobCharacter(MobCharacter mobCharacter)
    {
        mobCharacters.Add(mobCharacter);
    }

    // 민간인이 허기 정도를 체크하는 메서드를 Update문에서 실행
    private void Update()
    {
        if (!isGameOver)
        {
            foreach (var mobCharacter in mobCharacters)
            {
                mobCharacter.CheckHunger();
            }
        }
    }

    public void GameOver(string endingReason)
    {
        if (!isGameOver)
        {
            isGameOver = true;
            EndGame(endingReason);
            SoundManager.Instance.StopAllSounds();
        }
    }

    private void EndGame(string endingReason)
    {
        // 엔딩 내용을 설정
        StatManager.Instance.EndingReason = endingReason;

        // 게임 내 모든 오브젝트 정지
        StopAllObjects();

        // 사운드 정지
        SoundManager.Instance.StopAllSounds();

        // 팔로워 수 업데이트
        UpdateFollowerNumber();

        // UI 업데이트
        UIManager.Instance.ShowPopupUI<UI_GameOver>("UI_GameOver");
    }

    private void StopAllObjects()
    {
        // 모든 MobCharacter의 동작 정지
        var allMobCharacters = FindObjectsOfType<MobCharacter>();
        foreach (var mob in allMobCharacters)
        {
            mob.enabled = false;
        }

        // 필요한 경우 추가적인 오브젝트 정지 로직을 여기에 추가
        var allMovers = FindObjectsOfType<MobCharacterMoveController>();
        foreach (var mover in allMovers)
        {
            mover.enabled = false;
        }

        // Player와 관련된 동작 정지
        var player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.enabled = false;
        }

        // Time.timeScale을 0으로 설정하여 게임 전체를 멈춤
        Time.timeScale = 0f;
    }

    public void RetryGame()
    {
        // 게임 재시작 로직
        Time.timeScale = 1f; // 게임 속도 재설정

        // 필요한 초기화 작업 수행
        isGameOver = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        PromptManager.Instance.FollowerList.Clear();
        StatManager.Instance.followerCount = 0;
        StatManager.Instance.coinAmount = 100; //기본적으로 지급되는 돈
        StatManager.Instance.UpdateFollowerText();
    }

    public void UpdateFollowerNumber()
    {
        //StatManager.Instance.followerCount = mobCharacters.Count(mob => mob.isPropagated);
        StatManager.Instance.followerCount = StatManager.Instance.followerCount;
    }
}
