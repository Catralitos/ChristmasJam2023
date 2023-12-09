using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace SpaceHarrierScene
{
    public class HarrierScoreManager : MonoBehaviour
    {
        #region SingleTon

        /// <summary>
        /// Gets the sole instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static HarrierScoreManager Instance { get; private set; }

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
        
        [SerializeField] private TextMeshProUGUI highScoreText;
        [SerializeField] private TextMeshProUGUI scoreText;

        [SerializeField] private int scorePerFrame;
        
        [HideInInspector] public int highScore;
        [HideInInspector] public int score;

        [HideInInspector] public bool playing;
        
        private void Start()
        {
            playing = true;        
            StartCoroutine(SpawnTimer());
        }

        private void Update()
        {
            score += scorePerFrame;
            if (score > highScore)
            {
                highScore = score;
            }
            
            highScoreText.text = "HIGH SCORE " + highScore;
            scoreText.text = "SCORE " + score;
        }

        private IEnumerator SpawnTimer() {
        
            while (playing) {
                yield return new WaitForSeconds(1);
                score += scorePerFrame;  
            }
        }

        public void IncreaseScore(int points)
        {
            score += points;
        }
    }
}