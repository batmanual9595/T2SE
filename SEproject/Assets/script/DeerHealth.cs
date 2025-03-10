using System;
using UnityEngine;

public class DeerHealth : MonoBehaviour
{
    public int maxHearts = 3;
    private int currentHearts;

    public event Action<int> OnHealthChanged;

    private DeerStateMachine stateMachine;

    void Start()
    {
        currentHearts = maxHearts;
        OnHealthChanged?.Invoke(currentHearts);
        stateMachine = GetComponent<DeerStateMachine>();
    }

    public void TakeDamage()
    {
        currentHearts--;
        OnHealthChanged?.Invoke(currentHearts);

        if (currentHearts <= 0)
        {
            maxHearts--;

            if (maxHearts <= 0)
            {
                GameOver();
            }
            else
            {
                Respawn();
            }
        }
    }

    public void FallOffEdge()
    {
        maxHearts = 0; // Instantly lose all hearts
        currentHearts = 0;
        OnHealthChanged?.Invoke(currentHearts);
        GameOver();
    }

    public void Respawn()
    {
        if (maxHearts > 0)
        {
            currentHearts = maxHearts;
            OnHealthChanged?.Invoke(currentHearts);

            // Reset position to prevent falling off or weird ragdoll effects
            transform.position = new Vector3(167, 50, 400);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero; // Stop spinning

            // Reset ragdoll to normal animation state
            RagdollController ragdoll = GetComponent<RagdollController>();
            if (ragdoll != null)
            {
                ragdoll.SetRagdoll(false); // Disable ragdoll
            }

            stateMachine.EnableDeer(); // Re-enable movement
        }
    }
    private void GameOver()
    {
        UIManager.Instance.ShowGameOverScreen();
        stateMachine.DisableDeer();
    }

    public void ResetHealth()
    {
        maxHearts = 3;
        currentHearts = maxHearts;
        OnHealthChanged?.Invoke(currentHearts);
        stateMachine.EnableDeer();
    }
}
