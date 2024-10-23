using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    public struct Event {
        public string Name;
        public int Chance;
        public int TimeChange;
        public int HealthChange;
        public int SanityChange;
        public int MoneyChange;

        public Event(string name, int chance, int timeChange, int healthChange, int sanityChange, int moneyChange) {
            this.Name = name;
            this.Chance = chance;
            this.TimeChange = timeChange;
            this.HealthChange = healthChange;
            this.SanityChange = sanityChange;
            this.MoneyChange = moneyChange;
        }
    }

    public Event[] randomEvents = {
        new Event("friend", 60, 30, 0, 10, 0),
        new Event("robery", 20, 120, -10, -10, -50),
        new Event("beggar", 50, 1, 0, 5, -1),
        new Event("truck", 10, 360, -80, -50, -10000)
    };

    public void RandomEvent() {
        Debug.Log("Random Event has been attempt");
        int rng = UnityEngine.Random.Range(1, 100);
        Debug.Log($"Rng 1 is: " + rng);
        //Chance of an event to happen
        if (rng <= 50) {
            //if no event is chosen nothing can happen
            Event chosen = new Event("", 100, 0, 0, 0, 0);
            rng = UnityEngine.Random.Range(1, 100);
            Debug.Log($"Rng 2 is: " + rng);
            //Decides what event hapens
            for (int i = 0; i < randomEvents.Length; i++) {
                if (randomEvents[i].Chance > rng && randomEvents[i].Chance < chosen.Chance) {
                    chosen = randomEvents[i];
                }
            }
            //if an event is chosen it then happens
            if (chosen.Name != "") {
                DoEvent(chosen);
            }
        }
    }

    public void DoEvent(Event input) {
        Debug.Log($"" + input.Name + " has happened");
        TimeManager.Instance.AdvanceTimeOfDay(TimeSpan.FromMinutes(input.TimeChange));
        if (input.HealthChange < 0) {
            HealthManager.Instance.Damage(input.HealthChange * -1);
        }
        if (input.HealthChange > 0) {
            HealthManager.Instance.Heal(input.HealthChange);
        }
        if (input.SanityChange < 0) {
            SanityManager.Instance.ReduceSanity(input.SanityChange * -1);
        }
        if (input.SanityChange > 0) {
            SanityManager.Instance.IncreaseSanity(input.SanityChange);
        }
        if (input.MoneyChange < 0) {
            BankAccountManager.Instance.RemoveFunds(input.MoneyChange * -1);
        }
        if (input.MoneyChange > 0) {
            BankAccountManager.Instance.AddFunds(input.MoneyChange);
        }
    }
}