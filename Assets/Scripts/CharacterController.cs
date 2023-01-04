using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LongMethod
{
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterController : MonoBehaviour
    {
        [Header("Character Controls")]
        [SerializeField] private float _moveSpeed = 7f;
        [SerializeField] private float _jumpHeight = 10f;
        [SerializeField] private float _rotationSpeed = 45f;

        private Rigidbody _rigibody;

        private void Awake()
        {
            _rigibody = GetComponent<Rigidbody>();
            LockCursor();
        }

        void Update()
        {
            Move();
            Jump();
            Rotate();

            var t = transform.Find("Main Camera").transform;
            var xRot = t.localRotation.eulerAngles.x - 45 * Input.GetAxis("Mouse Y") * Time.deltaTime;
            if (xRot > 35) xRot = 35; else if (xRot < 10) xRot = 10;
            t.localRotation = Quaternion.Euler(xRot, 0, 0);
        }

        private void Move()
        {
            Vector3 horizantalMovement = Input.GetAxis("Horizontal") * transform.right;
            Vector3 verticalMovement = Input.GetAxis("Vertical") * transform.forward.normalized;
            Vector3 finalMovement = horizantalMovement + verticalMovement;

            transform.position += finalMovement * Time.deltaTime * _moveSpeed;
        }

        private void Jump()
        {
            if (Input.GetButtonDown("Jump"))
                _rigibody.velocity = Vector3.up * _jumpHeight;
        }

        private void Rotate()
        {
            float xRotation = 0f;
            float yRotation = transform.rotation.eulerAngles.y + _rotationSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
            float zRotation = 0f;

            transform.localRotation = Quaternion.Euler(xRotation, yRotation, zRotation);
        }


        private static void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
