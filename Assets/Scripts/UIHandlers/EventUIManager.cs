using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventUIManager : MonoBehaviour {
    [SerializeField]
    private Canvas eventUI;

    [SerializeField]
    private Image eventImage;

    [SerializeField]
    private TextMeshProUGUI eventTitle;

    [SerializeField]
    private TextMeshProUGUI eventSanity;

    [SerializeField]
    private TextMeshProUGUI eventHealth;

    [SerializeField]
    private TextMeshProUGUI eventMoney;

    [SerializeField]
    private TextMeshProUGUI eventDesc;

    [SerializeField]
    private Button eventClose;

    [SerializeField]
    private TextMeshProUGUI eventTimePassed;

    private void Awake() {
        eventUI.enabled = false;

        eventClose.onClick.AddListener(CloseEventUI);
    }

    public void DisplayEvent(EventManager.Event eventData) {
        eventUI.enabled = true;

        eventTitle.text = eventData.Name;
        eventDesc.text = eventData.Description;
        eventTimePassed.text = $"{eventData.TimeChange.TotalMinutes} minutes have passed.";

        eventSanity.text = FormatChange(eventData.SanityChange);
        eventHealth.text = FormatChange(eventData.HealthChange);
        eventMoney.text = FormatChange((float)eventData.MoneyChange);

        if (eventData.Speaker) {
            eventImage.sprite = eventData.Speaker;
        }
    }

    private static string FormatChange(float change) {
        if (Mathf.Approximately(change, 0)) {
            return "=";
        }

        var sign = change > 0 ? "+" : "-";
        return sign;
    }

    private void CloseEventUI() {
        eventUI.enabled = false;
    }
}