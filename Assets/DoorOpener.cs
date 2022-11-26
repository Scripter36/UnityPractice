using System.Collections;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    [SerializeField]
    private DoorController doorController;
    [SerializeField]
    private float closeTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            StopAllCoroutines();
            doorController.OpenDoor();
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            StopAllCoroutines();
            StartCoroutine(CloseDoor());
        }
    }

    IEnumerator CloseDoor() {
        yield return new WaitForSeconds(closeTime);
        doorController.CloseDoor();
    }
}
