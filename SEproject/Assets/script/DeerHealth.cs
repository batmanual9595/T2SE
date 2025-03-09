using System;
using UnityEngine;

public class DeerHealth : MonoBehaviour
{
    public int maxHearts = 3;
    private int currentHearts;

    public event Action<int> OnHealthChanged; 

    void Start()
    {
        currentHearts = maxHearts;
        OnHealthChanged?.Invoke(currentHearts); 
    }

    public void TakeDamage()
    {
        currentHearts--;
        OnHealthChanged?.Invoke(currentHearts);

        if (currentHearts <= 0)
        {
            GameOver();
        }
        else
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        transform.position = new Vector3(0, 1, 0); 
        GetComponent<Rigidbody>().velocity = Vector3.zero; 
    }

    private void GameOver()
    {
        UIManager.Instance.ShowGameOverScreen();
    }

    public void ResetHealth()
    {
        currentHearts = maxHearts;
        OnHealthChanged?.Invoke(currentHearts);
    }
}
