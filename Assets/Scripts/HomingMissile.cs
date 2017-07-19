using UnityEngine;
using System.Collections;

public class HomingMissile : MonoBehaviour
{

    public Transform target;
    public int speed;
    bool TRIGGERED = false;
    bool boom = false;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (TRIGGERED == false)
        {
            //rotate to look at the player
            var dir = target.position - transform.position;
            var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            this.gameObject.GetComponent<Rigidbody2D>().AddForce(this.gameObject.transform.up * speed);
        }

        if (boom)
        {
            Destroy(this.gameObject);
        }

    }

    void OnTriggerEnter2D()
    {
        TRIGGERED = true;
        StartCoroutine(lightFuse());
        GetComponent<Animator>().Play("Kaboom");
    }

    IEnumerator lightFuse()
    {
        yield return new WaitForSeconds(1.1f);
        boom = true;
    }

}
