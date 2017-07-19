using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlinkerController : MonoBehaviour {

    public GameObject bulletPrefabNormal;
    public GameObject homingMissilePrefab;
    public GameObject laserPrefab;
    public Transform bulletSpawn;
    public Transform RightSpawn;
    public Transform FarRightSpawn;
    public Transform LeftSpawn;
    public Transform FarLeftSpawn;
    public int bulletSpeed;
    bool Allowfire = true;
    public float startHealth;
    public float currentHealth;
    public Slider healthBar;
    private float timeToWait;

    int cycle = 1;

	// Use this for initialization
	void Start () {

        currentHealth = startHealth;
        healthBar.maxValue = startHealth;

	}
	
	// Update is called once per frame
	void Update () {

        if (Allowfire)
        {
            Allowfire = false;

            if (cycle == 1)
            {
                defaultShot();

                timeToWait = 5;
            }

            if(cycle == 2)
            {
                StartCoroutine(burst());

                timeToWait = 5;
            }

            if (cycle == 3)
            {
                missile();

                timeToWait = 4;
            }

            if (cycle == 4)
            {
                StartCoroutine(burstmissile());

                timeToWait = 5;
            }

            if (cycle == 5)
            {
                StartCoroutine(burstmissile());
                StartCoroutine(burst());

                timeToWait = 3;
            }

            if (cycle == 6)
            {
                StartCoroutine(burstmissile());

                timeToWait = 2.5f;
            }

            if (cycle == 7)
            {
                laser();
                timeToWait = 6;
            }

            if (cycle == 8)
            {
                laser();
                timeToWait = 4;
            }

            if (cycle == 9)
            {
                var randomNum = Mathf.Floor(Random.value);
                if (randomNum == 0)
                {
                    laser();
                    timeToWait = 4;
                }

                if (randomNum == 1)
                {
                    StartCoroutine(burstmissile());
                    timeToWait = 2;
                }
            }

            StartCoroutine(waitSome());
        }


        if(currentHealth <= healthBar.maxValue/10)
        {
            cycle++;
            startHealth = startHealth * 1.4f;
            currentHealth = startHealth;
            healthBar.maxValue = startHealth;
            Debug.Log("boss cycle " + cycle);

        }

	}

    void OnTriggerEnter2D()
    {
        GetComponent<Animator>().Play("Damaged");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 8 || col.gameObject.layer == 11)
        {
            Debug.Log("bullet detected!");
            Destroy(col.gameObject);

            if (col.gameObject.name == "Bullet(Clone)")
            {
                currentHealth = currentHealth - 4;
                Debug.Log("normalbullet hit");
            }

            if (col.gameObject.name == "PowerShot(Clone)")
            {
                currentHealth = currentHealth - 10;
            }

            if (col.gameObject.name == "BulletBarrier(Clone)")
            {
                currentHealth = currentHealth - 30;
            }

        }

        healthBar.value = currentHealth;
        Debug.Log(healthBar.value);
    }


    IEnumerator waitSome()
    {
        yield return new WaitForSeconds(timeToWait);
        Allowfire = true;
    }

    IEnumerator burst()
    {
        for (int i = 0; i < 3; i++)
        {
            defaultShot();
            yield return new WaitForSeconds(0.4f);
        }
    }

    IEnumerator burstmissile()
    {
        for (int i = 0; i < 4; i++)
        {
            missile();
            yield return new WaitForSeconds(0.7f);
        }
    }

    void defaultShot()
    {
        var bullet = (GameObject)Instantiate(
        bulletPrefabNormal,
        bulletSpawn.position,
        bulletSpawn.rotation);

        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * -bulletSpeed;

        Destroy(bullet, 2.2f);
    }

    void missile()
    {
        var bullet = (GameObject)Instantiate(
        homingMissilePrefab,
        bulletSpawn.position,
        bulletSpawn.rotation);

        bullet.GetComponent<HomingMissile>().target = GameObject.Find("Player").transform;
    }

    void laser()
    {

        var whichSpawn = Mathf.Floor(Random.value * 4);

        if (whichSpawn == 1)
        {
            var bullet = (GameObject)Instantiate(laserPrefab,FarLeftSpawn.position,FarLeftSpawn.rotation);

            bullet.GetComponent<LaserDrone>().target = GameObject.Find("Player").transform;
        }

        if (whichSpawn == 2)
        {
            var bullet = (GameObject)Instantiate(laserPrefab, LeftSpawn.position, LeftSpawn.rotation);

            bullet.GetComponent<LaserDrone>().target = GameObject.Find("Player").transform;
        }

        if (whichSpawn == 3)
        {
            var bullet = (GameObject)Instantiate(laserPrefab, RightSpawn.position, RightSpawn.rotation);

            bullet.GetComponent<LaserDrone>().target = GameObject.Find("Player").transform;
        }

        if (whichSpawn == 4)
        {
            var bullet = (GameObject)Instantiate(laserPrefab, FarRightSpawn.position, FarRightSpawn.rotation);

            bullet.GetComponent<LaserDrone>().target = GameObject.Find("Player").transform;
        }

    }
}
