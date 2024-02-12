using UnityEngine;

[DisallowMultipleComponent]
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _Instance;
    public static T Instance => _Instance;

    public static bool Initialized => _Instance != null;

    public T Init()
    {
        if (_Instance != null)
        {
            if (this != _Instance)
            {
                Destroy(gameObject);
            }
            return _Instance;
        }

        _Instance = GetComponent<T>();

        return _Instance;
    }

    protected void Awake()
    {
        Init();
    }
}
