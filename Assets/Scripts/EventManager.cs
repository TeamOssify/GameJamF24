using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour {

    const double MAX_SANITY = 100000;
    public struct Event {
        public string Name;
        public double SanityReq;
        public int Chance;
        public int TimeChange;
        public int HealthChange;
        public int SanityChange;
        public int MoneyChange;

        public Event(string name, double SanityReq, int chance, int timeChange, int healthChange, int sanityChange, int moneyChange) {
            this.Name = name;
            this.Chance = chance;
            this.SanityReq = MAX_SANITY * SanityReq;
            this.TimeChange = timeChange;
            this.HealthChange = healthChange;
            this.SanityChange = sanityChange;
            this.MoneyChange = moneyChange;
        }
    }

    public Event[] mapRandomEvents = {
        new Event("map1", 0.9, 60, 30, 0, 10, 0),
        new Event("map2", 0.9, 20, 120, -10, -10, -50),
        new Event("map3", 0.9, 50, 1, 0, 5, -1),
        new Event("map4", 0.9, 10, 360, -80, -50, -10000)
    };

    public Event[] houseRandomEvents = {
        new Event("house1", 0.9, 60, 30, 0, 10, 0),
        new Event("house2", 0.9, 20, 120, -10, -10, -50),
        new Event("house3", 0.9, 50, 1, 0, 5, -1),
        new Event("house4", 0.9, 10, 360, -80, -50, -10000)
    };

    public Event[] workRandomEvents = {
        new Event("work1", 0.9, 60, 30, 0, 10, 0),
        new Event("work2", 0.9, 20, 120, -10, -10, -50),
        new Event("work3", 0.9, 50, 1, 0, 5, -1),
        new Event("work4", 0.9, 10, 360, -80, -50, -10000)
    };

    public Event[] clubRandomEvents = {
        new Event("club1", 0.9, 60, 30, 0, 10, 0),
        new Event("club2", 0.9, 20, 120, -10, -10, -50),
        new Event("club3", 0.9, 50, 1, 0, 5, -1),
        new Event("club4", 0.9, 10, 360, -80, -50, -10000)
    };

    public void RandomEvent() {
        Debug.Log("Random Event has been attempt");
        int rng = UnityEngine.Random.Range(1, 100);
        Debug.Log($"Rng 1 is: " + rng);
        //Chance of an event to happen
        if (rng <= 50) {
            //selects which scene to choose from
            Event[] randomEvents = mapRandomEvents;
            Scene scene = SceneManager.GetActiveScene();
            switch (scene.name) {
                case "HouseLocation":
                    randomEvents = mapRandomEvents;
                    break;
                case "WorkLocation":
                    randomEvents = workRandomEvents;
                    break;
                case "ClubLocation":
                    randomEvents = clubRandomEvents;
                    break;
                default:
                    randomEvents = mapRandomEvents;
                    break;
            }
            //if no event is chosen nothing can happen
            Event chosen = new Event("", 1, 100, 0, 0, 0, 0);
            rng = UnityEngine.Random.Range(1, 100);
            Debug.Log($"Rng 2 is: " + rng);
            //Decides what event hapens
            for (int i = 0; i < randomEvents.Length; i++) {
                  //Current sanity is greater than the requirement for the even   
                if (SanityManager.Instance.Sanity >= randomEvents[i].SanityReq && rng <= randomEvents[i].Chance && randomEvents[i].Chance < chosen.Chance) {
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