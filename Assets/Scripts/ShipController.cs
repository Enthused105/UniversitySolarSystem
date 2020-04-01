using System;
using UnityEngine;
public class ShipController : MonoBehaviour
{

    private Vector3 prevMousePosition;
    private Rigidbody rigidbody;
    private float sensitivity = 1f;
    private float speed = 50f;
    private float speedMult = 6f;
    private Vector2 mouseLook;
    private Vector2 smooth;
    private bool lightSpeed;
    private ShipCamera camera;

    private void Awake()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
        rigidbody = GetComponent<Rigidbody>();
        lightSpeed = false;
    }

    public void Start()
    {
        camera = CameraModeController.mainCameraModeController.shipCamera;
    }

    void Update()
    {
        if (camera.enabled)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Vector2 mouseDiff = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            mouseDiff = mouseDiff.normalized;
            Vector3 eulerRotation = new Vector3((-mouseDiff.y * sensitivity) + transform.eulerAngles.x,
                (mouseDiff.x * sensitivity) + transform.eulerAngles.y, 0);
            transform.eulerAngles = eulerRotation;


            if (Input.GetKey(KeyCode.W))
            {
                if (lightSpeed == false)
                {
                    rigidbody.velocity = transform.forward * speed;

                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        rigidbody.velocity = transform.forward * speed * speedMult;
                    }
                }
            }
            else
            {
                if (lightSpeed == false)
                {
                    Cursor.lockState = CursorLockMode.None;
                }
                
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Vector3 r = GameObject.Find("Mercury").transform.position;
                Vector3 d = GameObject.Find("Mercury").transform.localScale;
                transform.position = r + d + Vector3.up * 50;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Vector3 r = GameObject.Find("Venus").transform.position;
                transform.position = r;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Vector3 r = GameObject.Find("Earth").transform.position;
                transform.position = r;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Vector3 r = GameObject.Find("Mars").transform.position;
                transform.position = r;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Vector3 r = GameObject.Find("Jupiter").transform.position;
                transform.position = r;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                Vector3 r = GameObject.Find("Saturn").transform.position;
                transform.position = r;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                Vector3 r = GameObject.Find("Uranus").transform.position;
                transform.position = r;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                Vector3 r = GameObject.Find("Neptune").transform.position;
                //Quaternion a = GameObject.Find("Neptune").transform.rotation;
                transform.position = r;
                //transform.rotation = a;
                rigidbody.velocity = Vector3.zero;
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                lightSpeed = true;
            }

            else if (Input.GetKeyDown(KeyCode.K))
            {
                lightSpeed = false;
            }

            if (lightSpeed)
            {
                rigidbody.velocity = transform.forward * 1;
            }



            prevMousePosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);

         
        }
    }
}
