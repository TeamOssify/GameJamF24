using UnityEngine;
using UnityEngine.EventSystems;

public sealed class HoverSoundComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField]
    private AudioClip hoverEnterSound;

    [SerializeField]
    [Range(0, 1)]
    private float hoverEnterVolume = 1f;

    [SerializeField]
    private AudioClip hoverExitSound;

    [SerializeField]
    [Range(0, 1)]
    private float hoverExitVolume = 1f;

    public void OnPointerEnter(PointerEventData eventData) {
        if (hoverEnterSound) {
            SoundEffectManager.Instance.PlaySoundEffect(hoverEnterSound, hoverEnterVolume);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (hoverExitSound) {
            SoundEffectManager.Instance.PlaySoundEffect(hoverExitSound, hoverExitVolume);
        }
    }
}