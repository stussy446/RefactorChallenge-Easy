using System;
using UnityEngine;

namespace LongMethod
{
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterController : MonoBehaviour
    {
        [Header("Player Movement Configs")]
        [SerializeField] private float _moveSpeed = 7f;
        [SerializeField] private float _jumpHeight = 10f;
        [SerializeField] private float _playerRotationSpeed = 45f;
      
        [Header("Camera Rotation Configs")]
        [SerializeField] private float _cameraRotationSpeed = 45f;
        [SerializeField] private float _minRotation = 10f;
        [SerializeField] private float _maxRotation = 35f;

        private Rigidbody _rigibody;
        private Transform _cameraTransform;

        private void Awake()
        {
            _rigibody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _cameraTransform = Camera.main.transform;
            LockCursor();
        }

        void Update()
        {
            MovePlayer();
            RotatePlayer();
            Jump();

            RotatePlayerCamera();
        }

        /// <summary>
        /// Handles player movement by manipulating the player's transform position with the horizontal and vertical
        /// axes values and a movement speed
        /// </summary>
        private void MovePlayer()
        {
            Vector3 horizantalMovement = Input.GetAxis("Horizontal") * transform.right;
            Vector3 verticalMovement = Input.GetAxis("Vertical") * transform.forward.normalized;
            Vector3 combinedMovement = horizantalMovement + verticalMovement;

            transform.position += combinedMovement * Time.deltaTime * _moveSpeed;
        }

        /// <summary>
        /// Handles player rotation based on the mouse x axis and a rotation speed
        /// </summary>
        private void RotatePlayer()
        {
            float xRotation = 0f;
            float yRotation = transform.rotation.eulerAngles.y + _playerRotationSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
            float zRotation = 0f;

            transform.localRotation = Quaternion.Euler(xRotation, yRotation, zRotation);
        }

        /// <summary>
        /// Handles player jumping based on a jump button press, player character jumps up to a height based on the jump Height provided
        /// </summary>
        private void Jump()
        {
            if (Input.GetButtonDown("Jump"))
                _rigibody.velocity = Vector3.up * _jumpHeight;
        }

        /// <summary>
        /// Rotates the camera within a minimum and maximum set range based on the y mouse axis and a camera rotation speed
        /// </summary>
        private void RotatePlayerCamera()
        {
            float yRotation = 0f;
            float zRotation = 0f;

            float rawXRotation = _cameraTransform.localRotation.eulerAngles.x - _cameraRotationSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime;
            float formattedXRotation = Math.Clamp(rawXRotation, _minRotation, _maxRotation);

            _cameraTransform.localRotation = Quaternion.Euler(formattedXRotation, yRotation, zRotation);
        }
        
        /// <summary>
        /// locks the computer's cursor
        /// </summary>
        private static void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
