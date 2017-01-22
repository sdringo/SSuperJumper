using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public float scrollSpeed = 0.01f;

    private GameObject bg1 = null;
    private GameObject bg2 = null;
    private Bounds bounds;

    private GameObject player = null;

    // Use this for initialization
    void Start () {
        bg1 = transform.GetChild(0).gameObject;
        bg2 = transform.GetChild(1).gameObject;

        bounds = bg2.GetComponent<SpriteRenderer>().bounds;
        bg2.transform.Translate(0, bounds.size.y, 0);

        Vector3 initPos = bounds.min / 2;

        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(0, initPos.y, 0);
    }
	
	// Update is called once per frame
	void Update () {
        bg1.transform.Translate(0, -scrollSpeed, 0);
        bg2.transform.Translate(0, -scrollSpeed, 0);

        if (bg1.transform.position.y < -bounds.size.y)
            bg1.transform.Translate(0, bounds.size.y * 2, 0);

        if (bg2.transform.position.y < -bounds.size.y)
            bg2.transform.Translate(0, bounds.size.y * 2, 0);
    }
}
