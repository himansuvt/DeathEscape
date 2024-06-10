using UnityEngine;

public class TimeBooster : MonoBehaviour
{
    public float timeBoostAmount = 30f; // Amount of time to boost the timer by

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            TimerManager.Instance.IncreaseTimer(timeBoostAmount); // Increase timer by the specified amount
            Destroy(transform.parent.parent.gameObject); // Destroy the TimeBooster object
        }
    }
}
