using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField]
    private int size;
    [SerializeField]
    private int quantity;
    public GameObject food;
    private Vector3 position;

    void Start()
    {
        for (int i = 0; i < quantity; i++)
            spawnFood();
    }

    public void spawnFood ()
    {
        int side = Random.Range(0, 5);
        //Debug.Log(side);
        switch (side)
        {
            case 0:
                position = new Vector3((int)Random.Range(-size + 2, size - 1), size, (int)Random.Range(-size + 2, size - 1));
                break;
            case 1:
                position = new Vector3((int)Random.Range(-size + 2, size - 1), -size, (int)Random.Range(-size + 2, size - 1));
                break;
            case 2:
                position = new Vector3(size, (int)Random.Range(-size + 2, size - 1), (int)Random.Range(-size + 2, size - 1));
                break;
            case 3:
                position = new Vector3(-size, (int)Random.Range(-size + 2, size - 1), (int)Random.Range(-size + 2, size - 1));
                break;
            case 4:
                position = new Vector3((int)Random.Range(-size + 2, size - 1), (int)Random.Range(-size + 2, size - 1), size);
                break;
            case 5:
                position = new Vector3((int)Random.Range(-size + 2, size - 1), (int)Random.Range(-size + 2, size - 1), -size);
                break;
        }

        Instantiate(food, position, Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3((2*size)+1, (2*size)+1, (2*size)+1));
    }
}
