using UnityEngine;

namespace SpaceHarrierScene
{
    public class Health : MonoBehaviour
    {
        [SerializeField] public int maxHits;
        [SerializeField] public int points;
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
                HarrierScoreManager.Instance.IncreaseScore(points);
                if (explosionPrefab) Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}