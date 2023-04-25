using UnityEngine;

namespace Logic.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _following;
        [SerializeField] float _rotationAngleX;
        [SerializeField] float distance;
        [SerializeField] private float OffsetY;

        private void LateUpdate()
        {
            if (_following == null)
                return;
            var rotation = Quaternion.Euler(_rotationAngleX, 0, 0);
            var position = rotation * new Vector3(0, 0, -distance) + FollowingPointPosition();
            transform.rotation = rotation;
            transform.position = position;
        }

        public void Follow(GameObject following) => _following = following.transform;

        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = _following.position;
            followingPosition.y += OffsetY;
            return followingPosition;
        }
    }
}