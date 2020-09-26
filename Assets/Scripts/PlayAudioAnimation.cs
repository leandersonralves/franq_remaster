using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Classe utilitaria com funcao para tocar som originado de SendMessage ou Animation Event.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class PlayAudioAnimation : MonoBehaviour
{
    /// <summary>
    /// Instancia para AudioSource que a funcao toca o AudioClip.
    /// </summary>
    [SerializeField]
    private AudioSource audioSource;

    /// <summary>
    /// Lista de AudioClip que podem ser tocados.
    /// </summary>
    /// <typeparam name="AudioClip">AudioClip do Unity.</typeparam>
    public List<AudioClip> audioClips = new List<AudioClip>();

    public void PlayAudioClip(string audioClipName)
    {
        var audioClip = audioClips.Find(x => x.name.Equals(audioClipName));
        if (audioClip)
        {
            audioSource.PlayOneShot(audioClip);
        }
        else
        {
            Debug.LogWarning("AudioClip (" + audioClipName + ") nao localizado no AudioSource (" + name + ")");
        }
    }
}
