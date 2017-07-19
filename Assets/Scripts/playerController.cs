using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{

    public int moveSpeed = 5;
    int speedx, speedy;

    public int bulletSpeed;
    public GameObject bulletPrefab;
    public GameObject bulletPrefabSnipe;
    public GameObject bulletPrefabSlow;
    public Transform bulletSpawn;
    public float startHealth;
    public float currentHp;
    bool Allowfire1 = true;
    bool Allowfire2 = true;
    bool Allowfire3 = true;
    public Slider hp;

    void Start ()
    {
        currentHp = startHealth;
        hp.maxValue = startHealth;
    }

    void Update () {

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            speedy = 0;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            speedy = 0;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {;
            speedx = 0;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            speedx = 0;
        }


        GetComponent<Rigidbody2D>().velocity = new Vector2 (speedx,speedy);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            speedy = moveSpeed;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            speedy = -moveSpeed;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            speedx = -moveSpeed;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            speedx = moveSpeed;
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(speedx, speedy);

        if (Input.GetKey(KeyCode.UpArrow))
        {
            speedy = moveSpeed;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            speedy = -moveSpeed;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            speedx = -moveSpeed;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            speedx = moveSpeed;
        }

            GetComponent<Rigidbody2D>().velocity = new Vector2(speedx, speedy);

        if ((Input.GetKey(KeyCode.Z)) && (Allowfire1))
        {
            //Default Bullet (x1 dmg)
            FireDefault();
        }
        if ((Input.GetKey(KeyCode.X)) && (Allowfire2))
        {   
            //PowerShot Easy hit (x5 dmg)
            FireSnipe();
        }
        if ((Input.GetKey(KeyCode.C)) && (Allowfire3))
        {
            //Barricade bullet (x10 dmg)
            FireSlow();
        }

    }

    void FireDefault()
    {
        Allowfire1 = false;
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * bulletSpeed;

        Destroy(bullet, 7.0f);

        StartCoroutine(AllowToFire1());
    }

    void FireSnipe()
    {
        Allowfire2 = false;
        var bullet = (GameObject)Instantiate(
            bulletPrefabSnipe,
            bulletSpawn.position,
            bulletSpawn.rotation);

        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * bulletSpeed * 4;

        Destroy(bullet, 3.0f);

        StartCoroutine(AllowToFire2());
    }

    void FireSlow()
    {
       Allowfire3 = false;
        var bullet = (GameObject)Instantiate(
            bulletPrefabSlow,
            bulletSpawn.position,
            bulletSpawn.rotation);

        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * (bulletSpeed/4);
        bullet.GetComponent<Rigidbody2D>().angularVelocity = 300;

        Destroy(bullet, 6.0f);

        StartCoroutine(AllowToFire3());
    }

    IEnumerator AllowToFire1()
    {
        yield return new WaitForSeconds(0.25f);
        Allowfire1 = true;
    }

    IEnumerator AllowToFire2()
    {
        yield return new WaitForSeconds(2);
        Allowfire2 = true;
    }

    IEnumerator AllowToFire3()
    {
        yield return new WaitForSeconds(7);
        Allowfire3 = true;
    }

    void OnCollisionEnter2D(Collision2D col)
    {   
        if(col.gameObject.name == "BlinkerBoss")
        {
            GetComponent<Animator>().Play("PlayerHurt");
            currentHp = currentHp - 10;
        }

        if(col.gameObject.layer == 10)
        {

            GetComponent<Animator>().Play("PlayerHurt");

            if (col.gameObject.name == "EnemyBulletDefault(Clone)")
            {
                Debug.Log("ouch");
                currentHp = currentHp - 8;
                Debug.Log(currentHp);
                Destroy(col.gameObject);
            }

            if (col.gameObject.name == "Missile(Clone)")
            {
                Debug.Log("MissileOuch");
                currentHp = currentHp - 8;
                Debug.Log(currentHp);
            }

            if (col.gameObject.name == "BoomPew(Clone)")
            {
                Debug.Log("MissileBoomOuch");
                currentHp = currentHp - 4;
                Debug.Log(currentHp);
                Destroy(col.gameObject);
            }

        }

        hp.value = currentHp;
    }

    void HitByRay()
    {
        Debug.Log("gitgud");
        currentHp = currentHp - 1;
        Debug.Log(currentHp);
        hp.value = currentHp;
    }

}
