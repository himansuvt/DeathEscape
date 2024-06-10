using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public Animator gameOverAnimator; // Reference to the Animator component controlling the game over animation

    public void PlayGameOverAnimation()
    {
        gameOverAnimator.SetTrigger("GameOver");
    }
}
