using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FortressDamageScript : MonoBehaviour {
    private int health = 100;
    private Slider healthSlider;

	// Use this for initialization
	void Start () {
        healthSlider = GameObject.Find("HealthBar").GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
        healthSlider.value = health;
    }

    public void takeDamage()
    {
        Debug.Log("Fortress taking damage!");
        Debug.Log("Fortress health:" + health);
        health--;
        if(health <= 0)
        {
            Destroy(this.gameObject);
            Application.Quit();
        }
    }
}
