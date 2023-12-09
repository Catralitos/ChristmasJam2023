using UnityEngine;

namespace SpaceHarrierScene
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private GameObject livesHolder;
        [SerializeField] private bool isPlayer;
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
            if (isPlayer) Destroy(livesHolder.gameObject.transform.GetChild(livesHolder.gameObject.transform.childCount-1).gameObject);
            if (_hitsLeft < 1)
            {
                if (!isPlayer) HarrierScoreManager.Instance.IncreaseScore(points);
                if (explosionPrefab) Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}