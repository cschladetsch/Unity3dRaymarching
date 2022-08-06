using UnityEngine;

namespace App {
    public class CameraMovement : MonoBehaviour {
        public Camera Camera;

        private Global Global;

        void Update() {
            Global = FindObjectOfType<Global>();
            MoveCamera();
            RotateCamera();
        }

        private void RotateCamera() {
            var origin = Camera.transform.eulerAngles;
            var destination = origin;

            if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetMouseButton(1)) {
                destination.x -= Input.GetAxis("Mouse Y") * Global.RotateAmount;
                destination.y += Input.GetAxis("Mouse X") * Global.RotateAmount;
            }

            if (destination != origin) {
                Camera.transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime * Global.RotateSpeed);
            }
        }

        private void MoveCamera() {
            var movement = new Vector3(0, 0, 0);

            if (Input.GetKey("a")) {
                movement.x -= Global.ScrollSpeed;
            }
            if (Input.GetKey("s")) {
                movement.z -= Global.ScrollSpeed;
            }
            if (Input.GetKey("d")) {
                movement.x += Global.ScrollSpeed;
            }
            if (Input.GetKey("w")) {
                movement.z += Global.ScrollSpeed;
            }

            movement = Camera.transform.TransformDirection(movement);
            movement.y = 0;
            movement.y -= Global.ScrollSpeed * Input.GetAxis("Mouse ScrollWheel");

            var origin = Camera.transform.position;
            var destination = origin;

            destination.x += movement.x;
            destination.y += movement.y;
            destination.z += movement.z;

            if (destination != origin) {
                Camera.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * Global.ScrollSpeed);
            }
        }
    }
}
