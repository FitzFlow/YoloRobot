using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject level1Detector;
    public GameObject level2Detector;

    public Timer timerScript;
    public Transform playerStartPosition; // Nouvelle référence au point de respawn


    private int currentLevel = 1;

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    private void Start()
    {
        ActivateLevel(1);

        if (timerScript != null)
        {
            timerScript.OnTimerEnd += HandleTimerEnded;
        }
    }

    private void HandleTimerEnded()
    {
        Debug.Log("Temps écoulé, réinitialisation du niveau...");

        // 🔥 Reset Timer
        timerScript.ResetTimer();

        // 🔥 Reset Position Joueur
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

    public void ActivateLevel(int level)
    {
        currentLevel = level;
        Debug.Log($"Niveau actuel'{currentLevel}'");

        // Activer/désactiver les détecteurs en fonction du niveau
        if (level == 1)
        {
            level1Detector.SetActive(true);
            level2Detector.SetActive(false);
        }
        else if (level == 2)
        {
            level1Detector.SetActive(false);
            level2Detector.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Niveau non reconnu : " + level);
        }
    }

    public void NextLevel()
    {
        ActivateLevel(currentLevel + 1);
        Debug.Log($"Niveau actuel'{currentLevel}'");
    }
}
