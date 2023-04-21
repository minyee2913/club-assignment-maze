using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private float speed  = 4f;
    private Rigidbody2D rigid;
    public GameObject wall;
    public GameObject cam;
    public GameObject canvas;
    private int type = 0;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        StartCoroutine(yee());
    }

    IEnumerator yee()
    {
        CinemachineVirtualCamera vcam = cam.GetComponent<CinemachineVirtualCamera>();

        for (float i = 25; i > 4f; i-=0.5f) {
            vcam.m_Lens.OrthographicSize = i;

            yield return new WaitForSeconds(0.03f);
        }
    }

    IEnumerator kuku()
    {
        canvas.SetActive(true);
        yield return new WaitForSeconds(2f);

        canvas.SetActive(false);
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        rigid.velocity = new Vector2 (x, y) * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "detector")
        {
            Quaternion rot = wall.transform.rotation;
            if (type == 0) rot.z = 180;
            else if (type == 1) rot.z = 0;
            type++;
            if (type > 1) type = 0;
            wall.transform.rotation = rot;

            StartCoroutine(kuku());

        }
    }
}
