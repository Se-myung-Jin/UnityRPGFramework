using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    AudioSource[] _audioSource = new AudioSource[(int)Define.Sound.MaxCount];
    Dictionary<string, AudioClip> _dicAudioClip = new Dictionary<string, AudioClip>();

    public void Init()
    {
        GameObject root = GameObject.Find("SoundHelper");
        if (root == null)
        {
            root = new GameObject { name = "SoundHelper" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSource[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSource[(int)Define.Sound.Bgm].loop = true;
        }
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSource)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }

        _dicAudioClip.Clear();
    }

    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        if (type == Define.Sound.Bgm)
        {
            AudioClip audioClip = GetOrAddAudioClip(path);
            if (audioClip == null)
            {
                Debug.Log($"AudioClip Missing : {path}");
                return;
            }

            AudioSource audioSource = _audioSource[(int)Define.Sound.Bgm];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioClip audioClip = GetOrAddAudioClip(path);
            if (audioClip == null)
            {
                Debug.Log($"AudioClip Missing : {path}");
                return;
            }

            AudioSource audioSource = _audioSource[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    AudioClip GetOrAddAudioClip(string path)
    {
        AudioClip audioClip = null;
        if (_dicAudioClip.TryGetValue(path, out audioClip))
            return audioClip;

        audioClip = GeneralManager.Resource.Load<AudioClip>(path);
        _dicAudioClip.Add(path, audioClip);

        return audioClip;
    }
}
