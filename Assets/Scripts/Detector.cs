using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour {
    private HeadController snake;
    private Stats stats;

    void Start()
    {
        snake = GameObject.Find("Head").GetComponent<HeadController>();
        stats = GetComponentInParent<Stats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("World"))
        {
            snake.move = true;
            Debug.Log("Entrou");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("World"))
        {
            snake.move = false;
            snake.transform.Rotate(Vector3.right, 90);
            StartCoroutine(snake.RotateCamera(90, 1));
            Debug.Log("Saiu");
        }
    }
}
