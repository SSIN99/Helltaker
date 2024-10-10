using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEfxSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] efxPrefebs;
    [SerializeField] private PlayerControl player;
    private List<GameObject> pool;
    private int poolCount = 6;
    void Start()
    {
        pool = new List<GameObject>();
        GameObject temp;
        for(int i = 0; i < poolCount; i++)
        {
            temp = Instantiate(efxPrefebs[i%3], transform.position, Quaternion.identity);
            temp.transform.SetParent(gameObject.transform);
            temp.SetActive(false);
            pool.Add(temp);
        }      
    }
    public void OnEfx(Vector2 pos)
    {
        StartCoroutine(OnEfx_Co(pos));
    }
    public IEnumerator OnEfx_Co(Vector2 pos)
    {
        GameObject temp = null;
        while (true)
        {
            int index = Random.Range(0, 6);
            if (!pool[index].activeSelf)
            {
                temp = pool[index];
                break;
            }
        }
        temp.transform.position = pos;
        temp.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        temp.SetActive(false);
    }
}
