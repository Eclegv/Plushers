using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OursonPlayerDetect : MonoBehaviour {

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            transform.parent.gameObject.SendMessage("SetTarget", collision.gameObject.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            transform.parent.gameObject.SendMessage("CancelTarget");
        }
    }
}
