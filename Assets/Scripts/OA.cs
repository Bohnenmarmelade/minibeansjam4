using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OA : MonoBehaviour
{
    public int wait = 5;
    private bool sceneChanged = false;
    private AudioSource oaSource;

    // Start is called before the first frame update
    void Start()
    {
        oaSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!oaSource.isPlaying && !sceneChanged)
        {
            StartCoroutine("startOA");
        }
    }

    IEnumerator startOA()
    {
        sceneChanged = true;
        yield return new WaitForSeconds(wait);
        Debug.Log("change");
        SceneManager.LoadScene("_preload");
    }
}
