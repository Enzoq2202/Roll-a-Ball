using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    private Vector3 offset;
    public float rotationSpeed = 5.0f;
    private float mouseX;

    void Start()
    {
        offset = transform.position - player.transform.position;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        Quaternion camTurnAngle = Quaternion.AngleAxis(mouseX, Vector3.up);
        Vector3 newPos = camTurnAngle * offset;
        transform.position = player.transform.position + newPos;
        transform.LookAt(player.transform);
    }
}
