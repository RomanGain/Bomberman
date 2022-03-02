using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombScript : MonoBehaviour
{
    [SerializeField]
    private float bombTimer = 3f;
    private List<Vector3Int> explosions;

    public GameObject explosion;
    public Tilemap tilemap;
    public Grid grid;

    void Awake()
    {

    }

    void Start()
    {
        grid = transform.parent.GetComponent<Grid>();
        SetExplosionCells();
    }

    // Update is called once per frame
    void Update()
    {
        bombTimer -= Time.deltaTime;

        if (bombTimer <= 0.0f)
        {
            SpawnExplosion();
            SpawnExplosionTrail();
            BlowUpBomb();
        }
    }

    void SetExplosionCells() 
    {
        Vector3Int cellPosition = grid.WorldToCell(transform.position);
        explosions = GetCellsAroundBomb(cellPosition);
    }

    private List<Vector3Int> GetCellsAroundBomb(Vector3Int bombCell)
    {
        List<Vector3Int> cellsAround = new List<Vector3Int>();
        cellsAround.Add(new Vector3Int(bombCell.x, bombCell.y + 1, bombCell.z)); // on the top from bomb cell
        cellsAround.Add(new Vector3Int(bombCell.x + 1, bombCell.y, bombCell.z)); // on the right from bomb cell
        cellsAround.Add(new Vector3Int(bombCell.x, bombCell.y - 1, bombCell.z)); //on the bottom from bomb cell
        cellsAround.Add(new Vector3Int(bombCell.x - 1, bombCell.y, bombCell.z)); //on the bottom from bomb cell
        return cellsAround;
    }


    void BlowUpBomb()
    {
        Destroy(gameObject);
    }

    void SpawnExplosion()
    {
        Instantiate(explosion, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    }

    void SpawnExplosionTrail()
    {
        foreach (var explosion in explosions)
        {
            SpawnExplosion(explosion);
        }
    }

    void SpawnExplosion(Vector3Int cellPosition)
    {
        var cellCenterPosition = grid.GetCellCenterWorld(cellPosition);
        Instantiate(explosion, cellCenterPosition, Quaternion.identity);
    }
}