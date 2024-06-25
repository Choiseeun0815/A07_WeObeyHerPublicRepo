using System.Collections.Generic;
using UnityEngine;

public  class MobCharacterSpawner : MonoBehaviour
{
    // 모브 캐릭터가 생성될 때 캐릭터와 캐릭터 사이에 유지되어야 할 최소 거리
    [SerializeField] float mobSpawnMinDistance = 5f;
    List<Vector3> mobSpawnPos = new List<Vector3>();

    [SerializeField]Transform prefabsSpawnPos;
    public void SpawnMob()
    {
        for (int i = 0; i < GameManager.Instance.pool.pools[(int)TAG.MobCharacter].size; i++)
        {
            Vector3 spawnPos = GetRandomPos();

            while (!isPossiblePos(spawnPos) )
            {
                // 유효한 위치를 찾을 때까지 위치 생성을 반복
                spawnPos = GetRandomPos();
            }

            if (isPossiblePos(spawnPos))
            {
                mobSpawnPos.Add(spawnPos);

                GameObject mobObj = GameManager.Instance.pool.SpawnFromPool("MobCharacter", prefabsSpawnPos);
                mobObj.transform.position = spawnPos;
                mobObj.SetActive(true);
            }
        }
    }

    // 우선은 카메라에 비치는 X의 최대, 최소 값과 Y의 최대 최소 값을 기준으로 생성 
    public Vector3 GetRandomPos()
    {
        float X = Random.Range(-25.5f, 26f);
        float Y = Random.Range(-18.5f, 18.5f);

        Vector3 randomPos = new Vector3(X, Y);
        return randomPos;
    }

    // 모브 캐릭터가 생성될 때 몰려있거나 겹쳐있지 않고,
    // 일정 거리를 두고 생성할 수 있도록 해주는 함수 
    public bool isPossiblePos(Vector3 position)
    {
        
        foreach(var pos in mobSpawnPos)
        {
            // 현재 생성하려는 모브 캐릭터의 위치와
            // 이전에 생성된(리스트에 할당된) 모브 캐릭터의 위치가 최소 거리보다 작다면 false 반환
            if(Vector3.Distance(pos, position)<mobSpawnMinDistance)
            {
                return false;
            }
        }
        return true;
    }
}

