using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float tempsRestant; 

    void Update()
    {
        if (tempsRestant > 0){
            tempsRestant -= Time.deltaTime;
        }
        else if (tempsRestant < 0){
            tempsRestant = 0;
        }
        int minutes = Mathf.FloorToInt(tempsRestant / 60);
        int secondes = Mathf.FloorToInt(tempsRestant % 60);
        timerText.text = string.Format("{0:00}:{1:00}",minutes,secondes);
    }
}
