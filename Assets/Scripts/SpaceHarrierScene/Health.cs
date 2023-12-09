using System;
using UnityEngine;

namespace SpaceHarrierScene
{
    public class Health : MonoBehaviour
    {
        [SerializeField] public int maxHits;
        private int _hitsLeft;

        [SerializeField] private GameObject explosionPrefab;
        
        private void Start()
        {
            _hitsLeft = maxHits;
        }

        public void TakeDamage()
        {
            _hitsLeft--;
            if (_hitsLeft < 1)
            {
                if (explosionPrefab) Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}