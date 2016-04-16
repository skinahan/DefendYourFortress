using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {
    public int killCount = 0;
    public int levelNumber = 1;
    GameObject killCounter;
	// Use this for initialization
	void Start () {
        killCounter = GameObject.FindGameObjectWithTag("KillCtr");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        killCounter.GetComponent<UnityEngine.UI.Text>().text = "Kills: " + killCount;
    }
}
