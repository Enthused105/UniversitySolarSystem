using System.Globalization;
using UnityEngine;
using UnityEngine.UI;


public class Speedo : MonoBehaviour
{
    private Text text;
    public ShipController ship;
    private float speed;
    private float acceleration;
    private float speedMult;
    private float drag;
    public bool lightSpeed;
    private void Awake()
    {
        text = GetComponent<Text>();
        acceleration = 0.5f;
        speedMult = 8f;
        drag = 0.1f;
    }

    private void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.W))
        {
            if (lightSpeed == false && speed < 300)
            {
                //Debug.Log("Hello");
                speed += acceleration;
                //rigidbody.drag = 1f;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    speed += 1 * acceleration * speedMult;
                    //rigidbody.drag = 0f;
                }
            }
        }
        if (lightSpeed)
        {
            speed = 1;
        }

        speed = speed * (1-drag) * Time.deltaTime;
        text.text = "Current Speed" + speed + "Times the Speed of Light";
    }
}
