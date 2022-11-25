using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Transform LeftDoor;
    [SerializeField]
    private Transform RightDoor;
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float distance = 1f;

    private Vector3 leftClosedPosition;
    private Vector3 rightClosedPosition;

    private bool isOpen = false;
    private float status = 0; // 0: completely closed, 1: completely open
    void Start()
    {
        leftClosedPosition = LeftDoor.localPosition;
        rightClosedPosition = RightDoor.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen && !Mathf.Approximately(status, 1f)) {
            status = Mathf.Clamp01(status + speed * Time.deltaTime);
            LeftDoor.localPosition = new Vector3(-status * distance, 0f, 0f) + leftClosedPosition;
            RightDoor.localPosition = new Vector3(status * distance, 0f, 0f) + rightClosedPosition;
        } else if (!isOpen && !Mathf.Approximately(status, 0f)) {
            status = Mathf.Clamp01(status - speed * Time.deltaTime);
            LeftDoor.localPosition = new Vector3(-status * distance, 0f, 0f) + leftClosedPosition;
            RightDoor.localPosition = new Vector3(status * distance, 0f, 0f) + rightClosedPosition;
        }
    }

    public void openDoor() { isOpen = true; }

    public void closeDoor() { isOpen = false; }
}
