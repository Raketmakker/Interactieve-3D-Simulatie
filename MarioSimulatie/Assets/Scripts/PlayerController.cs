using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool grounded;
    private Rigidbody rb;
    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";
    public float movementSpeed;
    public string mouseX = "Mouse X";
    public float mouseThreashold = 0.05f;
    public float xRotateSpeed = 300;
    public string mouseY = "Mouse Y";
    public float yRotateSpeed = 300;
    public Transform cam;
    public Transform head;
    public float jumpForce;
    public KeyCode jumpKey;

    private void Awake()
    {
        //Assign rigidbody component
        this.rb = GetComponent<Rigidbody>();
        //Lock cursor to game window
        Cursor.lockState = CursorLockMode.Confined;
        //Hide cursor
        Cursor.visible = false;
    }

    void Update()
    {
        //Assign user input to movement vector
        Vector3 movement = new Vector3();
        movement.x = Input.GetAxis(this.horizontalAxis);
        movement.z = Input.GetAxis(this.verticalAxis);
        //Move the player (transform) with movementspeed. Multiply by deltatime for framerate independence
        this.transform.Translate(-movement * Time.deltaTime * this.movementSpeed);

        //Check if the jumpkey is pressed and the player is on the ground
        if (Input.GetKeyDown(this.jumpKey) && this.grounded)
        {
            //Add (jump) force
            this.rb.AddForce(this.transform.up * this.jumpForce, ForceMode.Impulse);
        }

        //Get mouse movement (X axis) and check if it is moved more than a threashold
        float mouseX = Input.GetAxis(this.mouseX);
        if (mouseX > this.mouseThreashold || mouseX < -this.mouseThreashold)
        {
            //Rotate player around local axis
            this.transform.Rotate(transform.up, mouseX * Time.deltaTime * this.xRotateSpeed);
        }

        //Get mouse movement (Y axis) and check if it is moved more than a threashold
        float mouseY = Input.GetAxis(this.mouseY);
        if (mouseY > this.mouseThreashold || mouseY < -this.mouseThreashold)
        {
            //Rotate the camera around the head. Keeping it a the same distance
            this.cam.RotateAround(this.head.position, -this.cam.right, mouseY * Time.deltaTime * this.yRotateSpeed);
        }
    }

    //Callback for ground landing
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.root.gameObject != this.transform.root.gameObject)
        {
            this.grounded = true;
        }
    }

    //Callback for getting airborn
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.gameObject != this.transform.root.gameObject)
        {
            this.grounded = false;
        }
    }
}
