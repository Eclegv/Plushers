using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SpawnerScript : MonoBehaviour {

    public List<GameObject> entitiesToSpawn;
    public float timeBetweenEachSpawn;

    private bool active = false;
    private bool finished = false;
    private int spawnCount = 0;
    private float timeLeft;
    private Transform enemiesObject;

	// Use this for initialization
	void Start () {
        enemiesObject = GameObject.Find("Enemies").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (active && !finished)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
            }
            else
            {
                timeLeft = timeBetweenEachSpawn;
                Instantiate(entitiesToSpawn[spawnCount], transform.position, Quaternion.identity, enemiesObject);
                if (++spawnCount >= entitiesToSpawn.Count)
                    finished = true;
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            active = true;
        }
    }
}
