using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name, typeof(T));
                    instance = obj.GetComponent<T>();
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        // 이미 인스턴스가 있는 경우, 이전 인스턴스를 파괴합니다.
        if (instance != null && instance != this)
        {
            Destroy(instance.gameObject);
        }

        // 현재 인스턴스를 할당하고 씬 전환 시에도 유지되도록 설정합니다.
        instance = this as T;
        DontDestroyOnLoad(gameObject);
    }
}
