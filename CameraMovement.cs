using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using RTS;
 using System;
 
 public class UserInput : MonoBehaviour {
     private Player player;
 
     // Use this for initialization
     void Start () {
         player = transform.root.GetComponent<Player>();
     }
     
     // Update is called once per frame
     void Update () {
 
 
         if (player.human)
         {
             MoveCamera();
             RotateCamera();
         }
 
 
 
 
     }
 
     private void RotateCamera()
     {
         Vector3 origin = Camera.main.transform.eulerAngles;
         Vector3 destination = origin;
 
         //detect rotation amount if ALT is being held and the Right mouse button is down
         if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetMouseButton(1))
         {
             destination.x -= Input.GetAxis("Mouse Y") * ResourceManager.RotateAmount;
             destination.y += Input.GetAxis("Mouse X") * ResourceManager.RotateAmount;
         }
 
         //if a change in position is detected perform the necessary update
         if (destination != origin)
         {
             Camera.main.transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.RotateSpeed);
         }
     }
 
     private void MoveCamera()
     {
         float xpos = Input.mousePosition.x;
         float ypos = Input.mousePosition.y;
         Vector3 movement = new Vector3(0, 0, 0);
 
         //Move the GameObject
         if (Input.GetKey("a"))
         {
             movement.x -= ResourceManager.ScrollSpeed;
         }
         if (Input.GetKey("s"))
         {
             movement.z -= ResourceManager.ScrollSpeed;
 
         }
         if (Input.GetKey("d"))
         {
             movement.x += ResourceManager.ScrollSpeed;
         }
         if (Input.GetKey("w"))
         {
 
             movement.z += ResourceManager.ScrollSpeed;
         }
 
         //horizontal camera movement
         if (xpos >= 0 && xpos < ResourceManager.ScrollWidth)
         {
             movement.x -= ResourceManager.ScrollSpeed;
         }
         else if (xpos <= Screen.width && xpos > Screen.width - ResourceManager.ScrollWidth)
         {
             movement.x += ResourceManager.ScrollSpeed;
         }
 
         //vertical camera movement
         if (ypos >= 0 && ypos < ResourceManager.ScrollWidth)
         {
             movement.z -= ResourceManager.ScrollSpeed;
         }
         else if (ypos <= Screen.height && ypos > Screen.height - ResourceManager.ScrollWidth)
         {
             movement.z += ResourceManager.ScrollSpeed;
         }
 
         movement = Camera.main.transform.TransformDirection(movement);
         movement.y = 0;
         //away from ground movement
         movement.y -= ResourceManager.ScrollSpeed * Input.GetAxis("Mouse ScrollWheel");
 
         //calculate desired camera position based on received input
         Vector3 origin = Camera.main.transform.position;
         Vector3 destination = origin;
         destination.x += movement.x;
         destination.y += movement.y;
         destination.z += movement.z;
 
         if (destination.y > ResourceManager.MaxCameraHeight)
         {
             destination.y = ResourceManager.MaxCameraHeight;
         }
         else if (destination.y < ResourceManager.MinCameraHeight)
         {
             destination.y = ResourceManager.MinCameraHeight;
         }
 
         //if a change in position is detected perform the necessary update
         if (destination != origin)
         {
             Camera.main.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.ScrollSpeed);
         }
     }
 } 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 