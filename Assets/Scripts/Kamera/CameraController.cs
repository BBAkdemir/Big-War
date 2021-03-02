using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 1000f;
    public float Sensivity = 10f;

    public float sensitivity = 10f;
    public float maxYAngle = 80f;
    private Vector2 currentRotation;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(2))
        {
            currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
            currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
            currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
            currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
            Camera.main.transform.localRotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            Camera.main.transform.Translate(Vector3.forward * speed);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            Camera.main.transform.Translate(-Vector3.forward * speed);
        }

        Camera.main.transform.Translate(Input.GetAxis("Vertical") * Vector3.forward * speed);
        Camera.main.transform.Translate(Input.GetAxis("Horizontal") * Vector3.right * speed);
    }
}
