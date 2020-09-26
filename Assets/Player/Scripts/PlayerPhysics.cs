using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe para controle da fisica do jogador conforme Input do jogador.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerPhysics : MonoBehaviour
{
    /// <summary>
    /// Instancia do Rigidbody;
    /// </summary>
    private Rigidbody2D m_rigidbody2D;

    /// <summary>
    /// Transform cache;
    /// </summary>
    private Transform m_transform;

    /// <summary>
    /// Tocou o Collider do chao?
    /// </summary>
    private bool isGroundTouched = false;

    /// <summary>
    /// Checa se Franq tocou o chao e esta em repouso.
    /// </summary>
    /// <value></value>
    private bool IsGrounded { get { return isGroundTouched; } }

    /// <summary>
    /// Parametros fisicos de caminhada no chao.
    /// </summary>
    public float forceWalk;

    /// <summary>
    /// Parametros fisicos de caminhada durante queda.
    /// </summary>
    public float forceLateralFalling;

    /// <summary>
    /// Forca de Impulso para pulo.
    /// </summary>
    public float impulseJump;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_transform = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 totalForce = Vector2.zero;
        float horizontal = AutoInput.Horizontal;
        if (IsGrounded)
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

        m_rigidbody2D.AddForce(totalForce, ForceMode2D.Impulse);
    }


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (IsGrounded)
        {
            if (Input.GetKeyDown(AutoInput.Jump))
            {
                m_rigidbody2D.AddForce(Vector2.up * impulseJump, ForceMode2D.Impulse);
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
