using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceSystem : MonoBehaviour
{
    public bool waypointHit = false;
    public bool not_finished = true;


    // Start is called before the first frame update
    void Start()
    {
        
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
        if (other.gameObject.tag == "finish" && waypointHit)
        {
            not_finished = false;
        }

    }
}
