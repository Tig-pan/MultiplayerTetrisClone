using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisPiece : MonoBehaviour
{
    public int x;
    public int y;

    public IEnumerator LineClear()
    {
        float ii = Random.Range(0.75f, 1.5f);
        float i = ii;
        float x = Random.Range(-2f, 2f);
        float y = Random.Range(-0.5f, 1f);
        float r = Random.Range(-360f, 360f);

        Invoke("Kill", ii);

        GetComponent<SpriteRenderer>().sortingOrder = 4;

        while (i > 0 && this != null)
        {
            transform.position += new Vector3(x, y, 0) * Time.deltaTime;
            transform.Rotate(new Vector3(0, 0, r) * Time.deltaTime);
            transform.localScale = new Vector3(i / ii, i / ii, 1);
            y -= Time.deltaTime * 3f;
            yield return 0;
            i -= Time.deltaTime;
        }
    }

    void Kill()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject);
    }
}
