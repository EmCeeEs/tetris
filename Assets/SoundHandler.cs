using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler: MonoBehaviour
{
	[SerializeField]
	private List<AudioClip> rotateSound;
	[SerializeField]
	private List<AudioClip> cannotSound;
	[SerializeField]
	private List<AudioClip> tetrisSound;

	private AudioSource audioSource;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();

	}


	public void CanRotateNoise()
	{
		AudioClip clip = GetRandomClip(rotateSound);
		audioSource.PlayOneShot(clip);
	}
	public void CanNotRotateNoise()
	{
		AudioClip clip = GetRandomClip(cannotSound);
		audioSource.PlayOneShot(clip);
	}
	public void ClearRowNoise()
	{
		AudioClip clip = GetRandomClip(tetrisSound);
		audioSource.PlayOneShot(clip);
	} 

	private AudioClip GetRandomClip(List<AudioClip> audioList)
	{
		return audioList[Random.Range(0, audioList.Count)];
	}
}
