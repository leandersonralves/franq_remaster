using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BubblePhysics : MonoBehaviour
{
    /// <summary>
    /// Instancia do Rigidbody Controlado.
    /// </summary>
    [SerializeField]
    private Rigidbody2D m_rigidbody2D;

    /// <summary>
    /// Forca aplicada quando movimentada.
    /// </summary>
    [SerializeField]
    private float moveForce = 10f;

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        Vector2 totalForce = (Vector2.right * AutoInput.Horizontal + Vector2.up * AutoInput.Vertical) * moveForce;
        m_rigidbody2D.AddForce(totalForce, ForceMode2D.Force);
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        m_rigidbody2D.AddForce(transform.right * moveForce, ForceMode2D.Impulse);
    }
}
