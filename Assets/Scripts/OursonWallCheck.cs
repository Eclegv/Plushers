using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OursonWallCheck : MonoBehaviour {

    private List<Collider2D> walls;

    // Use this for initialization
    void Start()
    {
        walls = new List<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.name.Contains("Player") && !collision.gameObject.name.Contains("Bullet") && !collision.gameObject.name.Contains("Detect"))
        {
            walls.Add(collision);
        }
        if (walls.Count > 0)
        {
            transform.parent.gameObject.SendMessage("SetWall", true);
            b = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.name.Contains("Player") && !collision.gameObject.name.Contains("Bullet") && !collision.gameObject.name.Contains("Detect"))
        {
            walls.Remove(collision);
        }
        if (walls.Count == 0)
            transform.parent.gameObject.SendMessage("SetWall", false);
    }

    bool b = false;
    // Update is called once per frame
    void Update () {
        List<Collider2D> a = null;
		for (int i = 0; i < walls.Count; i++)
        {
            if (walls[i] == null)
            {
                if (a == null)
                    a = new List<Collider2D>();
                a.Add(walls[i]);
            }
        }
        if (a != null)
        {
            for (int i = 0; i < a.Count; i++)
            {
                walls.Remove(a[i]);
            }
        }
        if (walls.Count == 0 && b)
        {
            transform.parent.gameObject.SendMessage("SetWall", false);
            b = false;
        }
    }
}
