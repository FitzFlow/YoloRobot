using UnityEngine;
using TMPro;

public class CurrentLevelDisplay : MonoBehaviour
{
    public LevelManager levelManager;
    public TextMeshProUGUI levelText;

    private void Update()
    {
        if (!levelManager || !levelText)
            return;

        levelText.text = $"Niveau {levelManager.GetCurrentLevel()}";
    }
}
