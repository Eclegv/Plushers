using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OursonAttackFx : MonoBehaviour {

    public int damages;

    private float time;

    private List<Collider2D> cols;

    private void Start()
    {
        cols = new List<Collider2D>();
    }
    
    private void Update()
    {
        time += Time.deltaTime;
        if (time > 0.6 && time < 0.7)
        {
            for (int i = 0; i < cols.Count; i++)
                cols[i].gameObject.SendMessage("TakeDamages", damages);
        }
        if (time >= 0.8)
            Destroy(transform.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            cols.Add(collision);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            cols.Remove(collision);
        }
    }
}
