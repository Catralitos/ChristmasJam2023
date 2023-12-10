using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaceSystem : MonoBehaviour
{
    public bool waypointHit = false;
    public bool not_finished = true;
    public GameObject FinishUI;
    public CarSphereController carController;
    public Timer timer;
    public TextMeshProUGUI finalTimer;

    public GameObject YellowPart;

    public GameObject bluePart1;
    public GameObject bluePart2;
    public GameObject bluePart3;
    public GameObject bluePart4;
    // Start is called before the first frame update
    void Start()
    {
        // Randomize between blue and yellow
        float randomValue = Random.value; // Get a random float value between 0.0 and 1.0

        // Check if randomValue is less than 0.5 (50% chance)
        if (CarLevelManager.Instance.numCarsSpawned == 1)
        {
            MakeYellow();
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "waypoint")
        {
            waypointHit = true;

        }
        if (other.gameObject.tag == "finish" & waypointHit)
        {
            not_finished = false;
            FinishUI.SetActive(true);
            finalTimer.text = timer.timerSeconds.ToString();
            carController.DisableControls();
            timer.StopTimer();
        }

    }

    private void MakeYellow()
    {
        YellowPart.SetActive(true);
        bluePart1.SetActive(false);
        bluePart2.SetActive(false);
        bluePart3.SetActive(false);
        bluePart4.SetActive(false);
    }

    private void MakeBlue()
    {
        YellowPart.SetActive(false);
        bluePart1.SetActive(true);
        bluePart2.SetActive(true);
        bluePart3.SetActive(true);
        bluePart4.SetActive(true);
    }
}
