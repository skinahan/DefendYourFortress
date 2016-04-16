using UnityEngine;
using System.Collections;

public class ObjectSpawnerScript : MonoBehaviour {

    public GameObject objectToSpawn;
    public GameObject secondObjectToSpawn;
    public bool increaseDifficulty;
    public int spawnLimit = 20;
    private int objectsSpawned = 0;
    private float yOffset = 0.25f;
    private Quaternion rot;
    private int reSetDelay = 400;
    public int spawnDelay = 400;

	// Use this for initialization
	void Start () {
        rot = transform.rotation;
        reSetDelay = spawnDelay;
	}

    void spawn()
    {
        if (objectToSpawn == null && secondObjectToSpawn == null)
        {
            return;
        }
        else
        {
            if (spawnDelay <= 0 && (objectsSpawned < spawnLimit))
            {
                int offSet = Random.Range(-1, 2);
                yOffset = offSet * yOffset;
                Vector2 newPos = this.transform.position;
                newPos.y = newPos.y + yOffset;
                GameObject.Instantiate(objectToSpawn, newPos, rot);
                objectsSpawned++;
                if(secondObjectToSpawn != null)
                {
                    GameObject.Instantiate(secondObjectToSpawn, newPos, rot);
                    objectsSpawned++;
                }
                if(increaseDifficulty)
                {
                    reSetDelay--;
                }
                spawnDelay = reSetDelay;
            }
            else
            {
                spawnDelay--;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        spawn();	
	}
}
