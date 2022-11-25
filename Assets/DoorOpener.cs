using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    [SerializeField]
    private DoorController doorController;
    [SerializeField]
    private float closeTime = 3000f;

    private Timer timer = new Timer();
    // Start is called before the first frame update
    void Start()
    {
        timer.Elapsed += (sender, e) => {
            doorController.closeDoor();
        };
        timer.AutoReset = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            timer.Stop();
            doorController.openDoor();
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            timer.Interval = closeTime;
            timer.Start();
        }
    }
}
