using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    [Header("Player settings")]
    private Rigidbody rb;
    [SerializeField]
    private float speed;
    private Animator animator;
    private PlayerInput playerInput;
    private Vector2 moveInput;
    [Header("CAMERA PLAYER")]
    [SerializeField]
    private Transform fatherCam;
    [SerializeField]
    private float sesitiveCam;
    private Vector2 camInput;
    [SerializeField]
    private float minRotVertical;
   
    [SerializeField]
    private float maxRotVertical;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = playerInput.actions["move"].ReadValue<Vector2>();
        camInput = playerInput.actions["CanRotation"].ReadValue<Vector2>();

        
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void LateUpdate()
    {
        CanMove();
    }

    private void Move()
    {
        rb.velocity =transform.TransformDirection( new Vector3(moveInput.x, rb.velocity.y, moveInput.y) * speed);
        animator.SetFloat("horizontal", moveInput.x);
        animator.SetFloat("vertical", moveInput.y);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, fatherCam.eulerAngles.y, transform.eulerAngles.z);
    }
    private void CanMove()
    {
        fatherCam.position = transform.position+new Vector3(0, -0.445f, 0);
         fatherCam.eulerAngles += new Vector3(camInput.y, camInput.x, 0) * sesitiveCam*Time.deltaTime;
        // fatherCam.Rotate(camInput.y, camInput.x, 0 * sesitiveCam * Time.deltaTime,Space.Self);
        if (fatherCam.eulerAngles.x < 360 + minRotVertical&& fatherCam.eulerAngles.x>180) 
        {
            fatherCam.eulerAngles = new Vector3(minRotVertical, fatherCam.eulerAngles.y, fatherCam.eulerAngles.z);

        }
        else if (fatherCam.eulerAngles.x>maxRotVertical&&fatherCam.eulerAngles.x<180)
        {
            fatherCam.eulerAngles = new Vector3(maxRotVertical, fatherCam.eulerAngles.y, fatherCam.eulerAngles.z);
        }
    }
}
