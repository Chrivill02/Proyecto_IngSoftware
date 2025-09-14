using System;
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


    public Transform detectorSuelo; 
    public float radioDetector = 0.1f;
    public LayerMask layerSuelo;
    private bool estaEnSuelo;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D golpe = Physics2D.OverlapCircle(detectorSuelo.position, radioDetector, layerSuelo);
        estaEnSuelo = golpe && Mathf.Abs(golpe.transform.up.y) > 0.9f;

        if (estaEnSuelo && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("estaSaltando", true);
            rigidbody2D.AddForce(new Vector2(0, fuerzaSalto));
        }
        inputMovimiento = Input.GetAxis("Horizontal");
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
        if (estaEnSuelo)
        {
            animator.SetBool("estaSaltando", false);
        }

        if (collision.gameObject.tag == "Obstaculo")
        {
            gameManager.gameOver = true;
        }
    }
}
