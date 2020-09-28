using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe geral para obter informacoes do Player (estado, saude, cooldown)
/// </summary>
[RequireComponent(typeof(PlayerPhysics), typeof(Health))]
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Estado do Player.
    /// </summary>
    public enum State
    {
        Walk = Acts.Moving_X & ~Acts.Negative_Y,
        Jump,
        Melt,
        FriendlyBubble,
        Dash,
        Defense,
        Falling = Acts.Negative_Y,
        Dying,
        Dead,
        None
    }

    /// <summary>
    /// Parametros de outros Components que determinam o estado do Player.
    /// </summary>
    private enum Acts : byte
    {
        Negative_Y = 0b_1000_0000,
        Moving_X = 0b_0100_0000,
        Health_0 = 0b_0010_0000,
        // Dashing = 0b_0001_0000,
        // Liquified = 0b_0000_1000,
        // AnimDyingFinish = 0b_0000_0100,
        // AnimDefenserRunning = 0b_0000_0010,
        // AnimBubbleRunning = 0b_0000_0001,
        None = 0b_0000_0000
    }

    /// <summary>
    /// Atuais parametros de outros Components.
    /// </summary>
    private Acts currentActs = Acts.None;

    /// <summary>
    /// Componentes do Player para desativar e ativar.
    /// </summary>
    private MonoBehaviour[] playerComponents;

    /// <summary>
    /// Instância do Componente de saude do Jogador.
    /// </summary>
    private Health playerHeath;

    /// <summary>
    /// Estado atual do jogador.
    /// </summary>
    /// <value></value>
    public static State CurrentState { get; private set; }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        playerComponents = gameObject.GetComponents<MonoBehaviour>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        // currentActs = currentActs | (Acts)(playerHeath.Value <= 0 ? (1 << (byte)Acts.Health_0) : ~(1 << (byte)Acts.Health_0));
        // currentActs = currentActs | (Acts)(playerPhysics.VelocityY < 0 ? (1 << (byte)Acts.Negative_Y) : ~(1 << (byte)Acts.Negative_Y));
        // currentActs = currentActs | (Acts)(playerPhysics.VelocityX < 0 ? (1 << (byte)Acts.Moving_X) : ~(1 << (byte)Acts.Moving_X));
    }

    public void TakeControl ()
    {
        for (int i = 0; i < playerComponents.Length; i++)
        {
            playerComponents[i].enabled = false;
        }
    }

    public void GiveControl ()
    {
        for (int i = 0; i < playerComponents.Length; i++)
        {
            playerComponents[i].enabled = true;
        }
    }
}
