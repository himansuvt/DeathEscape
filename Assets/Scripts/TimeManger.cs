using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;
    public Animator animator;
    public float maxTime = 30f; // Maximum time in seconds
    private float currentTime = 30f; // Current time remaining
    private bool isTimerRunning = false; // Flag to indicate if the timer is running
    public Slider timerSlider; // Reference to the UI slider displaying the timer
    public GameOverManager gameOverManager; // Reference to the GameOverManager for triggering game over sequence
    private float initialDelay = 5f; // Initial delay before starting the timer

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
        if (initialDelay > 0)
        {
            initialDelay -= Time.deltaTime;
            if (initialDelay <= 0)
            {
                StartTimer(); // Start the timer after the initial delay
            }
            return; // Exit the update loop if still in initial delay
        }

        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerDisplay();

            if (currentTime < 5)
            {
                animator.SetBool("IsTimeEnding", true);
            }
            else
            {
                animator.SetBool("IsTimeEnding", false);
            }

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

    public void DecreaseTime(float amount)
    {
        currentTime -= amount;
        if (currentTime < 0)
        {
            currentTime = 0;
        }
        timerSlider.value = currentTime / maxTime;
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
