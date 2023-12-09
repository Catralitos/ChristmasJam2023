using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;

    public GameObject CarLevel;
    public GameObject SpaceHarrierLevel;
    // Start is called before the first frame update
    void Start()
    {
        VirtualCamera.LookAt = SpaceHarrierLevel.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            VirtualCamera.LookAt = CarLevel.transform;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            VirtualCamera.LookAt = SpaceHarrierLevel.transform;
        }
    }
}
