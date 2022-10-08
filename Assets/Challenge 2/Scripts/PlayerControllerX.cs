using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;

    public float spawnInterval = 3.0f;
    private float spawnTime;

    // Update is called once per frame
    void Update()
    {
        spawnTime -= Time.deltaTime;
        spawnTime = Mathf.Max(0, spawnTime);

        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && spawnTime == 0.0f)
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            spawnTime = spawnInterval;
        }
    }
}
