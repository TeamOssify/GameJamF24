using UnityEngine;
using UnityEngine.EventSystems;

public sealed class ClickSoundComponent : MonoBehaviour, IPointerClickHandler {
    [SerializeField]
    private AudioClip clickSound;

    [SerializeField]
    [Range(0, 1)]
    private float volume = 1f;

    public void OnPointerClick(PointerEventData eventData) {
        if (clickSound && eventData.button == PointerEventData.InputButton.Left) {
            SoundEffectManager.Instance.PlaySoundEffect(clickSound, volume);
        }
    }
}