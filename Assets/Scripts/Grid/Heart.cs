using UnityEngine;

public class Heart : MonoBehaviour {

    [SerializeField]
    private ParticleSystem particles;

    public void PlayParticles() {
        particles.Play();
    }

    private void Start() {
        Manager.Instance.Heart = this;
    }

}
