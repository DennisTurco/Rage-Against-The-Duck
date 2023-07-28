using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public float speed;
    private float time = 0.5f;
    private Vector2 pos0, pos1;
    private bool wait = false;
    private bool got = false;
    private bool end = false;

    void Start() {

    }

    void Update() {
        if(got == false) {
            pos0 = new Vector2(player.transform.position.x, player.transform.position.y);
            Debug.Log("Pos0: " + pos0);
            got = true;
        }

        if(wait == false) {
            StartCoroutine(timer());
        }

        if(end == true) {
            pos1 = new Vector2(player.transform.position.x, player.transform.position.y);
            Debug.Log("Pos1: " + pos1);

            float len = Mathf.Sqrt(Mathf.Pow(pos1.x - pos0.x, 2.0f) + Mathf.Pow(pos1.y - pos0.y, 2.0f));
            Debug.Log("len: " + len);

            if(len > 0.0) {
                Vector2 d = new Vector2((pos1.x - pos0.x) / len, (pos1.y - pos0.y) / len);
                Debug.Log("d: " + d);
                float dist = speed1;
                Vector2 p3 = new Vector2(pos1.x + dist * d.x, pos1.y + dist * d.y);
                Debug.Log("p3: " + p3);

                transform.position = Vector2.MoveTowards(this.transform.position, p3, speed * 0.5f);
            }

            wait = false;
            got = false;
            end = false;
        }

    }

    IEnumerator timer() {
        wait = true;
        yield return new WaitForSeconds(time);

        end = true;
    }
}