using UnityEngine;

public class GhostAI : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 10f;
    public float attackRadius = 2f;
    public float moveSpeed = 2f;
    public int health = 4; // Number of hits the ghost can take
    public float attackCooldown = 1.5f; // Time between attacks
    public float attackDamageTimeReduction = 3f; // Time to reduce from the timer on attack
    public AudioClip attackSound; // Attack sound effect
    //public AudioClip BreathingHigh;
    public Animator canvasAnimator; // Animator for the canvas

    private AudioSource audioSource; // AudioSource component to play the sounds
    // Change the access modifier to public
    public bool isPlayerInRange = false; // Now accessible from outside

    private bool isAlive = true;
    private float lastAttackTime;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (!isAlive) return;

        if (player != null)
        {
            // Calculate the direction from the ghost to the player
            Vector3 directionToPlayer = player.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            lookRotation.eulerAngles = new Vector3(0, lookRotation.eulerAngles.y, 0); // Lock the rotation to the Y axis
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (isPlayerInRange)
            {
                if (distanceToPlayer > detectionRadius)
                {
                    isPlayerInRange = false;
                }
                else if (distanceToPlayer <= attackRadius)
                {
                    if (Time.time >= lastAttackTime + attackCooldown)
                    {
                        AttackPlayer();
                        lastAttackTime = Time.time;
                    }
                }
                else
                {
                    FollowPlayer();
                }
            }
        }
    }

    private void FollowPlayer()
    {
        transform.position = Vector3.Lerp(transform.position, player.position, moveSpeed * Time.deltaTime);
        //if (BreathingHigh != null)
        //{
        //    audioSource.PlayOneShot(BreathingHigh);
        //}
    }

    private void AttackPlayer()
    {
        // Play attack sound
        if (attackSound != null)
        {
            audioSource.PlayOneShot(attackSound);
        }

        // Trigger the attack animation
        canvasAnimator.SetBool("IsAttacking", true);

        // Decrease timer
        TimerManager.Instance.DecreaseTime(attackDamageTimeReduction);

        // Stop attack animation after a delay
        Invoke("StopAttackAnimation", 0.2f); // Adjust this duration based on your animation length
    }

    private void StopAttackAnimation()
    {
        canvasAnimator.SetBool("IsAttacking", false);
    }

    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isAlive = false;
        // Add death effects here
        Destroy(gameObject); // Optionally destroy the ghost after a delay
    }

    // No change needed here, as it's already public
    public void SetPlayerInRange(bool inRange)
    {
        isPlayerInRange = inRange;
    }
}
