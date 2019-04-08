using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour {

    public static Sound Instance { get; private set; }

    private const float DefaultSoundVolume = 0.5f;
    private const float DefaultMusicVolume = 0.75f;

    [SerializeField]
    private AudioSource musicPlayer;
    [SerializeField]
    private AudioSource soundPlayer;
    [SerializeField]
    private Slider musicVolumeSlider;
    [SerializeField]
    private Slider soundVolumeSlider;
    [SerializeField]
    private MusicData[] musicClips;
    [SerializeField]
    private SoundData[] soundClips;
    private float soundVolume;

    public float SoundVolume {
        get {
            return soundVolume;
        }
    }

    public void PlayMusicClip(MusicEnum searchedMusic, bool shouldLoop) {
        foreach (MusicData data in musicClips) {
            if (data.name == searchedMusic) {
                musicPlayer.clip = data.clip;
                musicPlayer.loop = shouldLoop;
                musicPlayer.Play();
            }
        }
    }

    public void PlaySoundClip(SoundEnum searchEnum, float volume) {
        foreach (SoundData data in soundClips) {
            if (data.name == searchEnum) {
                //soundPlayer.clip = data.clip;
                soundPlayer.PlayOneShot(data.clip, volume);
            }
        }
    }

    public void StopMusic() {
        musicPlayer.Stop();
    }

    public void AdjustMusicVolume() {
        musicPlayer.volume = musicVolumeSlider.value;
    }

    public void AdjustSoundVolume() {
        soundVolume = soundVolumeSlider.value;
        soundPlayer.volume = soundVolume;
    }

    public void SaveSoundVolumeIngame() {
        //Please no hate because of this :(
        //FindByType<AudioSource>() would also return Sources on Manager which shouldn't be touched. By tag might be faster anyway
        AudioSource[] entitySources = GameObject.FindGameObjectsWithTag(TagData.Entity).Select(x => x.GetComponent<AudioSource>()).ToArray();
        for (int i = 0; i < entitySources.Length; i++) {
            entitySources[i].volume = soundVolume;
        }
        AudioSource[] buildingSources = GameObject.FindGameObjectsWithTag(TagData.Tower).Select(x => x.GetComponent<AudioSource>()).ToArray();
        for (int i = 0; i < buildingSources.Length; i++) {
            buildingSources[i].volume = soundVolume;
        }
    }

    public void PlaySoundClipWithSource(SoundEnum soundEnum, AudioSource source, float duration, float volume) {
        foreach (SoundData data in soundClips) {
            if (data.name == soundEnum) {
                source.clip = data.clip;
                source.PlayOneShot(data.clip, volume);
                if (duration > 0) {
                    StartCoroutine(PlayForDuration(duration, source));
                }
            }
        }
    }

    private IEnumerator PlayForDuration(float duration, AudioSource source) {
        source.loop = true;
        yield return new WaitForSeconds(duration);
        source.loop = false;
        source.Stop();
    }

    private void Awake() {
        Instance = this;
        soundVolume = DefaultSoundVolume;
    }

    private void Start() {
        musicPlayer.volume = DefaultMusicVolume;
        soundPlayer.volume = DefaultSoundVolume;
        musicVolumeSlider.value = DefaultMusicVolume;
        soundVolumeSlider.value = DefaultSoundVolume;
    }

}
