using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscalatorController : MonoBehaviour
{
    [SerializeField]
    GameObject stairPrefab;
    GameObject[] stairs = new GameObject[10];
    float status = 0f; // 0: first stair is on bottom, 1: first stair is on top
    [SerializeField]
    float speed = 1f;

    float yMove = 0.5f;
    float zMove = 1.5f;

    void Start()
    {
        for (int i = 0; i < 10; i++) {
            GameObject stair = Instantiate(stairPrefab, transform.position + new Vector3(0f, yMove * i, zMove * i), Quaternion.identity);
            stair.transform.parent = transform;
            stairs[i] = stair;
        }
        Destroy(stairPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator moveEscalator() {
        while (true) {
            yield return null;
            status = status + Time.deltaTime * speed;
            if (status > 1) status = status - 1;
            for (int i = 0; i < 10; i++) {
                stairs[i].transform.position = transform.position + new Vector3(0f, (yMove * i + yMove * 10f * status) % (yMove * 10f), (zMove * i + zMove * 10f * status) % (zMove * 10f));
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            StartCoroutine(moveEscalator());
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            StopAllCoroutines();
        }
    }
}
