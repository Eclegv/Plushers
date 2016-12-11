using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public int damages;
    public float speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Detect"))
            return ;
        if (collision.gameObject.name.Contains("Enemy"))
        {
            collision.gameObject.SendMessage("TakeDamages", damages);
            Destroy(transform.gameObject);
            return ;
        }
        else if (!collision.gameObject.name.Contains("Player"))
        {
            Destroy(transform.gameObject);
            return ;
        }
    }
    
    void Start () {
        GetComponent<Rigidbody2D>().AddForce(transform.up * speed);
	}
	
	void Update () {
		
	}
}
