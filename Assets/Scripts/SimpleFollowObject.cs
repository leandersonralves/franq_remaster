using UnityEngine;

/// <summary>
/// Classe para controle de camera simples, seguindo o Target, com damp.
/// </summary>
public class SimpleFollowObject : MonoBehaviour
{
    /// <summary>
    /// Target a ser seguido.
    /// </summary>
    [SerializeField]
    private Transform follow;

    /// <summary>
    /// Offset de posicao até o Target.
    /// </summary>
    [SerializeField]
    private Vector3 offsetPosition;

    /// <summary>
    /// Cache do Transform atual.
    /// </summary>
    private Transform m_transform;

    /// <summary>
    /// Tempo para alcançar a posicao desejada.
    /// </summary>
    [SerializeField]
    private float timeToReachPos = 0.5f;

    private Vector3 currentCameraVel;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        m_transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        m_transform.position = Vector3.SmoothDamp(
            m_transform.position,
            follow.position + offsetPosition,
            ref currentCameraVel,
            timeToReachPos
        );

    }
}
