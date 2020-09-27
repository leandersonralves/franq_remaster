using UnityEngine;

/// <summary>
/// Extensao do StateMachineBehaviour para tocar som durante estado de entrada do Animator.
/// </summary>
public class AudioStateLoop : StateMachineBehaviour
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

    private bool cacheEnterStateLoop = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!audioSource)
            audioSource = animator.GetComponent<AudioSource>();

        audioSource.clip = audioClip;
        audioSource.Play();
        cacheEnterStateLoop = audioSource.loop;
        audioSource.loop = true;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!audioSource)
            audioSource = animator.GetComponent<AudioSource>();

        if (audioSource.isPlaying && audioSource.clip != null && audioSource.clip == audioClip)
        {
            audioSource.Stop();
            audioSource.loop = cacheEnterStateLoop;
        }
    }
}