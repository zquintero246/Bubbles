using UnityEngine;

public class PersistentCanvas : MonoBehaviour
{
    private static PersistentCanvas instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Mantener el Canvas entre escenas
        }
        else
        {
            Destroy(gameObject);  // Evitar duplicados si ya existe otro Canvas
        }
    }
}