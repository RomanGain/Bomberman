using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField]
    private float explosionTrail;

    private float gridCellWidth = 5f;
    private float gridCellHeight = 5f;

    private List<Vector3Int> explosionTrails;

    private void Awake()
    {
        explosionTrails = new List<Vector3Int>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Explode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if *CompareTag
    }

    private void Explode()
    {
        Destroy(gameObject, 0.5f);
    }
}