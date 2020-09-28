using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Atualiza as informações do HeadUp-Display.
/// </summary>
public class PlayerHeadup : MonoBehaviour
{
    /// <summary>
    /// Instancia do Material do HUD de Saude.
    /// </summary>
    [SerializeField]
    private Material materialHUDHealth;
    
    /// <summary>
    /// Instancia do Material do HUD de Cooldown.
    /// </summary>
    [SerializeField]
    private Material materialHUDCooldown;

    /// <summary>
    /// Nome da propriedade Cutoff dentro do material.
    /// </summary>
    private const string TAG_CUTOFF = "_Cutoff";

    /// <summary>
    /// Instancia do Componente de Health para consultar a saude atual.
    /// </summary>
    [SerializeField]
    private Health healthPlayer;

    /// <summary>
    /// Instancia do Componente de Cooldown para consultar a estamina atual.
    /// </summary>
    [SerializeField]
    private Skills.Cooldown cooldownPlayer;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        materialHUDHealth.SetFloat(TAG_CUTOFF, 1 - (healthPlayer.CurrentHealth / healthPlayer.MaxHeath));
        materialHUDCooldown.SetFloat(TAG_CUTOFF, 1 - cooldownPlayer.AvailableCooldown / cooldownPlayer.MaxCooldown);
    }
}
