using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickEfxSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] efxPrefebs;
    private List<GameObject> smallPool;
    private List<GameObject> bigPool;
    private int poolCount = 8;
    void Start()
    {
        smallPool = new List<GameObject>();
        bigPool = new List<GameObject>();
        GameObject temp;
        for (int i = 0; i < poolCount; i++)
        {
            temp = Instantiate(efxPrefebs[i % 4], transform.position, Quaternion.identity);
            temp.transform.SetParent(gameObject.transform);
            temp.SetActive(false);
            if (i % 4 > 1)
                bigPool.Add(temp);
            else
                smallPool.Add(temp);
        }
    }
    public void OnSmallEfx(Vector2 pos)
    {
        StartCoroutine(OnSmallEfx_Co(pos));
    }
    public IEnumerator OnSmallEfx_Co(Vector2 pos)
    {
        Vector2 offset = new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
        GameObject temp = null;
        while (true)
        {
            int index = Random.Range(0, 4);
            if (!smallPool[index].activeSelf)
            {
                temp = smallPool[index];
                break;
            }
        }
        temp.transform.position = pos + offset;
        temp.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        temp.SetActive(false);
    }
    public void OnBigEfx(Vector2 pos)
    {
        StartCoroutine(OnBigEfx_Co(pos));
    }
    public IEnumerator OnBigEfx_Co(Vector2 pos)
    {
        GameObject temp = null;
        while (true)
        {
            int index = Random.Range(0, 4);
            if (!bigPool[index].activeSelf)
            {
                temp = bigPool[index];
                break;
            }
        }
        temp.transform.position = pos;
        temp.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        temp.SetActive(false);
    }
}
