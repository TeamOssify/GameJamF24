using TMPro;
using UnityEngine;

public class DaysUI : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI dateText;

    private void OnEnable() {
        DateManager.Instance.DayChanged.AddListener(OnDayChanged);
        UpdateDateText(DateManager.Instance.CurrentDate, DateManager.Instance.CurrentDayOfWeek);
    }

    private void OnDisable() {
        DateManager.Instance.DayChanged.RemoveListener(OnDayChanged);
    }

    private void OnDayChanged(DateChangedArgs args) {
        UpdateDateText(args.Date, args.DayOfWeek);
    }

    private void UpdateDateText(int date, DayOfWeek dayOfWeek) {
        dateText.text = $"{dayOfWeek}, Day {date}";
    }
}