using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public GameObject CountDown;
    public GameObject lapTimer;




    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDownRoutine());
    }

   IEnumerator CountDownRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        CountDown.GetComponent<TextMeshProUGUI>().text = "3";

        CountDown.SetActive(true);

        yield return new WaitForSeconds(1f);
        CountDown.SetActive(false);
        CountDown.GetComponent<TextMeshProUGUI>().text = "2";
        CountDown.SetActive(true);
        yield return new WaitForSeconds(1f);
        CountDown.SetActive(false);
        CountDown.GetComponent<TextMeshProUGUI>().text = "1";
        CountDown.SetActive(true);
        yield return new WaitForSeconds(1f);
        CountDown.SetActive(false);
        CountDown.GetComponent<TextMeshProUGUI>().text = "GO!";
        CountDown.SetActive(true);

        //GameObject.FindObjectsOfType<CarController>();


    }

    public void StartCountdown()
    {
        StartCoroutine(CountDownRoutine());
    }
}
