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
        this.rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 movement = new Vector3();
        movement.x = Input.GetAxis(this.horizontalAxis);
        movement.z = Input.GetAxis(this.verticalAxis);
        this.transform.Translate(-movement * Time.deltaTime * this.movementSpeed);

        if (Input.GetKeyDown(this.jumpKey) && this.grounded)
        {
            this.rb.AddForce(this.transform.up * this.jumpForce, ForceMode.Impulse);
        }

        float mouseX = Input.GetAxis(this.mouseX);
        if (mouseX > this.mouseThreashold || mouseX < -this.mouseThreashold)
        {
            this.transform.Rotate(transform.up, mouseX * Time.deltaTime * this.xRotateSpeed);
        }
        
        float mouseY = Input.GetAxis(this.mouseY);
        if (mouseY > this.mouseThreashold || mouseY < -this.mouseThreashold)
        {
            this.cam.RotateAround(this.head.position, -this.cam.right, mouseY * Time.deltaTime * this.yRotateSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.root.gameObject != this.transform.root.gameObject)
        {
            this.grounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.gameObject != this.transform.root.gameObject)
        {
            this.grounded = false;
        }
    }
}
