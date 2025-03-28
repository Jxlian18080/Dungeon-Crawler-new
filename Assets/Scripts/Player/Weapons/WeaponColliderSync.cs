using UnityEngine;

public class WeaponColliderSync : MonoBehaviour {

    [SerializeField] string objectToFind;
    private Transform bone;

    void Start() {
        bone = GameObject.Find(objectToFind)?.transform;

        if (bone == null) {
            Debug.LogError(objectToFind + "-Bone nicht gefunden! Stelle sicher, dass der Name korrekt ist.");
        }
    }

    void LateUpdate() {
        if (bone != null) {
            transform.SetPositionAndRotation(bone.position, bone.rotation);
        }
    }
}
