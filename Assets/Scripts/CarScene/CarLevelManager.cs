using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLevelManager : MonoBehaviour
{

    #region SingleTon

    /// <summary>
    /// Gets the sole instance.
    /// </summary>
    /// <value>
    /// The instance.
    /// </value>
    public static CarLevelManager Instance { get; private set; }

    /// <summary>
    /// Awakes this instance (if none already exists).
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
    
    [SerializeField] private bool isSinglePlayer = true;
    
     public int numCarsSpawned;
     public bool allCarsSpawned;
     public bool countdownEnded;

    private void Update()
    {
        if (allCarsSpawned) return;
        
        switch (isSinglePlayer)
        {
            case true when numCarsSpawned == 1:
            case false when numCarsSpawned == 2:
                allCarsSpawned = true;
                break;
        }
    }
}
