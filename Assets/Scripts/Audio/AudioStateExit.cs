using UnityEngine;

/// <summary>
/// Extensao do StateMachineBehaviour para tocar som durante estado de saida do Animator.
/// </summary>
public class AudioStateExit : StateMachineBehaviour
{
    /// <summary>
    /// Clip de passos, tocado enquanto anda.
    /// </summary>
    [SerializeField]
    private AudioClip audioClip;

    /// <summary>
    /// Volume do clip a ser tocado.
    /// </summary>
    [SerializeField]
    private float volume = 1;

    /// <summary>
    /// Instancia do AudioSource gettado a primeira vez para cache.
    /// </summary>
    private AudioSource audioSource;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!audioSource)
            audioSource = animator.GetComponent<AudioSource>();

        audioSource.PlayOneShot(audioClip);
    }
}