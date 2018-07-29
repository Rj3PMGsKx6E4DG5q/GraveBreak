using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class SoundManager : MonoBehaviour
{
	
	private static SoundManager instance;
	public static SoundManager Instance {
		get {
			instance = instance ?? GameObject.FindObjectOfType<SoundManager> ();
			return instance;
		}
	}



	public class BgmName
	{
		public const string BGM_GAME_0		= "bgm_00";
		public const string BGM_GAME_1		= "bgm_01";
		public const string BGM_GAME_2		= "bgm_02";
		public const string BGM_BOSS		= "bgm_boss";
		public const string BGM_OPENING		= "bgm_opening";
		public const string BGM_ENDING		= "bgm_ending";
	}



	public class SeName
	{
		public const string SE_OK				= "se_ok";
		public const string SE_CANCEL			= "se_cancel";
		public const string SE_MOVE				= "se_move";
		public const string SE_MAKE				= "se_make";
		public const string SE_KNOCK			= "se_knock";
		public const string SE_HOLE				= "se_hole";
		public const string SE_TOUCH			= "se_touch";
		public const string SE_WARP				= "se_warp";
		public const string SE_GIMMICK			= "se_gimmick";
		public const string SE_BUMP				= "se_bump";
		public const string SE_SLIP				= "se_slip";
		public const string SE_BREAK			= "se_break";
		public const string SE_CHANGE			= "se_change";
		public const string SE_GACHA			= "se_gacha";
		public const string SE_RESULT			= "se_result";

		public const string JINGLE_TITLE		= "jin_title";
		public const string JINGLE_START		= "jin_start";
		public const string JINGLE_GOT			= "jin_got";
		public const string JINGLE_1UP			= "jin_1up";
		public const string JINGLE_CLEAR		= "jin_clear";
		public const string JINGLE_CLEAR_TIME0	= "jin_clear_time0";
		public const string JINGLE_GAMEOVER		= "jin_gameover";
	}



	private const int MAX_SE_AUDIO_SOURCE	= 20;


	private AudioSource bgmAudioSource;
	private List<AudioSource> seAudioSourceList;
	private Dictionary<string, AudioClip> seAudioClipList;

	private float bgmVolume		= 1.0f;
	private float seVolume		= 1.0f;

	private bool isMute = false;



	private void Awake ()
	{
		Init ();
	}



	private void Init ()
	{
		bgmAudioSource = gameObject.AddComponent<AudioSource> ();
		bgmAudioSource.loop = true;

		seAudioSourceList = new List<AudioSource> ();
		for (int i = 0; i < MAX_SE_AUDIO_SOURCE; i++) {
			seAudioSourceList.Add (gameObject.AddComponent<AudioSource> ());
		}

		seAudioClipList = new Dictionary<string, AudioClip> ();
	}



	public bool GetMute ()
	{
		return this.isMute;
	}



	public void SetMute (bool isMute)
	{
		if (this.isMute != isMute) {
			this.isMute = isMute;

			bgmAudioSource.mute = isMute;
			foreach (AudioSource audioSource in seAudioSourceList)
				audioSource.mute = isMute;
		}
	}



	public void PlayBgm (string name)
	{
		StopBgm ();
		bgmAudioSource.Stop ();
		bgmAudioSource.clip = Resources.Load<AudioClip> ("Sounds/" + name);
		bgmAudioSource.Play ();
	}



	public void StopBgm ()
	{
		bgmAudioSource.Stop ();
		bgmAudioSource.clip = null;
	}



	public void PauseBgm ()
	{
		bgmAudioSource.Pause ();
	}



	public void ResumeBgm ()
	{
		bgmAudioSource.UnPause ();
	}



	public float GetVolumeBgm ()
	{
		return bgmVolume;
	}



	public void SetVolumeBgm (float volume)
	{
		if (bgmVolume != volume) {
			bgmVolume = volume;
			bgmAudioSource.volume = volume;
		}
	}



	public void PlaySe (string name)
	{
		if (!seAudioClipList.ContainsKey (name))
			seAudioClipList.Add (name, Resources.Load<AudioClip> ("Sounds/" + name));
			
		foreach (AudioSource audioSource in seAudioSourceList) {
			if (!audioSource.isPlaying) {
				audioSource.clip = seAudioClipList [name];
				audioSource.Play ();
				break;
			}
		}
	}



	public void StopSe ()
	{
		foreach (AudioSource audioSource in seAudioSourceList) {
			audioSource.Stop ();
			audioSource.clip = null;
		}
	}



	public void PauseSe ()
	{
		foreach (AudioSource audioSource in seAudioSourceList)
			audioSource.Pause ();
	}



	public void ResumeSe ()
	{
		foreach (AudioSource audioSource in seAudioSourceList)
			audioSource.UnPause ();
	}



	public float GetVolumeSe ()
	{
		return seVolume;
	}



	public void SetVolumeSe (float volume)
	{
		if (seVolume != volume) {
			seVolume = volume;
			foreach (AudioSource audioSource in seAudioSourceList)
				audioSource.volume = volume;
		}
	}

}
