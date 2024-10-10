using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Box : MonoBehaviour
{
    [SerializeField] private Tilemap wallTile;
    [SerializeField] private float moveTime = 0.05f;
    private Vector3 cur, des;
    private MoveEfxSpawner MoveEfx;
    private KickEfxSpawner kickEfx;
    private void Start()
    {
        cur = transform.position;
        GameObject.FindObjectOfType<MoveEfxSpawner>().TryGetComponent(out MoveEfx);
        GameObject.FindObjectOfType<KickEfxSpawner>().TryGetComponent(out kickEfx);
    }
    public void Move(Vector3 dir)
    {
        des = cur + dir;

        if (!isWall() && !isObject())
        {
            MoveEfx.OnEfx(transform.position);
            kickEfx.OnBigEfx(transform.position);
            StartCoroutine(Move_Co());
        }
        else
        {
            kickEfx.OnSmallEfx(transform.position);
        }
        AudioManager.Instance.PlayEfx(Efx.StoneKick);
    }
    public IEnumerator Move_Co()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(cur, des, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = des;
        cur = transform.position;
    }
    public bool isWall()
    {
        if (wallTile.HasTile(wallTile.WorldToCell(des)))
            return true;
        else
            return false;
    }
    public bool isObject()
    {
        foreach (Collider2D col in Physics2D.OverlapCircleAll(des, 0.4f))
        {
            if (col.CompareTag("Box") ||
                col.CompareTag("Laser") ||
                col.CompareTag("Gen"))
            {
                return true;
            }
        }
        return false;
    }
}
