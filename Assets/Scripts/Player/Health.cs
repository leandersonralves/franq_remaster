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
    /// Vida maxima.
    /// </summary>
    [SerializeField]
    private float maxHealth = 100f;

    /// <summary>
    /// Consulta apenas leitura do valor de saude maxima.
    /// </summary>
    /// <value></value>
    public float MaxHeath { get { return maxHealth; } }

    /// <summary>
    /// Valor da saude atual.
    /// </summary>
    public float CurrentHealth { get; private set; }

    /// <summary>
    /// Instancia do Animator.
    /// </summary>
    [SerializeField]
    private Animator m_animator;

    /// <summary>
    /// Instancia do PlayerController.
    /// </summary>
    [SerializeField]
    private PlayerController playerController;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        CurrentHealth = MaxHeath;
    }

    /// <summary>
    /// Causa Dano no Player, diminuindo o valor de vida.
    /// </summary>
    /// <param name="damageValue">Valor do Dano causado.</param>
    public void Damage (float damageValue)
    {
        if (PlayerController.CurrentState != PlayerController.State.Defense)
        {
            CurrentHealth -= damageValue;
            if (CurrentHealth <= 0)
            {
                playerController.Die();
            }
        }
    }
}
