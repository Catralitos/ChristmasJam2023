using Extensions;
using UnityEngine;

namespace SpaceHarrierScene
{
    public class FloorTile : MonoBehaviour
    {
        public LayerMask playerMask;
        
        public void OnTriggerEnter(Collider other)
        {
            if (playerMask.HasLayer(other.gameObject.layer))
            {
                LevelGenerator.Instance.SpawnMoreLevel();
            }
        }
    }
}