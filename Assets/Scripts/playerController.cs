using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

        if (currentHp <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

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
            //PowerShot Easy hit (x3 dmg)
            FireSnipe();
        }
        if ((Input.GetKey(KeyCode.C)) && (Allowfire3))
        {
            //Barricade bullet (x100 dmg)
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
        yield return new WaitForSeconds(1.5f);
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
            currentHp = currentHp - 100;
        }

        if(col.gameObject.layer == 10)
        {

            GetComponent<Animator>().Play("PlayerHurt");

            if (col.gameObject.name == "EnemyBulletDefault(Clone)")
            {
                currentHp = currentHp - 2;
                Destroy(col.gameObject);
            }

            if (col.gameObject.name == "Missile(Clone)")
            {
                currentHp = currentHp - 8;
                Destroy(col.gameObject);
            }

            if (col.gameObject.name == "BoomPew(Clone)")
            {
                currentHp = currentHp - 5;
                Destroy(col.gameObject);
            }

            if (col.gameObject.name == "HealthPack(Clone)")
            {
                Debug.Log("RENOOOOOO JACKSON");
                GetComponent<Animator>().Play("ShipHeal");
                Destroy(col.gameObject);
                currentHp = currentHp + 60;
                Destroy(col.gameObject);
            }

        }

        hp.value = currentHp;
    }

    void HitByRay()
    {
        Debug.Log("gitgud");
        currentHp = currentHp - 2;
        hp.value = currentHp;
    }

}
