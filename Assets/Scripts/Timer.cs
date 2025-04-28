using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float tempsInitial = 30f; // 30 secondes
    private float tempsRestant;
    public TextMeshProUGUI timerText;

    public delegate void TimerEnded();
    public event TimerEnded OnTimerEnd;

    private void Start()
    {
        tempsRestant = tempsInitial;
    }

    private void Update()
    {
        if (!timerText)
            return;

        if (tempsRestant > 0)
        {
            tempsRestant -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            tempsRestant = 0;
            UpdateTimerDisplay();

            // 🔥 Déclenche l'événement une seule fois
            if (OnTimerEnd != null)
            {
                OnTimerEnd.Invoke();
                OnTimerEnd = null; // Pour éviter des appels multiples
            }
        }
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(tempsRestant / 60);
        int secondes = Mathf.FloorToInt(tempsRestant % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, secondes);
    }

    public void ResetTimer()
    {
        tempsRestant = tempsInitial;
    }
}
