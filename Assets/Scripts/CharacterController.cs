using System;
using UnityEngine;

namespace LongMethod
{
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterController : MonoBehaviour
    {
        [Header("Character Movement Configs")]
        [SerializeField] private float _moveSpeed = 7f;
        [SerializeField] private float _jumpHeight = 10f;

        [Header("Rotation Configs")]
        [SerializeField] private float _rotationSpeed = 45f;
        [SerializeField] private float _minRotation = 10f;
        [SerializeField] private float _maxRotation = 35f;

        private Rigidbody _rigibody;

        private void Awake()
        {
            _rigibody = GetComponent<Rigidbody>();
            LockCursor();
        }

        void Update()
        {
            MovePlayer();
            RotatePlayer();
            Jump();

            RotatePlayerCamera();
        }

        private void MovePlayer()
        {
            Vector3 horizantalMovement = Input.GetAxis("Horizontal") * transform.right;
            Vector3 verticalMovement = Input.GetAxis("Vertical") * transform.forward.normalized;

            Vector3 combinedMovement = horizantalMovement + verticalMovement;

            transform.position += combinedMovement * Time.deltaTime * _moveSpeed;
        }
        private void RotatePlayer()
        {
            float xRotation = 0f;
            float yRotation = transform.rotation.eulerAngles.y + _rotationSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
            float zRotation = 0f;

            transform.localRotation = Quaternion.Euler(xRotation, yRotation, zRotation);
        }

        private void Jump()
        {
            if (Input.GetButtonDown("Jump"))
                _rigibody.velocity = Vector3.up * _jumpHeight;
        }

        private void RotatePlayerCamera()
        {
            Transform cameraTransform = Camera.main.transform;
            float yRotation = 0f;
            float zRotation = 0f;

            float rawXRotation = cameraTransform.localRotation.eulerAngles.x - _rotationSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime;
            float formattedXRotation = Math.Clamp(rawXRotation, _minRotation, _maxRotation);

            cameraTransform.localRotation = Quaternion.Euler(formattedXRotation, yRotation, zRotation);
        }
        
        private static void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
