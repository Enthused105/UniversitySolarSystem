using System;
using UnityEngine;
public class ShipController : MonoBehaviour
{

    private Vector3 prevMousePosition;
    private Rigidbody rigidbody;
    private float sensitivity = 1f;
    private float speed = 50f;
    private float speedMult = 4f;

    private ShipCamera camera;

    private void Awake()
    {
        transform.eulerAngles = new Vector3(0,0,0);
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Start()
    {
        camera = CameraModeController.mainCameraModeController.shipCamera;
    }

    void Update()
    {
        if (camera.enabled)
        {
            Vector3 mouseDiff = Input.mousePosition - prevMousePosition;
            mouseDiff = mouseDiff.normalized;
            Vector3 eulerRotation = new Vector3((-mouseDiff.y * sensitivity) + transform.eulerAngles.x,
                (mouseDiff.x * sensitivity) + transform.eulerAngles.y, 0);

            transform.eulerAngles = eulerRotation;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                rigidbody.velocity = transform.forward * speedMult * speed;
            }
            else
            {
                rigidbody.velocity = transform.forward * speed;
            }

            prevMousePosition = Input.mousePosition;
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
        }
    }

}
