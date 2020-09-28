using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    /// Tempo de atraso para recarregar cena.
    /// </summary>
    private const float DELAY_RELOAD_SCENE = 3f;

    /// <summary>
    /// Atuais parametros de outros Components.
    /// </summary>
    private Acts currentActs = Acts.None;

    /// <summary>
    /// Componentes do Player para desativar e ativar quando o controle está sobre outro objeto.
    /// </summary>
    [SerializeField]
    private MonoBehaviour[] playerComponents;

    /// <summary>
    /// Estado atual do jogador.
    /// </summary>
    /// <value></value>
    public static State CurrentState { get; private set; }

    /// <summary>
    /// Instancia do Animator.
    /// </summary>
    public Animator animator;

    /// <summary>
    /// Inicia processo de Morte.
    /// </summary>
    public void Die()
    {
        animator.SetBool(AnimatorParams.BOOL_DYING, true);
        StartCoroutine(ReloadScene());
    }

    /// <summary>
    /// Recarrega a cena atual com atraso.
    /// </summary>
    /// <returns></returns>
    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(DELAY_RELOAD_SCENE);
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }

    public void TakeControl()
    {
        for (int i = 0; i < playerComponents.Length; i++)
        {
            playerComponents[i].enabled = false;
        }
    }

    public void GiveControl()
    {
        for (int i = 0; i < playerComponents.Length; i++)
        {
            playerComponents[i].enabled = true;
        }
    }
}
