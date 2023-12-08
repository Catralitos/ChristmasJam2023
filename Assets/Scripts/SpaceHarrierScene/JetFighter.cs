using System;
using SpaceHarrierScene.Lama;
using UnityEngine;

namespace SpaceHarrierScene
{
    [RequireComponent(typeof(Rigidbody))]
    public class JetFighter : MonoBehaviour
    {
        [SerializeField] private float distanceFromPlayer;
        [SerializeField] private float forwardFlightSpeed;

        private Rigidbody _rb;
        private Vector3 _jetPosition;
        private bool _reachedPosition;
        
        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Vector3 playerPosition = LamaEntity.Instance.gameObject.transform.position;
            Vector3 jetTransform = transform.position;
            //Set the position to be the same as the players, but clamp so it doesn't go over the limits
            _jetPosition = new Vector3(
                jetTransform.x, jetTransform.y, 
                Mathf.Clamp(jetTransform.z - forwardFlightSpeed * Time.deltaTime, playerPosition.z + distanceFromPlayer, float.PositiveInfinity));
            
            if (!_reachedPosition && MathF.Abs(jetTransform.z - (playerPosition.z + distanceFromPlayer)) <= 0.01f)
            {
                _reachedPosition = true;
            }

            if (_reachedPosition)
            {
                //do code for firing 
            }
        }

        private void FixedUpdate()
        {
            _rb.MovePosition(_jetPosition);
        }
    }
}