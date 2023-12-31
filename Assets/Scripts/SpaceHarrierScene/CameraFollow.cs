using SpaceHarrierScene.Lama;
using UnityEngine;

namespace SpaceHarrierScene
{
    /// <summary>
    /// A script to make the camera follow the player while staying inside the field
    /// </summary>
    public class CameraFollow : MonoBehaviour
    {
        /// <summary>
        /// The horizontal limit from the center of the field
        /// </summary>
        public float horizontalLimit;
        /// <summary>
        /// The vertical limit from the center of the field
        /// </summary>
        public float verticalLimit;

        public float distanceFromPlayer;
        
        /// <summary>
        /// Updates this instance.
        /// </summary>
        private void Update()
        {
            if (LamaEntity.Instance == null) return;
            Vector3 playerPosition = LamaEntity.Instance.gameObject.transform.position;
            Transform cameraTransform = transform;
            //Set the position to be the same as the player«s, but clamp so it doesn't go over the limits
            cameraTransform.position = new Vector3(
                Mathf.Clamp(playerPosition.x, -horizontalLimit, horizontalLimit),
                Mathf.Clamp(playerPosition.y, -verticalLimit, verticalLimit),
                playerPosition.z - distanceFromPlayer);
        }
    }
}