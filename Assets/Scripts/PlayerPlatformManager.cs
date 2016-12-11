using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPlatformManager : MonoBehaviour {

    public Text text;
    public int count;
    

	// Use this for initialization
	void Start () {
        UpdateText();
	}
	
	void UpdateText () {
        text.text = "x " + count;
    }

    public void AddOurson(int number)
    {
        count += number;
        UpdateText();
    }

    public void RemoveOurson(int number)
    {
        count -= number;
        if (count < 0)
            count = 0;
        UpdateText();
    }

    public int GetCount()
    {
        return count;
    }
}
