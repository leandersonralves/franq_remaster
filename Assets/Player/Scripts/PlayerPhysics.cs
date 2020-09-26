using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe para controle da fisica do jogador conforme Input do jogador.
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerPhysics : MonoBehaviour
{
    /// <summary>
    /// Instancia do Rigidbody;
    /// </summary>
    private Rigidbody2D m_rigidbody2D;

    /// <summary>
    /// Tocou o Collider do chao?
    /// </summary>
    private bool isGroundTouched = false;

    /// <summary>
    /// Parametros fisicos de caminhada no chao.
    /// </summary>
    [SerializeField]
    private float forceWalk;

    /// <summary>
    /// Parametros fisicos de caminhada durante queda.
    /// </summary>
    [SerializeField]
    private float forceLateralFalling;

    /// <summary>
    /// Forca de Impulso para pulo.
    /// </summary>
    [SerializeField]
    private float impulseJump;

    /// <summary>
    /// Instancia do Animator.
    /// </summary>
    [SerializeField]
    private Animator m_animator;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 totalForce = Vector2.zero;
        float horizontal = AutoInput.Horizontal;
        if (isGroundTouched)
        {
            if (!Mathf.Approximately(horizontal, 0f))
            {
                totalForce += Vector2.right * forceWalk * horizontal;
            }
        }
        else
        {
            if (!Mathf.Approximately(horizontal, 0f))
            {
                totalForce += Vector2.right * forceLateralFalling * horizontal;
            }
        }

        m_animator.SetBool(AnimatorParams.BOOL_MOVING, Mathf.Abs(m_rigidbody2D.velocity.x) > 0.5f);
        m_animator.SetBool(AnimatorParams.BOOL_FALLING, m_rigidbody2D.velocity.y < -3.0f);
        m_rigidbody2D.AddForce(totalForce, ForceMode2D.Impulse);
    }


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (isGroundTouched)
        {
            if (Input.GetKeyDown(AutoInput.Jump))
            {
                m_rigidbody2D.AddForce(Vector2.up * impulseJump, ForceMode2D.Impulse);
                m_animator.SetTrigger(AnimatorParams.TRIGGER_JUMP);
            }
        }
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(Tags.GROUND))
        {
            isGroundTouched = true;
            m_animator.SetTrigger(AnimatorParams.TRIGGER_GROUND);
        }
    }

    /// <summary>
    /// Sent when a collider on another object stops touching this
    /// object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(Tags.GROUND))
        {
            isGroundTouched = false;
        }
    }
}
