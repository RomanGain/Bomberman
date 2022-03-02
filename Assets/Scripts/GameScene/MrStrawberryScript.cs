using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrStrawberryScript : MonoBehaviour
{
    // Start is called before the first frame update
    private float MoveX;
    private float MoveY;

    private Animator animator;
    private string WALK_ANIMATION = "isWalk";
    private string DEATH_ANIMATION = "isDead";

    [SerializeField]
    private float moveForce = 10f;

    private float direction = -1f;
    private int currentSteps = 0;
    private int maxSteps = 100;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Start()
    {
        SetDirection();
    }

    private void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetWalkAnimation();
        Move();
    }

    void SetWalkAnimation()
    {
        animator.SetBool(WALK_ANIMATION, true);
    }

    private void Move()
    {
        if (currentSteps < maxSteps)
        {
            transform.position += new Vector3(direction, 0f) * Time.deltaTime * moveForce;
            ++currentSteps;
        }
        else
        {
            currentSteps = 0;
            direction *= -1;
            SetDirection();
        }
    }

    private void SetDirection()
    {
        if (direction < 0)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Explosion"))
        {
            animator.SetBool(DEATH_ANIMATION, true);
            Destroy(gameObject, 0.5f);
        }
    }
}