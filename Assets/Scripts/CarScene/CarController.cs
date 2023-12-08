using UnityEngine;

//From https://github.com/PrismYoutube/Unity-Car-Controller/blob/main/Code

namespace CarScene
{
    public class CarController : MonoBehaviour
    {
        private float _horizontalInput, _verticalInput;
        private float _currentSteerAngle, _currentBreakForce;
        private bool _isBreaking;

        // Settings
        [SerializeField] private float motorForce, breakForce, maxSteerAngle;

        // Wheel Colliders
        [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
        [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

        // Wheels
        [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
        [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

        private void FixedUpdate() {
            GetInput();
            HandleMotor();
            HandleSteering();
            UpdateWheels();
        }

        private void GetInput() {
            // Steering Input
            _horizontalInput = Input.GetAxis("Horizontal");

            // Acceleration Input
            _verticalInput = Input.GetKey(KeyCode.LeftShift) ? 1.0f : 0.0f;

            // Breaking Input
            _isBreaking = Input.GetKey(KeyCode.Space);
        }

        private void HandleMotor() {
            frontLeftWheelCollider.motorTorque = _verticalInput * motorForce;
            frontRightWheelCollider.motorTorque = _verticalInput * motorForce;
            _currentBreakForce = _isBreaking ? breakForce : 0f;
            ApplyBreaking();
        }

        private void ApplyBreaking() {
            frontRightWheelCollider.brakeTorque = _currentBreakForce;
            frontLeftWheelCollider.brakeTorque = _currentBreakForce;
            rearLeftWheelCollider.brakeTorque = _currentBreakForce;
            rearRightWheelCollider.brakeTorque = _currentBreakForce;
        }

        private void HandleSteering() {
            _currentSteerAngle = maxSteerAngle * _horizontalInput;
            frontLeftWheelCollider.steerAngle = _currentSteerAngle;
            frontRightWheelCollider.steerAngle = _currentSteerAngle;
        }

        private void UpdateWheels() {
            UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
            UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
            UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
            UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        }

        private static void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform) {
            wheelCollider.GetWorldPose(out var pos, out var rot);
            wheelTransform.rotation = rot;
            wheelTransform.position = pos;
        }
    }
}