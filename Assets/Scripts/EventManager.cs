using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class EventManager : MonoBehaviour {
    [SerializeField]
    private LocationManager locationManager;

    [Header("Event dialogue sprites")]
    [SerializeField]
    private Sprite Bartender;

    [SerializeField]
    private Sprite Coworker;

    [SerializeField]
    private Sprite Stranger1;

    [SerializeField]
    private Sprite Stranger2;

    [SerializeField]
    private Sprite Stranger3;

    [SerializeField]
    private Sprite Stranger4;

    [SerializeField]
    private Sprite Friend;

    [SerializeField]
    private Sprite TruckKun;

    [SerializeField]
    private Sprite PlagueDoctor;

    [SerializeField]
    private Sprite Doctor;

    public sealed record Event {
        public string Name { get; }
        public float PercentSanityRequired { get; }
        public int Chance { get; }
        public TimeSpan TimeChange { get; }
        public float HealthChange { get; }
        public float SanityChange { get; }
        public decimal MoneyChange { get; }
        public string Description { get; }
        public Sprite Speaker { get; }

        public Event(string name, double percentSanityRequired, int chance, TimeSpan timeChange, float healthChange, float sanityChange, decimal moneyChange, string description, Sprite speaker) {
            Name = name;
            Chance = chance;
            PercentSanityRequired = (float)percentSanityRequired;
            TimeChange = timeChange;
            HealthChange = healthChange;
            SanityChange = sanityChange;
            MoneyChange = moneyChange;
            Description = description;
            Speaker = speaker;
        }
    }

    private Event[] _mapRandomEvents;
    private Event[] _houseRandomEvents;
    private Event[] _workRandomEvents;
    private Event[] _clubRandomEvents;
    private Event[] _hospitalRandomEvents;

    private void Awake() {
        _mapRandomEvents = new[] {
            new Event("Friend Hangout", 1, 60, TimeSpan.FromMinutes(30), 0, 10, 0, "You hang out with your friend.", Friend),
            new Event("Beggar", 0.9, 40, TimeSpan.FromMinutes(5), 0, 5, -1, "You gave a beggar a dollar.", Stranger2),
            new Event("Robbery", 0.7, 40, TimeSpan.FromMinutes(1), 0, 5, -1, "Someone robbed you!", Stranger4),
            new Event("Truck-Kun", 0.5, 10, TimeSpan.FromMinutes(360), -80, -50, -10000, "Starting life in another world!", TruckKun),
            new Event("Spooked", 0.3, 80, TimeSpan.FromMinutes(10), -10, -20, 0, "The skinwalkers are rather lively today.", Stranger3),
            new Event("Panic", 0.2, 60, TimeSpan.FromMinutes(30), -20, -30, 0, "You can't seem to keep yourself composed..", Stranger3),
            new Event("Heart Attack", 0.1, 10, TimeSpan.FromMinutes(360), -50, -20, 0, "You find yourself struggling to stand up.. Losing vision.. Cant... ", Stranger3)
        };

        _houseRandomEvents = new[] {
            new Event("D2D Salesman", 1, 30, TimeSpan.FromMinutes(15), 0, -5, -36, "A person knocked on your door. Something about vacuums? She was so convincing you just bought it.", Stranger1),
            new Event("Pests", 1, 50, TimeSpan.FromMinutes(5), -1, -5, 0, "Your house is infested..", Stranger3),
            new Event("Spooky Pests", 0.2, 60, TimeSpan.FromMinutes(30), -5, -15, -15, "the bugs are in my skin", Stranger3),
            new Event("Broken Chair", 1, 20, TimeSpan.FromMinutes(60), -20, -20, -20, "You attempt to sit down on your chair, but the legs snapped off.", Stranger3),
            new Event("Spooked", 0.4, 80, TimeSpan.FromMinutes(10), -10, -20, 0, "People keep knocking on my door.. what the hell is going on", Stranger3),
            new Event("Panic", 0.2, 60, TimeSpan.FromMinutes(30), -20, -30, 0, "You can't shake the feeling that something is wrong.", Stranger3),
            new Event("Heart Attack", 0.1, 10, TimeSpan.FromMinutes(360), -50, -20, 0, "You find yourself struggling to stand up.. Losing vision.. Cant...", Stranger3),
            new Event("Friend Visit", 1, 30, TimeSpan.FromMinutes(90), 0, 10, 0, "Your friend comes over to visit! Nice.", Friend)
        };

        _workRandomEvents = new[] {
            new Event("Annoying Worker", 1, 70, TimeSpan.FromMinutes(10), 0, -10, 0, "A coworker won't stop talking, and it's driving you crazy.", Coworker),
            new Event("Broken Printer", 1, 50, TimeSpan.FromMinutes(30), -1, -15, -50, "The office printer broke down again, Damn it...", Coworker),
            new Event("Extra Work", 1, 50, TimeSpan.FromMinutes(60), 0, -5, 18, "You were assigned extra work, but it comes with a small bonus.", Coworker),
            new Event("Nice Worker", 1, 70, TimeSpan.FromMinutes(10), 0, 15, 0, "A kind coworker helped you out, making your day a bit easier.", Coworker),
            new Event("Promotion", 1, 80, TimeSpan.FromMinutes(0), 0, 15, 30, "Nice! your boss noticed your hard work and promoted you.", Coworker),
            new Event("Spooked", 0.4, 80, TimeSpan.FromMinutes(10), -10, -20, 0, "I think my coworkers are out to get me. I cant trust anyone", Coworker),
            new Event("Panic", 0.3, 60, TimeSpan.FromMinutes(30), -20, -30, 0, "rot rot rot rot rot rot rot", PlagueDoctor),
            new Event("Heart Attack", 0.1, 10, TimeSpan.FromMinutes(360), -50, -20, 0, "You find yourself struggling to stand up.. Losing vision.. Cant...", Stranger3)
        };

        _clubRandomEvents = new[] {
            new Event("Free Drink", 1, 30, TimeSpan.FromMinutes(5), -5, 30, 0, "You received a free drink at the club. Cant complain about that!", Stranger4),
            new Event("Drink", 1, 50, TimeSpan.FromMinutes(5), -5, 30, -5, "You bought a drink at the club, enjoying the night.", Stranger3),
            new Event("Blackout", 0.5, 30, TimeSpan.FromMinutes(180), -20, -20, -20, "You drank too much and blacked out, looks like you lost track of time...", Stranger3),
            new Event("Kicked Out", 0.4, 10, TimeSpan.FromMinutes(30), -5, -20, -15, "You were kicked out of the club for causing trouble with others at the bar.", Stranger3),
            new Event("Spooked", 0.3, 80, TimeSpan.FromMinutes(10), -10, -20, 0, "I feel like someone is watching me.. I should get out of here", Stranger3),
            new Event("Panic", 0.2, 60, TimeSpan.FromMinutes(30), -20, -30, 0, "Someone poisoned my drink I know it. I know it. I know it.", PlagueDoctor),
            new Event("Heart Attack", 0.1, 10, TimeSpan.FromMinutes(360), -50, -20, 0, "You find yourself struggling to stand up.. Losing vision.. Cant...", Stranger3)
        };

        _hospitalRandomEvents = new[] {
            new Event("Flirty Nurse", 1, 50, TimeSpan.FromMinutes(20), 0, 25, 0, "A nurse was complimenting you. You find yourself with a smile on your face.", Doctor),
            new Event("Witness", 1, 30, TimeSpan.FromMinutes(5), 0, -25, 0, "You witnessed sad news being broken to a family. You cant help but feel bad for them..", Stranger1),
            new Event("Lord of Rot", 0.1, 25, TimeSpan.FromMinutes(60), -10, -10, -10, "The Lord of Rot visited me today. I gave him the daily sacrifices.", PlagueDoctor),
            new Event("Insurance Clutch", 1, 60, TimeSpan.FromMinutes(0), 0, 10, 100, "Your health insurance came through. Thats a little extra money in your pocket.", Doctor),
            new Event("Patched Up", 1, 50, TimeSpan.FromMinutes(60), 30, 30, -20, "A nurse was available and quickly patched you up for free. Nice!", Doctor),
            new Event("Botched Job", 1, 30, TimeSpan.FromMinutes(30), -20, -10, 0, "Damn it! They screwed something up during my treatment..", Doctor),
            new Event("Spooked", 0.3, 80, TimeSpan.FromMinutes(10), -10, -20, 0, "The hospital walls are eternally shifting.. i cant seem to find my way around", Stranger3),
            new Event("Panic", 0.2, 60, TimeSpan.FromMinutes(30), -20, -30, 0, "the voices", PlagueDoctor),
            new Event("Heart Attack", 0.1, 10, TimeSpan.FromMinutes(360), -50, -20, 0, "You find yourself struggling to stand up.. Losing vision.. Cant...", Stranger3),
        };
    }

    public void RandomEvent() {
        Debug.Log("A random event is being attempted.");

        //Chance of an event to happen
        if (Random.Range(1, 100) > 50) {
            Debug.Log("Rolled > 50 - skipping event.");
            return;
        }

        //selects which scene to choose from
        var randomEvents = locationManager.CurrentLocation switch {
            Location.House => _houseRandomEvents,
            Location.Work => _workRandomEvents,
            Location.Club => _clubRandomEvents,
            Location.Map => _mapRandomEvents,
            Location.Hospital => _hospitalRandomEvents,
            _ => Array.Empty<Event>()
        };

        var sanityManager = SanityManager.Instance;
        var currentSanityPercent = sanityManager.Sanity / sanityManager.MaxSanity;

        // First filter by sanity requirement
        var validEvents = randomEvents
            .Where(x => currentSanityPercent < x.PercentSanityRequired)
            .ToArray();

        if (validEvents.Length == 0) {
            Debug.Log("No events available for sanity level");
            return;
        }

        // Then randomly select events that pass their chance check
        var possibleEvents = validEvents
            .Where(x => Random.Range(1, 100) <= x.Chance)
            .ToArray();

        if (possibleEvents.Length == 0) {
            Debug.Log("No events passed the chance check");
            return;
        }

        // Select a random event from the possible events
        var chosenEvent = possibleEvents[Random.Range(0, possibleEvents.Length)];

        RunEvent(chosenEvent);
    }

    public void RunEvent(Event input) {
        Debug.Log($"Event \"{input.Name}\" has happened!");

        if (!DontDestroySingleton.TryGetInstance("EventUIManager", out var eventUIManagerObj)
            || !eventUIManagerObj.TryGetComponent(out EventUIManager eventUIManager)) {
            Debug.LogError("Couldn't get EventUIManager.");
            return;
        }

        eventUIManager.DisplayEvent(input);

        TimeManager.Instance.AdvanceTimeOfDay(input.TimeChange);

        if (input.HealthChange < 0) {
            HealthManager.Instance.Damage(input.HealthChange * -1);
        }
        else if (input.HealthChange > 0) {
            HealthManager.Instance.Heal(input.HealthChange);
        }

        if (input.SanityChange < 0) {
            SanityManager.Instance.ReduceSanity(input.SanityChange * -1);
        }
        else if (input.SanityChange > 0) {
            SanityManager.Instance.IncreaseSanity(input.SanityChange);
        }

        if (input.MoneyChange < 0) {
            BankAccountManager.Instance.RemoveFunds(input.MoneyChange * -1);
            ExpensesManager.Instance.AddExpense(input.Name, (float)input.MoneyChange);
        }
        else if (input.MoneyChange > 0) {
            BankAccountManager.Instance.AddFunds(input.MoneyChange);
        }
    }
}