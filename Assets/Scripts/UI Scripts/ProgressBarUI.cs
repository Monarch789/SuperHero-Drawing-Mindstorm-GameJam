
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour{

    [SerializeField] private GameObject HasProgressGameObject;

    private IHasProgress hasProgress;

    // reference of image
    [SerializeField] private Image barImage;

    private void Awake() {
        //get the reference from the gameObject
        if (HasProgressGameObject.TryGetComponent(out IHasProgress _hasProgress)) {
            hasProgress = _hasProgress;
        }
        else {
            Debug.LogError("No IHasProgress on the gameObject");
        }
    }
    private void Start() {
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
       
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangeEventAgs e) {
        barImage.fillAmount = e.progressAmount;
    }

    private void OnDestroy() {
        hasProgress.OnProgressChanged -= HasProgress_OnProgressChanged;

    }
}
