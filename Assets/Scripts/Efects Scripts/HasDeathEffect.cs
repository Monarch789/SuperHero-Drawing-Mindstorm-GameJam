using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasDeathEffect : MonoBehaviour{
    //reference of gameObject for IHasDeathEffect
    [SerializeField] private GameObject IHasDeathEffectGameObject;

    private IHasDeathEffect hasDeathEffect;

    //reference of Bllod splash death effect
    [SerializeField] private ParticleSystem bloodSplash;

    private void Awake() {
        if(IHasDeathEffectGameObject.TryGetComponent(out IHasDeathEffect _hasDeathEffect)) {
            hasDeathEffect = _hasDeathEffect;
        }
        else {
            Debug.LogError("No IHasDeathEffect on game Object" + IHasDeathEffectGameObject.name);
        }
    }

    private void Start() {
        hasDeathEffect.OnDeath += HasDeathEffect_OnDeath;
    }

    private void HasDeathEffect_OnDeath(object sender, System.EventArgs e) {
        Instantiate(bloodSplash,transform);

        //Destroy the object after sometime
        StartCoroutine(DestroyGameObjectCoroutine());
    }

    private IEnumerator DestroyGameObjectCoroutine() {
        yield return new WaitForSeconds(bloodSplash.main.duration/.25f);

        IHasDeathEffectGameObject.SetActive(false);
    }
}
