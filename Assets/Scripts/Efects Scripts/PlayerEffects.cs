
using UnityEngine;

public class PlayerEffects : MonoBehaviour{

    [SerializeField] private ParticleSystem damageBuffParticleSystem;
    [SerializeField] private ParticleSystem healthBuffParticleSystem;

    private void Start() {
        DamageBuff.OnDamageAdd += DamageBuff_OnDamageAdd;
        HealthBuff.OnHealthAdd += HealthBuff_OnHealthAdd;
    }

    private void HealthBuff_OnHealthAdd(object sender, HealthBuff.OnHealthAddEventArgs e) {
        ParticleSystem newHealthParticles =  Instantiate(healthBuffParticleSystem,transform.position - new Vector3(0f,1f,0f),Quaternion.identity);

        newHealthParticles.transform.parent = transform;
    }

    private void DamageBuff_OnDamageAdd(object sender, DamageBuff.OnDamageAddEventArgs e) {
        ParticleSystem newDamageParticles = Instantiate(damageBuffParticleSystem, transform.position - new Vector3(0f, 1f, 0f), Quaternion.identity);
    
        newDamageParticles.transform.parent = transform;
    }

    private void OnDestroy() {
        DamageBuff.OnDamageAdd -= DamageBuff_OnDamageAdd;
        HealthBuff.OnHealthAdd -= HealthBuff_OnHealthAdd;
    }

}
