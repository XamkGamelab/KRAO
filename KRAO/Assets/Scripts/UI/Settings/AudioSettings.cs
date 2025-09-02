using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    public AudioMixer AudioMixer;
    private Slider audioVolumeSlider => GetComponentInChildren<Slider>();
    private Toggle audioMuteToggle => GetComponentInChildren<Toggle>();

    private void Awake()
    {
        // Start listening to volume slider and toggle value changes
        audioVolumeSlider.onValueChanged.AddListener(SetVolume);
        audioMuteToggle.onValueChanged.AddListener(MuteAudio);
    }

    private void SetVolume(float _volume)
    {
        // Master Volume(Slider, logarithmic - 80dB to + 0dB)
        AudioMixer.SetFloat("Volume", Mathf.Log10(_volume) * 20);
    }

    // Mute audio if toggle checked (_muted == true)
    private void MuteAudio(bool _muted)
    {
        if (_muted)
        {
            AudioMixer.SetFloat("Volume", -80f);
        }
        else
        {
            AudioMixer.SetFloat("Volume", 0f);
        }
    }
}