using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Rigidbody LeftDoorHinge;
    [SerializeField]
    private Rigidbody RightDoorHinge;
    [SerializeField]
    private float openTime = 1f;
    private float openAngle = 90f;

    private Vector3 leftClosedRotation;
    private Vector3 rightClosedRotation;
    private Vector3 leftOpenedRotation;
    private Vector3 rightOpenedRotation;
    private float status = 0; // 0: completely closed, 1: completely open

    private bool nextOpen = true;
    void Start()
    {
        leftClosedRotation = LeftDoorHinge.rotation.eulerAngles;
        rightClosedRotation = LeftDoorHinge.rotation.eulerAngles;

        leftOpenedRotation = LeftDoorHinge.rotation.eulerAngles + new Vector3(0f, openAngle, 0f);
        rightOpenedRotation = RightDoorHinge.rotation.eulerAngles + new Vector3(0f, -openAngle, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (nextOpen) OpenDoor();
            else CloseDoor();
            nextOpen = !nextOpen;
        }
    }

    private void updateDoorAngle() {
        LeftDoorHinge.MoveRotation(Quaternion.Euler(Vector3.Slerp(leftClosedRotation, leftOpenedRotation, status)));
        RightDoorHinge.MoveRotation(Quaternion.Euler(Vector3.Slerp(rightClosedRotation, rightOpenedRotation, status)));
    }

    private IEnumerator OpenDoorCoroutine() {
        while (status < 1) {
            updateDoorAngle();
            status = Mathf.Clamp01(status + Time.deltaTime / openTime);
            yield return null;
        }
        updateDoorAngle();
    }

    private IEnumerator CloseDoorCoroutine() {
        while (status > 0) {
            updateDoorAngle();
            status = Mathf.Clamp01(status - Time.deltaTime / openTime);
            yield return null;
        }
        updateDoorAngle();
    }

    public void OpenDoor() {
        StopAllCoroutines();
        StartCoroutine(OpenDoorCoroutine());
    }

    public void CloseDoor() {
        StopAllCoroutines();
        StartCoroutine(CloseDoorCoroutine());
    }
}
