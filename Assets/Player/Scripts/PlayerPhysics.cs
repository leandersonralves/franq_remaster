using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Struct para definicao de parametros fisicos em determinados estados (pulando, andando).
/// </summary>
[System.Serializable]
public struct PhysicsParameters
{
    public float force;

    public float maxSpeed;

    public float MaxSqrtSpeed { get { return maxSpeed * maxSpeed; } }
}

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
    public PhysicsParameters walk;

    /// <summary>
    /// Parametros fisicos de caminhada durante queda.
    /// </summary>
    public PhysicsParameters walkInFalling;

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
        if (IsGrounded)
        {
            float horizontal = AutoInput.Horizontal;
            if (!Mathf.Approximately(horizontal, 0f))
            {
                if (m_rigidbody2D.velocity.sqrMagnitude > walk.MaxSqrtSpeed)
                    m_rigidbody2D.velocity = Vector3.ClampMagnitude(m_rigidbody2D.velocity, walk.maxSpeed);

                m_rigidbody2D.AddForce (Vector2.right * walk.force * horizontal * Time.fixedDeltaTime, ForceMode2D.Impulse);
            }
            else if (!Mathf.Approximately(m_rigidbody2D.velocity.x, 0f))
            {
                m_rigidbody2D.velocity = new Vector2 (0f , m_rigidbody2D.velocity.y); 
            }
        }
        else
        {
            float horizontal = AutoInput.Horizontal;
            if (!Mathf.Approximately(horizontal, 0f))
            {
                m_rigidbody2D.AddForce (Vector2.right * walkInFalling.force * horizontal * Time.fixedDeltaTime, ForceMode2D.Impulse);
            }
            if (m_rigidbody2D.velocity.x > walkInFalling.maxSpeed)
                m_rigidbody2D.velocity = new Vector3(walkInFalling.maxSpeed, m_rigidbody2D.velocity.y);
        }
    }

    void Update ()
    {
        if (IsGrounded)
        {
            if (Input.GetKeyDown(AutoInput.Jump))
            {
                m_rigidbody2D.AddForce(Vector3.up * impulseJump * Time.fixedDeltaTime, ForceMode2D.Impulse);
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
