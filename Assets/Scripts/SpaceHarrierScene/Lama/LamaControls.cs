using System;
using Extensions;
using UnityEngine;

namespace SpaceHarrierScene.Lama
{
    public class LamaControls : MonoBehaviour
    {
        
        [SerializeField] private float horizontalLimit;
        [SerializeField] private float verticalLimit;
        
        [SerializeField] private float forwardFlightSpeed;
        [SerializeField] private float sidewaysFlightSpeed;

        [SerializeField] private float fireCooldown;
        private float _fireCooldownLeft;
        [SerializeField] private GameObject bulletPrefab;
            
        private float _horizontalInput, _verticalInput;

        public LayerMask obstacles;

        private Rigidbody _rb;
        private Health _health;
        private Vector3 _playerPos;
        private bool _moving = true;
              
        private void Start()
        {
            _health = GetComponent<Health>();
            _rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log(other.gameObject);
            if (obstacles.HasLayer(other.gameObject.layer))
            {
                Destroy(other.gameObject);
                 //TODO spawn explosão
                _health.TakeDamage();
            }
        }

        // Update is called once per frame
        private void Update()
        {
            _horizontalInput = Input.GetAxis("Horizontal");

            _verticalInput = Input.GetAxis("Vertical");

            if (_moving)
            {
                Vector3 position = transform.position;
                _playerPos = new Vector3(
                    Mathf.Clamp(position.x + _horizontalInput * sidewaysFlightSpeed * Time.deltaTime, -horizontalLimit, horizontalLimit), 
                    Mathf.Clamp(position.y + _verticalInput * sidewaysFlightSpeed * Time.deltaTime, -verticalLimit, verticalLimit),
                    position.z + forwardFlightSpeed * Time.deltaTime);

                _fireCooldownLeft -= Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.Space) && _fireCooldownLeft <= 0.0f)
                {
                    Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    _fireCooldownLeft = fireCooldown;
                }
            }
        }

        private void FixedUpdate()
        {
            _rb.MovePosition(_playerPos);
        }
    }
}
