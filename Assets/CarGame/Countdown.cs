using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public GameObject lapTimer;

    private TextMeshProUGUI countdownText;
    
    private bool _started;

    private void Start()
    {
        countdownText = GetComponent<TextMeshProUGUI>();
        countdownText.text = "";
    }

    // Start is called before the first frame update
    private void Update()
    {
        if (!_started && CarLevelManager.Instance.allCarsSpawned)
        {
            _started = true;
            StartCoroutine(CountDownRoutine());
        }
    }

   IEnumerator CountDownRoutine()
    {
        yield return new WaitForSeconds(0.5f);

        countdownText.text = "3";
        yield return new WaitForSeconds(1f);
        countdownText.text = "2";
        yield return new WaitForSeconds(1f);
        countdownText.text = "1";
        yield return new WaitForSeconds(1f);
        countdownText.text = "GO!";
        CarLevelManager.Instance.countdownEnded = true;
        yield return new WaitForSeconds(1f);
        countdownText.text = "";

        //GameObject.FindObjectsOfType<CarController>();


    }
}

