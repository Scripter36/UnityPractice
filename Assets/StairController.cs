using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairController : MonoBehaviour
{
    void OnCollisonEnter(Collision collision) {
        Debug.Log("StairController.OnCollisonEnter");
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.transform.parent = transform;
        }
    }

    void OnCollisonExit(Collision collision) {
        Debug.Log("StairController.OnCollisonExit");
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.transform.parent = null;
        }
    }
}
