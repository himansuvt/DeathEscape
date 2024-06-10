using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    public float maxTime = 30f; // Maximum time in seconds
    private float currentTime; // Current time remaining
    private bool isTimerRunning = false; // Flag to indicate if the timer is running
    public Slider timerSlider; // Reference to the UI slider displaying the timer
    public GameOverManager gameOverManager; // Reference to the GameOverManager for triggering game over sequence

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartTimer();
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerDisplay();

            if (currentTime <= 0f)
            {
                EndTimer();
            }
        }
    }

    private void UpdateTimerDisplay()
    {
        timerSlider.value = currentTime / maxTime;
    }

    public void StartTimer()
    {
        currentTime = maxTime;
        isTimerRunning = true;
        timerSlider.maxValue = 1f;
        timerSlider.value = 1f;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    public void IncreaseTimer(float amount)
    {
        currentTime = Mathf.Min(currentTime + amount, maxTime);
    }

    private void EndTimer()
    {
        StopTimer();
        gameOverManager.PlayGameOverAnimation();
        Invoke("ReloadScene", 2f);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
