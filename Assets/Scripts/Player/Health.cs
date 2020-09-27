using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe para controle da saude.
/// </summary>
[RequireComponent(typeof(Animator))]
public class Health : MonoBehaviour
{
    /// <summary>
    /// Valor da saude entre [0,100), 0 = sem saude.
    /// </summary>
    public float Value = 100f;

    /// <summary>
    /// Instancia do Animator.
    /// </summary>
    [SerializeField]
    private Animator m_animator;

    /// <summary>
    /// Causa Dano no Player, diminuindo o valor de vida.
    /// </summary>
    /// <param name="damageValue">Valor do Dano causado.</param>
    public void Damage (float damageValue)
    {
        if (Player.CurrentState != Player.State.Defense)
        {
            Value -= damageValue;
        }
    }
}
