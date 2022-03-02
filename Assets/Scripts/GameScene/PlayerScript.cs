using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 10f;

    private float movementX;
    private float movementY;

    private Rigidbody2D body;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private string WALK_ANIMATION = "isWalk";
    private string DEATH_ANIMATION = "isDead";

    [SerializeField]
    int maxPlantBombAmountPerTime = 1;
    int currentPlantBombAmount = 0;

    public GameObject bomb;


    private GridLayout gridLayout;
    private Grid grid;

    private float gridCellWidth;
    private float gridCellHeight;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        grid = transform.parent.GetComponent<Grid>();
        gridLayout = transform.parent.GetComponent<GridLayout>();
        gridCellWidth = gridLayout.cellSize.x;
        gridCellHeight = gridLayout.cellSize.y;
    }

    void Start()
    {

    }

    private void Update()
    {
        GridMovement();
        AnimatePlayer();

        if (Input.GetButtonDown("Jump"))
        {
            SetBomb();
        }
    }

    void FixedUpdate()
    {
    
    }

    void GridMovement()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");


        if (movementX > 0)
        {
            transform.position += Vector3.right * Time.deltaTime * moveForce;
        }
        else if (movementX < 0)
        {
            transform.position += Vector3.left * Time.deltaTime * moveForce;
        }

        if (movementY > 0)
        {
            transform.position += Vector3.up * Time.deltaTime * moveForce;
        }
        else if (movementY < 0)
        {
            transform.position += Vector3.down * Time.deltaTime * moveForce;
        }
    }

    void AnimatePlayer()
    {
        if (movementX != 0 || movementY != 0)
        {
            animator.SetBool(WALK_ANIMATION, true);

            if (movementX > 0)
                spriteRenderer.flipX = false;
            else if (movementX < 0)
                spriteRenderer.flipX = true;
        }
        else
        {
            animator.SetBool(WALK_ANIMATION, false);
        }
    }

    void SetBomb()
    {
        SpawnBomb();
        ++currentPlantBombAmount;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") ||
            collision.gameObject.CompareTag("Explosion"))
        {
            animator.SetBool(DEATH_ANIMATION, true);
            Dead();
        }
    }

    private void SpawnBomb()
    {
        Vector3 cellCenterPosition;
        cellCenterPosition = GetCellCenterPosition();
        
        var newBomb = Instantiate(bomb, cellCenterPosition, Quaternion.identity);
        newBomb.transform.parent = grid.transform;

        Debug.Log("The bomb has been planted");
    }

    private Vector3 GetCellCenterPosition()
    {
        Vector3 cellCenterPosition;

        Vector3Int cellPosition = grid.WorldToCell(transform.position);
        cellCenterPosition = grid.GetCellCenterWorld(cellPosition);
        
        Debug.Log(cellPosition);
        return cellCenterPosition;
    }

    private void Dead()
    {
        Debug.Log("You Dead");
    }
}