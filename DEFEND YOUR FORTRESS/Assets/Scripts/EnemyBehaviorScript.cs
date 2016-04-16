using UnityEngine;
using System;
using System.Collections;

public class EnemyBehaviorScript : MonoBehaviour {
    public bool pickedUp = false;
    private float deathThreshold = 9f;
    private int movementSpeed = 5;
    private Rigidbody2D rb;
    public int attackDelay = 45;
    GameObject fortress;
    GameObject gameManager;
    GameObject[] groundLayers;
    private bool attacking = false;
    private bool grounded = false;
    int layer = 1;
    private Animator enemyAnim;

    // Use this for initialization
    void Start() {
        fortress = GameObject.FindGameObjectWithTag("Fortress");
        gameManager = GameObject.Find("GameManager");
        groundLayers = GameObject.FindGameObjectsWithTag("Ground");
        rb = this.GetComponent<Rigidbody2D>();
        enemyAnim = this.GetComponent<Animator>();
        setLayer();
    }

    void setLayer()
    {
        this.gameObject.layer = UnityEngine.Random.Range(8, 10);
    }
	
	// Update is called once per frame
	void Update () {
        //groundCheck();
        if (!attacking && grounded && !pickedUp)
        {
            walk();
        }
        if(attacking)
        {
            attack();
        }
        if(pickedUp)
        {
            linkTransform();        
        }
	}

    void linkTransform()
    {
        var v3 = Input.mousePosition;
        v3.z = 0;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        this.gameObject.GetComponent<Transform>().position = new Vector2(v3.x, v3.y);
    }

    void release()
    {
        pickedUp = false;
        grounded = false;
    }

    void OnMouseDown()
    {
        if (!pickedUp)
        {
            pickedUp = true;
            enemyAnim.SetBool("pickedUpAnim", true);
            enemyAnim.SetBool("landedSafe", false);
            grounded = false;
            attacking = false;
            Debug.Log("Enemy picked up!");
        }
    }

    void OnMouseUp()
    {
        pickedUp = false;
        enemyAnim.SetBool("pickedUpAnim", false);
        float xForce = UnityEngine.Random.Range(-5f, 2f);
        rb.AddForce(new Vector2(xForce, 10f), ForceMode2D.Impulse);
    }

    void walk()
    {
        if (grounded)
        {

            Vector2 walkSpeed = new Vector2(movementSpeed, 1);
            if (Math.Abs(rb.velocity.x) < 2)
            {
                walkSpeed.x = movementSpeed;
            } else
            {
                walkSpeed.x = 0;
            }
            rb.AddForce(walkSpeed);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("Collision detected.");
        GameObject other = coll.gameObject;
        switch(other.tag)
        {
            case "Fortress":
                if (!grounded)
                    checkSpeed();
                else
                    attacking = true;
                break;
            case "Ground":
                checkSpeed();
                break;
            default:
                break;
        }
    }

    void attack()
    {
        if(attackDelay <= 0)
        {
            fortress.GetComponent<FortressDamageScript>().takeDamage();
            attackDelay = 45;
        } else {
            attackDelay--;
        }
    }

    void die()
    {
        gameManager.GetComponent<GameManagerScript>().killCount++;
        enemyAnim.SetBool("died", true);
        StartCoroutine(WaitAndDie(0.52F));
    }

    IEnumerator WaitAndDie(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
    }

    void land()
    {
        grounded = true;
        enemyAnim.SetBool("landedSafe", true);
        walk();
    }

    void checkSpeed()
    {
        Debug.Log("Checking speed.");
        Debug.Log(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.magnitude));
        if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.magnitude) >= deathThreshold)
        {
            die();
        } else
        {
            land();
        }
    }

    void groundCheck()
    {
        if (this.gameObject.layer == 8)
        {
            if (this.transform.position.y <= -3.5)
            {
                grounded = true;
            }
            else
            {
                grounded = false;
            }
        } else if (this.gameObject.layer == 9)
        {
            if (this.transform.position.y <= -1.5)
            {
                grounded = true;
            }
            else
            {
                grounded = false;
            }
        }
    }
}
