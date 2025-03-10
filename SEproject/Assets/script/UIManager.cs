using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Image[] heartIcons;
    public GameObject gameOverScreen;
    public Button respawnButton;
    public Button restartButton;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        FindObjectOfType<DeerHealth>().OnHealthChanged += UpdateHealthUI;
        respawnButton.onClick.AddListener(Respawn);
        restartButton.onClick.AddListener(RestartGame);
        gameOverScreen.SetActive(false);
    }

    public void UpdateHealthUI(int health)
    {
        for (int i = 0; i < heartIcons.Length; i++)
        {
            heartIcons[i].enabled = (i < health);
        }
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }

    private void Respawn()
    {
        DeerHealth deerHealth = FindObjectOfType<DeerHealth>();

        if (deerHealth != null && deerHealth.maxHearts > 0)
        {
            deerHealth.Respawn();
            gameOverScreen.SetActive(false);
        }
    }

    private void RestartGame()
    {
        FindObjectOfType<DeerHealth>().ResetHealth();
        gameOverScreen.SetActive(false);
    }
}
