using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] prefabs;
    Vector3 spawnPos = new Vector3(35, 0, 0);
    float spawnRate = 2.0f;
    float spawn;
    PlayerController control;

    // Start is called before the first frame update
    void Start()
    {
        spawn = spawnRate;
        control = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (control.gameOver == false && control.intro == false)
        {
            int random = Random.Range(0, 3);
            spawn -= Time.deltaTime;
            spawnRate -= Time.deltaTime * 0.0133333333333333333333f;
            if (spawn <= 0.0f)
            {
                Instantiate(prefabs[random], spawnPos, prefabs[random].transform.rotation);
                spawn = spawnRate;
            }
        }
    }
}
