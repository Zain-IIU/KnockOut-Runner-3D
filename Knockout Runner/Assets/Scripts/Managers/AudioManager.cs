using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sounds[] sounds;

  [SerializeField]  AudioSource source;
  [SerializeField]  private AudioSource permanentSource;

    [Header("Hit Sounds Effects")] [SerializeField]
    private Sounds[] soundsArray;
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Play(string sound)
    {
        var s = Array.Find(sounds, item => item.name == sound);

        source.loop = s.loop;
        source.clip = s.clip;
        source.volume = s.volum;
        source.pitch = s.pitch;
        source.Play();
        
    }

    public void Play(string sound,bool isPermanent)
    {
        var s = Array.Find(sounds, item => item.name == sound);

        permanentSource.loop = s.loop;
        permanentSource.clip = s.clip;
        permanentSource.volume = s.volum;
        permanentSource.pitch = s.pitch;
        permanentSource.Play();
        
    }
    

    public void PlayRandomFromArray()
    {
        var index = Random.Range(0, soundsArray.Length);
        var s = soundsArray[index];
        source.loop = s.loop;
        source.clip = s.clip;
        source.volume = s.volum;
        source.pitch = s.pitch;
        source.Play();
    }

    public void PlayWithAudioSource(string sound)
    {
        var newSource = gameObject.AddComponent<AudioSource>();
        
        var s = Array.Find(sounds, item => item.name == sound);

        newSource.loop = s.loop;
        newSource.clip = s.clip;
        newSource.volume = s.volum;
        newSource.pitch = s.pitch;
        newSource.Play();
        StartCoroutine(nameof(DestroySource),newSource);

    }

    IEnumerator DestroySource(AudioSource s)
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(s);
    }


  
    
 
    
}