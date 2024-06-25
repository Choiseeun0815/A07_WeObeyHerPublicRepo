using Unity.VisualScripting;
using UnityEngine;

public class Spawner: MonoBehaviour
{
    public GameObject prefab;

    private void Start()
    {
        for(int i=0;i<3;i++)
            SpawnObject();
    }

    private Vector3 SetRandomPos()
    {
        float X = Random.Range(-25.5f, 26f);
        float Y = Random.Range(.5f, 18.5f);

        Vector3 randomPos = new Vector3(X, Y);
        return randomPos;
    }

    private void SpawnObject()
    {
        Instantiate(prefab, SetRandomPos(), Quaternion.identity);
    }
}