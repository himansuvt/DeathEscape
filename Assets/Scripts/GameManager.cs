using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool hasKey = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make sure this object persists across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GrabKey()
    {
        hasKey = true;
    }
}
