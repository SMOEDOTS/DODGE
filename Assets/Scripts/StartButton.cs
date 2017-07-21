using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartButton : MonoBehaviour {

    public GameObject thisbutton;
	// Use this for initialization

	void Start () {
        StartCoroutine(reactive());
	}
	
	// Update is called once per frame
	void Update () {

    }


    void OnClick()
    {
        SceneManager.LoadScene("BossOne");
    }


    IEnumerator reactive()
    {
        //Fix This
        thisbutton.SetActive(false);
        yield return new WaitForSeconds(3);
        print(Time.time);
        thisbutton.SetActive(true);

    }

}
