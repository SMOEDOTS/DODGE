using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlinkerController : MonoBehaviour {

    public GameObject bulletPrefabNormal;
    public GameObject homingMissilePrefab;
    public GameObject laserPrefab;
    public GameObject hPack;
    public Transform bulletSpawn;
    public Transform RightSpawn;
    public Transform FarRightSpawn;
    public Transform LeftSpawn;
    public Transform FarLeftSpawn;
    public Transform bspawn1;
    public Transform bspawn2;
    public Transform bspawn3;
    public Transform bspawn4;
    public int bulletSpeed;
    bool Allowfire = true;
    bool HpTimer = false;
    bool allowHpax = true;
    public float startHealth;
    public float currentHealth;
    public Slider healthBar;
    private float timeToWait;

    int cycle = 1;

    // Use this for initialization
    void Start() {

        currentHealth = startHealth;
        healthBar.maxValue = startHealth;

    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.V))
        {
            cycle++;
            Debug.Log("boss cycle " + cycle);
        }

        if (HpTimer)
        {
            StartCoroutine(reno());
            HpTimer = false;
        }

        if (Allowfire)
        {
            Allowfire = false;

            if (cycle == 1)
            {
                defaultShot();

                timeToWait = 5;
            }

            if (cycle == 2)
            {
                StartCoroutine(burst());

                timeToWait = 5;
            }

            if (cycle == 3)
            {
                sgunShot();

                timeToWait = 5;
            }

            if (cycle == 4)
            {
                StartCoroutine(sburst(3));

                timeToWait = 5;
            }

            if (cycle == 5)
            {
                StartCoroutine(sburst(5));

                timeToWait = 5;
            }

            if (cycle == 6)
            {
                missile();

                timeToWait = 4;
            }

            if (cycle == 7)
            {
                StartCoroutine(burstmissile());

                timeToWait = 5;
            }

            if (cycle == 8)
            {
                missile();
                StartCoroutine(sburst(5));
                timeToWait = 5;
            }

            if (cycle == 9)
            {
                StartCoroutine(burstmissile());
                StartCoroutine(sburst(2));
                timeToWait = 5;
            }

            if (cycle == 10)
            {
                
                    if (allowHpax)
                    {
                        allowHpax = false;
                        HpTimer = true;
                    }

                StartCoroutine(burstmissile());
                StartCoroutine(sburst(5));

                timeToWait = 3;
            }

            if (cycle == 11)
            {
                StartCoroutine(burstmissile());
                StartCoroutine(sburst(7));
                timeToWait = 5;
            }

            if (cycle == 12)
            {
                StartCoroutine(burstmissile());

                timeToWait = 2.5f;
            }

            if (cycle == 13)
            {
                laser();
                timeToWait = 12;
            }

            if (cycle == 14)
            {
                laser();
                laser();
                timeToWait = 4;
            }

            if (cycle == 15)
            {
                laser();
                laser();
                StartCoroutine(sburst(5));
                timeToWait = 4;
            }

            if (cycle == 16)
            {
                laser();
                laser();
                StartCoroutine(burstmissile());
                StartCoroutine(sburst(5));
                timeToWait = 5;
            }

            if (cycle == 17)
            {
                laser();
                laser();
                StartCoroutine(sburst(5));
                timeToWait = 4;
            }

            if (cycle == 18)
            {
                var randomNum = Mathf.Round(Random.value);
                if (randomNum == 0)
                {
                    laser();
                    laser();
                    laser();
                    StartCoroutine(sburst(5));
                    timeToWait = 4;
                }

                if (randomNum == 1)
                {
                    StartCoroutine(burstmissile());
                    StartCoroutine(sburst(5));
                    timeToWait = 8;
                }
            }

            if (cycle == 19)
            {
                missile();
                StartCoroutine(sburst(1));
                laser();
                timeToWait = 2;
            }

            if (cycle == 20)
            {
                    laser();
                    laser();
                    laser();
                    StartCoroutine(burstmissile());
                    StartCoroutine(sburst(8));
                    timeToWait = 8;
                    healp();
            }


            StartCoroutine(waitSome());
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
            Destroy(col.gameObject);

            if (col.gameObject.name == "Bullet(Clone)")
            {
                currentHealth = currentHealth - 2;
            }

            if (col.gameObject.name == "PowerShot(Clone)")
            {
                currentHealth = currentHealth - 6;
            }

            if (col.gameObject.name == "BulletBarrier(Clone)")
            {
                currentHealth = currentHealth - 200;
            }

        }

        healthBar.value = currentHealth;

        if (currentHealth <= healthBar.maxValue / 10 || currentHealth <= 0)
        {
            cycle++;
            startHealth = startHealth * 1.1f;
            currentHealth = startHealth;
            healthBar.maxValue = startHealth;
            Debug.Log("boss cycle " + cycle);

        }
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
            yield return new WaitForSeconds(0.8f);
        }
    }

    IEnumerator sburst(int rpt)
    {
        for (int i = 0; i < rpt; i++)
        {
            sgunShot();
            yield return new WaitForSeconds(0.2f);
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

    IEnumerator reno()
    {
        healp();
        var ttw = 15;
        var RNG = Mathf.Round(Random.value);
        if (RNG == 0)
        {
            ttw = 15;
        }
        else
        {
            ttw = 20;
        }
        yield return new WaitForSeconds(ttw);
        HpTimer = true;
        
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

    void sgunShot()
    {
        var bullet = (GameObject)Instantiate(
        bulletPrefabNormal,
        bulletSpawn.position,
        bulletSpawn.rotation);

        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * -bulletSpeed;

        Destroy(bullet, 2.2f);


        var bullet1 = (GameObject)Instantiate(
        bulletPrefabNormal,
        bulletSpawn.position,
        bspawn1.rotation);

        bullet1.GetComponent<Rigidbody2D>().velocity = bullet1.transform.up * -bulletSpeed;

        Destroy(bullet1, 3.2f);

        var bullet2 = (GameObject)Instantiate(
        bulletPrefabNormal,
        bulletSpawn.position,
        bspawn2.rotation);

        bullet2.GetComponent<Rigidbody2D>().velocity = bullet2.transform.up * -bulletSpeed;

        Destroy(bullet2, 3.2f);

        var bullet3 = (GameObject)Instantiate(
        bulletPrefabNormal,
        bulletSpawn.position,
        bspawn3.rotation);

        bullet3.GetComponent<Rigidbody2D>().velocity = bullet3.transform.up * -bulletSpeed;

        Destroy(bullet3, 3.2f);

        var bullet4 = (GameObject)Instantiate(
        bulletPrefabNormal,
        bulletSpawn.position,
        bspawn4.rotation);

        bullet4.GetComponent<Rigidbody2D>().velocity = bullet4.transform.up * -bulletSpeed;

        Destroy(bullet4, 3.2f);
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

        var whichSpawn = Mathf.Round(Random.value * 3);

        if (whichSpawn == 0)
        {
            var bullet = (GameObject)Instantiate(laserPrefab, FarLeftSpawn.position, FarLeftSpawn.rotation);

            bullet.GetComponent<LaserDrone>().target = GameObject.Find("Player").transform;
        }

        if (whichSpawn == 1)
        {
            var bullet = (GameObject)Instantiate(laserPrefab, LeftSpawn.position, LeftSpawn.rotation);

            bullet.GetComponent<LaserDrone>().target = GameObject.Find("Player").transform;
        }

        if (whichSpawn == 2)
        {
            var bullet = (GameObject)Instantiate(laserPrefab, RightSpawn.position, RightSpawn.rotation);

            bullet.GetComponent<LaserDrone>().target = GameObject.Find("Player").transform;
        }

        if (whichSpawn == 3)
        {
            var bullet = (GameObject)Instantiate(laserPrefab, FarRightSpawn.position, FarRightSpawn.rotation);

            bullet.GetComponent<LaserDrone>().target = GameObject.Find("Player").transform;
        }

    }

    void healp()
    {

        var whichSpawn = Mathf.Round(Random.value * 3);

        if (whichSpawn == 0)
        {
            var bullet = (GameObject)Instantiate(hPack, FarLeftSpawn.position, FarLeftSpawn.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * -(bulletSpeed / 2);

            Destroy(bullet, 8f);
        }

        if (whichSpawn == 1)
        {
            var bullet = (GameObject)Instantiate(hPack, LeftSpawn.position, LeftSpawn.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * -(bulletSpeed / 2);

            Destroy(bullet, 8f);
        }

        if (whichSpawn == 2)
        {
            var bullet = (GameObject)Instantiate(hPack, RightSpawn.position, RightSpawn.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * -(bulletSpeed / 2);

            Destroy(bullet, 8f);
        }

        if (whichSpawn == 3)
        {
            var bullet = (GameObject)Instantiate(hPack, FarRightSpawn.position, FarRightSpawn.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * -(bulletSpeed / 2);

            Destroy(bullet, 8f);
        }


    }
}
