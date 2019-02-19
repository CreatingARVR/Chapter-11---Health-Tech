using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;

	// Use this for initialization
	void Awake () {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
            if (s.playOnAwake)
            {
                Play(s.name);
                Debug.Log("Sounds " + s.name + s.source.isPlaying);
            }
        }
	}
	
	public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); 
        s.source.Play();
        Debug.Log(s.source.name + " is Playing");
    }

	public  void Stop(string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name); 
		s.source.Stop();
	}

    public void SetLoop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s.source.loop == false)
        {
            s.source.loop = true;
        }
        else
        {
            s.source.loop = false;
        }
	}

	public bool GetLoop(string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		return s.source.loop;
	}

	public bool IsPlaying(string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		return s.source.isPlaying;
	}

	public void SetPitch(string name, float p)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		s.source.pitch = p;
	}

	public void SetVolume(string name, float v)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		s.source.volume = v;
	}

    public void PlayDelayed(string name, float delay)
    {
		Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.PlayDelayed(delay);
    }

}
