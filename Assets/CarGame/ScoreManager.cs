using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI inputScore;
    [SerializeField]
    private TMP_InputField inputName;
    public Timer timer;
    public UnityEvent<string, int> submitScoreEvent;


    public void SubmitScore()
    {
        submitScoreEvent.Invoke(inputName.text, int.Parse(timer.timerSeconds.ToString()));
    }
}
