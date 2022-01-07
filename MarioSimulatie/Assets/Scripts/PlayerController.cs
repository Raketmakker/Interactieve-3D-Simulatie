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
    public float rotateSpeed = 1;
    public float jumpForce;
    public KeyCode jumpKey;

    private void Awake()
    {
        this.rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    // Update is called once per frame
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

        float mouse = Input.GetAxis(this.mouseX);
        if (mouse > this.mouseThreashold || mouse < -this.mouseThreashold)
        {
            this.transform.Rotate(transform.up, mouse * Time.deltaTime * this.rotateSpeed);
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
