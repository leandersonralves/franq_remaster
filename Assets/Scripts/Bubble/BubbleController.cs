using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe para controle do tempo de vida da Bolha (ativar/desativar) conforme o caso.
/// </summary>
[RequireComponent(typeof(Animator))]
public class BubbleController : MonoBehaviour
{
    /// <summary>
    /// Tempo de vida que a bolha fica ativa.
    /// </summary>
    /// <returns></returns>
    [SerializeField]
    private const float TOTAL_LIFE_TIME = 5f;

    /// <summary>
    /// Tempo de aguardo da animacao de estouro.
    /// </summary>
    /// <returns></returns>
    private const float TOTAL_ANIM_BURST = 2f;

    /// <summary>
    /// Posicao original relativa ao root (Player).
    /// </summary>
    /// <returns></returns>
    private Vector3 originLocalPosition = new Vector3(0.3f, 0.85f, 0f);

    /// <summary>
    /// Instancia do Animator da Bolha.
    /// </summary>
    [SerializeField]
    private Animator animator;

    /// <summary>
    /// Instancia do PlayerController para pegar/dar controle.
    /// </summary>
    private PlayerController playerController;

    private Transform cacheTransform;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        cacheTransform = transform;
        playerController = GetComponentInParent<PlayerController>();
    }

    /// <summary>
    /// Cria uma Bolha, se ja exister, ira desativar.
    /// </summary>
    public void Create ()
    {
        if (isActiveAndEnabled)
        {
            StopAllCoroutines();
            StartCoroutine(CounterLife(0f));
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            cacheTransform.localPosition = originLocalPosition;
            StartCoroutine(CounterLife(TOTAL_LIFE_TIME));
        }
    }

    /// <summary>
    /// Contator para destruir a bolha com atraso.
    /// </summary>
    /// <param name="delay">Tempo de atraso para estourar a bolha.</param>
    IEnumerator CounterLife (float delay)
    {
        playerController.TakeControl();

        yield return new WaitForSeconds(delay);

        animator.SetTrigger(AnimatorParams.TRIGGER_ON_BURST);
        yield return new WaitForSeconds(TOTAL_ANIM_BURST);

        playerController.GiveControl();
        gameObject.SetActive(false);
    }
}
