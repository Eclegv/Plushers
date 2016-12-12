using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeManager : MonoBehaviour {

    public int maxlife;
    public GameObject heartPrefab;
    public Sprite heartfull;
    public Sprite hearthalf;
    public Sprite heartempty;

    public int life;

    private List<Image> hearts;

	// Use this for initialization
	void Start () {
        hearts = new List<Image>();
        life = maxlife;
        Transform canvas = GameObject.Find("Canvas").transform;
        for (int i = 0; i < maxlife / 2; i++)
        {
            GameObject img = Instantiate(heartPrefab, canvas);
            RectTransform r = img.GetComponent<RectTransform>();
            r.anchoredPosition = new Vector3(35 + i * 65, -35, 0);
            r.localScale = new Vector3(1, 1, 1);
            hearts.Add(img.GetComponent<Image>());
        }
        UpdateHearts();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if ((i + 1) * 2 <= life)
                hearts[i].sprite = heartfull;
            else if (i * 2 + 1 == life)
                hearts[i].sprite = hearthalf;
            else
                hearts[i].sprite = heartempty;
        }
    }

    public void TakeDamages(int damages)
    {
        life -= damages;
        if (life < 0)
            life = 0;
        UpdateHearts();
    }
}
