using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioLoop : MonoBehaviour {

	void Start ()
	{
		SceneManager.OnDie += Respawn;
	}

	void Respawn ()
	{
		GetComponent<AudioSource>().Stop();
		GetComponent<AudioSource>().Play();
	}
}
