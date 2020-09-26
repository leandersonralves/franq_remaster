using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Melt : MonoBehaviour
{
    /// <summary>
    /// Instancia do Animator.
    /// </summary>
    [SerializeField]
    private Animator m_animator;

    void Update()
    {
        if (Input.GetKeyDown(AutoInput.Melt))
        {
            m_animator.SetBool(AnimatorParams.BOOL_MELTING, true);
        }
        else if (Input.GetKeyUp(AutoInput.Melt))
        {
            m_animator.SetBool(AnimatorParams.BOOL_MELTING, false);
        }
    }
}
