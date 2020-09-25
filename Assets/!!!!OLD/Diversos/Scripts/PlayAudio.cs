using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlayAudio : MonoBehaviour {

	void Play () {
		if(GetComponent<AudioSource>().clip != null)
			GetComponent<AudioSource>().Play();
	}
}
