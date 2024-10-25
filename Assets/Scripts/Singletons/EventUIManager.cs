using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventUIManager : MonoBehaviour {
    private static EventUIManager _instance;
    public static EventUIManager Instance {
        get {
            if (_instance == null) {
                if (DontDestroySingleton.TryGetInstance("EventUI", out GameObject instanceObj)) {
                    _instance = instanceObj.GetComponent<EventUIManager>();
                }
            }
            return _instance;
        }
    }

    [SerializeField] private Canvas _eventUI;
    [SerializeField] private Image _eventImage;
    [SerializeField] private TextMeshProUGUI _eventTitle;
    [SerializeField] private TextMeshProUGUI _eventSanity;
    [SerializeField] private TextMeshProUGUI _eventHealth;
    [SerializeField] private TextMeshProUGUI _eventMoney;
    [SerializeField] private TextMeshProUGUI _eventDesc;
    [SerializeField] private Button _eventClose;
    [SerializeField] private TextMeshProUGUI _eventTimePassed;

    private void Awake() {
        _instance = this;

        _eventUI.enabled = false;

        _eventClose.onClick.AddListener(CloseEventUI);
    }

    public void DisplayEvent(EventManager.Event eventData) {
        _eventUI.enabled = true;

        _eventTitle.text = eventData.Name;
        _eventDesc.text = eventData.Description;
        _eventTimePassed.text = $"{eventData.TimeChange} minutes have passed";

        _eventSanity.text = FormatChange("Sanity", eventData.SanityChange);
        _eventHealth.text = FormatChange("Health", eventData.HealthChange);
        _eventMoney.text = FormatChange("Money", eventData.MoneyChange);
    }

    private string FormatChange(string stat, int change) {
        if (change == 0)
            return $"0";

        string sign = change > 0 ? "+" : "-";
        return $"{sign}";
    }

    private void CloseEventUI() {
        _eventUI.enabled = false;
    }
}