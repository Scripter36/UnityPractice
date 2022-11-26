using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float cameraDistance = 3f;
    public float mouseSensitivity = 10f;
    public float wheelSensitivity = 5f;
    public float jumpForce = 5f;

    [SerializeField]
    private Transform cameraTransform;
    [SerializeField]
    private Transform cameraHolderTransform;
    [SerializeField]
    private Rigidbody playerRigidbody;

    private float yaw = 0f;
    private float pitch = 0f;
    private Vector3? lastStairPosition = null;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraDistance -= Input.GetAxis("Mouse ScrollWheel") * wheelSensitivity;
        cameraDistance = Mathf.Clamp(cameraDistance, -10f, 10f);
        if (cameraDistance < -1f) {
            cameraTransform.localEulerAngles = new Vector3(0f, 180f, 0f);
        } else {
            cameraTransform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        float multiplier = Input.GetKey(KeyCode.LeftShift) ? 2f : 1f;

        Vector3 move = playerRigidbody.transform.right * x + playerRigidbody.transform.forward * z;
        playerRigidbody.transform.Translate(move * moveSpeed * multiplier * Time.deltaTime, Space.World);

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        yaw += mouseX * mouseSensitivity;
        pitch -= mouseY * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        cameraTransform.localPosition = new Vector3(0f, 0f, -cameraDistance);
        cameraHolderTransform.eulerAngles = new Vector3(pitch, yaw, 0f);
        playerRigidbody.transform.eulerAngles = new Vector3(0f, yaw, 0f);

        RaycastHit hit;
        if (Physics.Raycast(playerRigidbody.transform.position, Vector3.down, out hit, 1.1f)) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            if (hit.collider.gameObject.tag == "Stair") {
                if (lastStairPosition != null) {
                    Vector3 movement = hit.collider.gameObject.transform.position - (Vector3)lastStairPosition;
                    if (movement.magnitude < 1f) {
                        playerRigidbody.transform.Translate(movement, Space.World);
                    }
                }
                lastStairPosition = hit.collider.gameObject.transform.position;
            }
        } else {
            lastStairPosition = null;
        }
    }
}
