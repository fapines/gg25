using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(PlaySound);
    }

    void PlaySound()
    {
        if (clickSound == null)
        {
            SoundManager.Instance.SetSoundClip(SoundManager.Instance.tapButtonAudioClip);
        }
        else SoundManager.Instance.SetSoundClip(clickSound);
    }
}
