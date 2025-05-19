using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<AudioClip> sounds;
    private AudioSource _audioSource;
    private Dictionary<string, AudioClip> _soundsDict = new Dictionary<string, AudioClip>();
    public static SoundManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            foreach (var clip in sounds)
            {
                _soundsDict[clip.name] = clip;
            }
             Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string name)
    {
        if (_audioSource.isPlaying) return;

        if (_soundsDict.TryGetValue(name, out AudioClip clip))
        {
            _audioSource.PlayOneShot(clip);
        }
    } 

}
