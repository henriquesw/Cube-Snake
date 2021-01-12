using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        GameObject.Find("Spawner").GetComponent<Spawner>().spawnFood();
    }
}
