using System;
using UnityEngine;

namespace SpaceHarrierScene.Lama
{
    public class LamaEntity : MonoBehaviour
    {
        #region SingleTon

        /// <summary>
        /// Gets the sole instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static LamaEntity Instance { get; private set; }

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

        [HideInInspector] public Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        #endregion
    }
}