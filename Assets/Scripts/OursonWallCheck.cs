using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OursonWallCheck : MonoBehaviour {

    private List<Collider2D> grounds;

    // Use this for initialization
    void Start()
    {

        grounds = new List<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.name.Contains("Player") && !collision.gameObject.name.Contains("Bullet"))
        {
            grounds.Add(collision);
        }
        if (grounds.Count > 0)
            transform.parent.gameObject.SendMessage("WallDetected");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.name.Contains("Player"))
        {
            grounds.Remove(collision);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
