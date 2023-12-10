using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    static float timer;

    public int timerSeconds;

    private bool timerEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timerEnabled & CarLevelManager.Instance.countdownEnded)
        {
            timer += Time.deltaTime;
        }
       

        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);

        string time = string.Format("{0:0}:{1:00}", minutes, seconds);

        timeText.text = time;
        timerSeconds = seconds + (minutes*60);
    }

    public void StopTimer()
    {
        timerEnabled = false;
        
    }
}
