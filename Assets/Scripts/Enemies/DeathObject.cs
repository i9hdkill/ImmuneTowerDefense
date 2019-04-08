using System.Collections;
using UnityEngine;

public class DeathObject : MonoBehaviour {

    [SerializeField]
    private AudioSource audioSource;
    private SoundDataHolder deathSoundData;

    private void OnEnable() {
        HandleDeath();
    }

    public void Activate(SoundDataHolder deathSoundData, Vector3 pos) {
        this.deathSoundData = deathSoundData;
        transform.position = pos;
        gameObject.SetActive(true);
    }

    private void HandleDeath() {
        PlayDeathSound();
        StartCoroutine(DelayDisable());
    }

    private void PlayDeathSound() {
        Sound.Instance.PlaySoundClipWithSource(deathSoundData.SoundName, audioSource, 0, deathSoundData.Volume);
    }

    private IEnumerator DelayDisable() {
        yield return new WaitForSecondsRealtime(3f);
        gameObject.SetActive(false);
    }
}
