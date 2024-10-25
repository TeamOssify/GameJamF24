using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
        public string Description;

        public Event(string name, double SanityReq, int chance, int timeChange, int healthChange, int sanityChange, int moneyChange, string Description) {
            this.Name = name;
            this.Chance = chance;
            this.SanityReq = MAX_SANITY * SanityReq;
            this.TimeChange = timeChange;
            this.HealthChange = healthChange;
            this.SanityChange = sanityChange;
            this.MoneyChange = moneyChange;
            this.Description = Description;
        }
    }

    public Event[] mapRandomEvents = {
        new Event("Friend", 1, 60, 30, 0, 10, 0, "You hang out with your friend."),
        new Event("Beggar", 0.9, 40, 5, 0, 5, -1, "You gave a beggar a dollar."),
        new Event("Robbery", 0.7, 40, 1, 0, 5, -1, "Someone robbed you!"),
        new Event("Truck-Kun", 0.5, 10, 360, -80, -50, -10000,"Starting life in another world!"),
        new Event("Spooked", 0.3, 80, 10, -10, -20, 0,"The skinwalkers are rather lively today."),
        new Event("Panic", 0.2, 60, 30, -20, -30, 0, "You can't seem to keep yourself composed.."),
        new Event("Heart Attack", 0.1, 10, 360, -50, -20, 0, "You find yourself struggling to stand up.. Losing vision.. Cant... ")
    };

    public Event[] houseRandomEvents = {
        new Event("D2D Salesman", 0.9, 30, 15, 0, -5, -36,"A person knocked on your door. Something about vacuums? He was so convincing you just bought it."),
        new Event("Pests", 0.9, 50, 5, -1, -5, 0, "Your house is infested.."),
        new Event("Spooky Pests", 0.5, 60, 30, -5, -15, -15,"the bugs are in my skin"),
        new Event("Broken Chair", 0.4, 20, 60, -20, -20, -20,"You attempt to sit down on your chair, but the legs snapped off."),
        new Event("Spooked", 0.3, 80, 10, -10, -20, 0, "People keep knocking on my door.. what the hell is going on"),
        new Event("Panic", 0.2, 60, 30, -20, -30, 0, "You can't shake the feeling that something is wrong."),
        new Event("Heart Attack", 0.1, 10, 360, -50, -20, 0, "You find yourself struggling to stand up.. Losing vision.. Cant...")
    };

    public Event[] workRandomEvents = {
        new Event("Annoying Worker", 1, 70, 10, 0, -10, 0, "A coworker won't stop talking, and it's driving you crazy."),
        new Event("Broken Printer", 0.8, 50, 30, -1, -15, -50, "The office printer broke down again, Damn it..."),
        new Event("Extra Work", 0.9, 50, 60, 0, -5, 18, "You were assigned extra work, but it comes with a small bonus."),
        new Event("Nice Worker", 1, 70, 10, 0, 15, 0, "A kind coworker helped you out, making your day a bit easier."),
        new Event("Promotion", 1, 80, 0,0,15,30, "Nice! your boss noticed your hard work and promoted you."),
        new Event("Spooked", 0.3, 80, 10, -10, -20, 0, "I think my coworkers are out to get me. I cant trust anyone"),
        new Event("Panic", 0.2, 60, 30, -20, -30, 0, "rot rot rot rot rot rot rot"),
        new Event("Heart Attack", 0.1, 10, 360, -50, -20, 0, "You find yourself struggling to stand up.. Losing vision.. Cant...")
    };

    public Event[] clubRandomEvents = {
        new Event("Free Drink", 1, 30, 5, -5, 30, 0, "You received a free drink at the club. Cant complain about that!"),
        new Event("Drink", 1, 50, 5, -5, 30, -5, "You bought a drink at the club, enjoying the night."),
        new Event("Blackout", 0.5, 30, 180, -20, -20, -20, "You drank too much and blacked out, looks like you lost track of time..."),
        new Event("Kicked Out", 0.4, 10, 30, -5, -20, -15, "You were kicked out of the club for causing trouble with others at the bar."),
        new Event("Spooked", 0.3, 80, 10, -10, -20, 0, "I feel like someone is watching me.. I should get out of here"),
        new Event("Panic", 0.2, 60, 30, -20, -30, 0, "Someone poisoned my drink I know it. I know it. I know it."),
        new Event("Heart Attack", 0.1, 10, 360, -50, -20, 0, "You find yourself struggling to stand up.. Losing vision.. Cant...")
    };


    public void RandomEvent() {
        Debug.Log("Random Event has been attempted");
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
            Event chosen = new Event("", 1, 100, 0, 0, 0, 0,"");
            rng = UnityEngine.Random.Range(1, 100);
            Debug.Log($"Rng 2 is: " + rng);
            //Decides what event hapens
            for (int i = 0; i < randomEvents.Length; i++) {
                  //Current sanity is greater than the requirement for the even   
                if (SanityManager.Instance.Sanity <= randomEvents[i].SanityReq && rng <= randomEvents[i].Chance && randomEvents[i].Chance < chosen.Chance) {
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

        EventUIManager.Instance.DisplayEvent(input);

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