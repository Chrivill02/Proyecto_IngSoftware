using UnityEngine;
public class GlueBullet : Proyectil
{
    [SerializeField] private float impactAnimationDuration = 0.3f;
    private Animator anim;
    protected override void Awake() { base.Awake(); anim = GetComponent<Animator>(); }
    
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit) return;
        Damageable target = collision.GetComponent<Damageable>();
        if (target != null || collision.CompareTag("Breakable"))
        { // Afecta enemigos, jefes, minions, rompibles
            hasHit = true;
            rb.linearVelocity = Vector2.zero;
            if (anim != null) anim.SetTrigger("Impact");

            if (target != null) { target.RecibirDano(dano); }
            else if (collision.CompareTag("Breakable")) { Destroy(collision.gameObject); }

            Destroy(gameObject, impactAnimationDuration);
        }
        
    }
}