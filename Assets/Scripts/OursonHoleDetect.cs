using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OursonHoleDetect : MonoBehaviour {

    private List<Collider2D> grounds;

    // Use this for initialization
    void Start()
    {

        grounds = new List<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Ground"))
        {
            grounds.Add(collision);
        }
        if (grounds.Count > 0)
            transform.parent.gameObject.SendMessage("SetHole", false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Ground"))
        {
            grounds.Remove(collision);
        }
        if (grounds.Count == 0)
            transform.parent.gameObject.SendMessage("SetHole", true);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
