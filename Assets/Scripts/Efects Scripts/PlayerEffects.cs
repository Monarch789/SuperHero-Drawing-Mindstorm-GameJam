
using UnityEngine;

public class PlayerEffects : MonoBehaviour{

    [SerializeField] private ParticleSystem damageBuffParticleSystem;
    [SerializeField] private ParticleSystem healthBuffParticleSystem;

    private void Start() {
        Player.Instance.OnDamageIncreased += Player_OnDamageIncreased;
        Player.Instance.OnHealthIncreased += Player_OnHealthIncreased;
    }

    private void Player_OnHealthIncreased(object sender, System.EventArgs e) {
        ParticleSystem newHealthParticles = Instantiate(healthBuffParticleSystem, transform.position - new Vector3(0f, 1f, 0f), Quaternion.identity);

        newHealthParticles.transform.parent = transform;
    }

    private void Player_OnDamageIncreased(object sender, System.EventArgs e) {
        ParticleSystem newDamageParticles = Instantiate(damageBuffParticleSystem, transform.position - new Vector3(0f, 1f, 0f), Quaternion.identity);

        newDamageParticles.transform.parent = transform;
    }

    private void OnDestroy() {
        Player.Instance.OnDamageIncreased -= Player_OnDamageIncreased;
        Player.Instance.OnHealthIncreased -= Player_OnHealthIncreased;
    }

}
