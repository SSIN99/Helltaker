using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSpawner : MonoBehaviour
{
    [SerializeField] GameObject spikePrefeb;
    [SerializeField] int poolCount, genCount;
    [SerializeField] float startX, startY;
    [SerializeField] float waveTime;
    Queue<GameObject> pool;
    private void Start()
    {
        GameObject temp;
        pool = new Queue<GameObject>();
        for(int i = 0; i < poolCount; i++)
        {
            temp = Instantiate(spikePrefeb, transform.position, Quaternion.identity);
            temp.SetActive(false);
            temp.transform.SetParent(gameObject.transform);
            pool.Enqueue(temp);
        }
    }
    public void WaveSpike()
    {
        StartCoroutine(WaveSpike_Co());
    }
    public IEnumerator WaveSpike_Co()
    {
        GameObject temp;
        for(int i = 0; i < 4; i++)
        {
            for (int j = 0; j < genCount; j++)
            {
                temp = pool.Dequeue();
                temp.transform.position = new Vector2(startX + j, startY - i);
                temp.SetActive(true);
                pool.Enqueue(temp);
            }
            yield return new WaitForSeconds(waveTime);
        }
    }

}
