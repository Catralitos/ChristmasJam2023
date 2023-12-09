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
        private bool _addedJoint;
        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log("Collided with " + other.gameObject);
        }

        private void Update()
        {
            Vector3 playerPosition = LamaEntity.Instance.gameObject.transform.position;
            Vector3 jetTransform = transform.position;
            if (!_reachedPosition && 
                (MathF.Abs(jetTransform.z - (playerPosition.z + distanceFromPlayer)) <= 1f || jetTransform.z < playerPosition.z))
            {
                _reachedPosition = true;
            }

            if (!_reachedPosition)
            {
                _jetPosition = new Vector3(
                    jetTransform.x, jetTransform.y,
                    jetTransform.z - forwardFlightSpeed * Time.deltaTime);
                
                Debug.Log("transform.position 1 " + _jetPosition);
            }
            else
            {
                if (!_addedJoint)
                {
                    FixedJoint fj = gameObject.AddComponent<FixedJoint>();
                    fj.connectedBody = LamaEntity.Instance.rb;
                    fj.enableCollision = true;
                    _addedJoint = true;
                    _rb.position = new Vector3(_rb.position.x, _rb.position.y, playerPosition.z + distanceFromPlayer);
                }
                
                //do code for firing
            }
        }

        private void FixedUpdate()
        {
           if (!_reachedPosition) _rb.MovePosition(_jetPosition);
        }
    }
}