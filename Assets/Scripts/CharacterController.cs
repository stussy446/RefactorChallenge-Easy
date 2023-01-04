using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LongMethod
{
    public class CharacterController : MonoBehaviour
    {
        [Header("Character Controls")]
        [SerializeField] private float _moveSpeed = 7f;
        [SerializeField] private float _jumpHeight = 10f;

        void Update()
        {
            LockCursor();

            var mov = (Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward).normalized;
            transform.position += mov * Time.deltaTime * _moveSpeed;

            if (Input.GetButtonDown("Jump"))
                GetComponent<Rigidbody>().velocity = Vector3.up * _jumpHeight;



            transform.localRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 45 * Input.GetAxis("Mouse X") * Time.deltaTime, 0);

            var t = transform.Find("Main Camera").transform;
            var xRot = t.localRotation.eulerAngles.x - 45 * Input.GetAxis("Mouse Y") * Time.deltaTime;
            if (xRot > 35) xRot = 35; else if (xRot < 10) xRot = 10;
            t.localRotation = Quaternion.Euler(xRot, 0, 0);
        }

        private static void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
