
using UnityEngine;

public class LoaderCallBack : MonoBehaviour{

    bool isFirstUpdate = false;

    private void Update() {
        if (!isFirstUpdate) {
            isFirstUpdate = true;

            //load the target scene
            Loader.LoaderCallBack();
        }
    }

}
