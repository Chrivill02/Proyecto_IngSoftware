using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public float fuerzaSalto;
    public NewMonoBehaviourScript gameManager;

    public float speed = 5f;
    private Rigidbody2D rigidbody2D;
    private float inputMovimiento;



    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputMovimiento = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && !animator.GetBool("estaSaltando"))
        {
            animator.SetBool("estaSaltando", true);
            rigidbody2D.AddForce(new Vector2(0, fuerzaSalto));
        }
    }

    private void FixedUpdate()
    {
        rigidbody2D.linearVelocity = new Vector2(inputMovimiento * speed, rigidbody2D.linearVelocity.y);
        if (inputMovimiento != 0)
        {
            animator.SetBool("estaCorriendo", true);
        }
        else
        {
            animator.SetBool("estaCorriendo", false);
        }

        if (inputMovimiento < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (inputMovimiento > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Suelo")
        {
            animator.SetBool("estaSaltando", false);
        }

        if (collision.gameObject.tag == "Obstaculo")
        {
            gameManager.gameOver = true;
        }
    }
}
