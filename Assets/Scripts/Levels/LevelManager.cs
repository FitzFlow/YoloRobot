using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public GameObject level1Detector;
    public GameObject level2Detector;
    public Timer timerScript;
    public Transform playerStartPosition;
    public TextMeshProUGUI levelCompleteText;

    private int currentLevel = 1;
    private AudioSource audioSource;
    public AudioClip levelCompleteSound;

    private void Start()
    {
        ActivateLevel(1);

        audioSource = GetComponent<AudioSource>();

        if (timerScript != null)
        {
            timerScript.OnTimerEnd += HandleTimerEnded;
        }

        if (levelCompleteText != null)
        {
            levelCompleteText.gameObject.SetActive(false); // 🔥 On cache le texte au départ
        }
    }

    public void ActivateLevel(int level)
    {
        currentLevel = level;

        if (level1Detector != null)
            level1Detector.SetActive(currentLevel == 1);

        if (level2Detector != null)
            level2Detector.SetActive(currentLevel == 2);

        Debug.Log($"Niveau actuel '{currentLevel}'");
    }

    public void NextLevel()
    {
        PlayLevelCompleteSound();

        if (levelCompleteText != null)
        {
            levelCompleteText.gameObject.SetActive(true);
            levelCompleteText.text = $"Niveau {currentLevel} terminé ! 🎉";
        }

        ActivateLevel(currentLevel + 1);
    }

    private void PlayLevelCompleteSound()
    {
        if (audioSource != null && levelCompleteSound != null)
        {
            audioSource.PlayOneShot(levelCompleteSound);
            Debug.Log("Playing Level Complete Sound");
        }
        else
        {
            Debug.LogWarning("AudioSource ou LevelCompleteSound est manquant !");
        }
    }

    private void HandleTimerEnded()
    {
        Debug.Log("Temps écoulé, réinitialisation du niveau...");

        // 🔥 Reset Timer
        if (timerScript != null)
        {
            timerScript.ResetTimer();
        }

        // 🔥 Reset Joueur
        ResetPlayerPosition();

        // 🔥 Reset Niveau
        RestartCurrentLevel();
    }

    public void RestartCurrentLevel()
    {
        ActivateLevel(currentLevel);
    }

    private void ResetPlayerPosition()
    {
        if (playerStartPosition != null)
        {
            Camera.main.transform.position = playerStartPosition.position;
            Camera.main.transform.rotation = playerStartPosition.rotation;
        }
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}
