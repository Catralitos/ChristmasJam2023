using Extensions;
using UnityEngine;

namespace SpaceHarrierScene
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private bool isPlayer;
        [SerializeField] private float travelSpeed;
        [SerializeField] private float lifeTime;
        [SerializeField] private LayerMask enemyAndDestructablesLayer;
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private LayerMask obstacles;
        [SerializeField] private GameObject explosionPrefab;
        
        private Rigidbody _rb;
        private float _timeLeft;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _timeLeft = lifeTime;

            if (isPlayer)
            {
                _rb.AddForce(Vector3.forward * travelSpeed, ForceMode.Impulse);
            }
            else
            {
                _rb.AddForce(Vector3.back * travelSpeed, ForceMode.Impulse);
            }
        }

        private void Update()
        {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft < 0) Destroy(gameObject);
        }
        
        public void OnTriggerEnter(Collider other)
        {
            if ((enemyAndDestructablesLayer.HasLayer(other.gameObject.layer) && isPlayer) 
                || (playerLayer.HasLayer(other.gameObject.layer) && !isPlayer))
            {
                other.gameObject.GetComponent<Health>().TakeDamage();
                Destroy(gameObject);
            } else if (obstacles.HasLayer(other.gameObject.layer))
            {
                if (explosionPrefab) Instantiate(explosionPrefab, other.gameObject.transform.position + Vector3.back * 0.5f,
                    Quaternion.identity);
                Destroy(gameObject);
            }
        }
        
    }
}