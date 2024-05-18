using System;
using System.Collections.Generic;

namespace EventManagementSystem
{
    class Program
    {
        static List<Event> events = new List<Event>(); // A list to store the events

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\nEvent Management System");
                Console.WriteLine("1. Add Event");
                Console.WriteLine("2. List Events");
                Console.WriteLine("3. Search Event");
                Console.WriteLine("4. Exit");
                Console.Write("Select an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        AddEvent(); // Call the AddEvent method when the user selects option 1
                        break;
                    case "2":
                        ListEvents(); // Call the ListEvents method when the user selects option 2
                        break;
                    case "3":
                        SearchEvent(); // Call the SearchEvent method when the user selects option 3
                        break;
                    case "4":
                        return; // Exit the program when the user selects option 4
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void AddEvent()
        {
            try
            {
                Console.Write("Enter event name: ");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentException("Event name cannot be empty.");
                }

                if (events.Exists(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new ArgumentException("An event with this name already exists.");
                }

                Console.Write("Enter event date (yyyy-mm-dd): ");
                DateTime date;
                while (!DateTime.TryParse(Console.ReadLine(), out date))
                {
                    Console.WriteLine("Invalid date format. Please try again.");
                }

                if (events.Exists(e => e.Date == date))
                {
                    throw new ArgumentException("An event is already scheduled on this date.");
                }

                Console.Write("Enter event location: ");
                string location = Console.ReadLine();

                Console.Write("Enter event description: ");
                string description = Console.ReadLine();

                // Create a new Event object and add it to the events list
                events.Add(new Event { Name = name, Date = date, Location = location, Description = description });
                Console.WriteLine("Event added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void ListEvents()
        {
            if (events.Count == 0)
            {
                Console.WriteLine("No events found.");
                return;
            }

            Console.WriteLine("\nList of Events:");
            foreach (var ev in events)
            {
                // Display the details of each event
                Console.WriteLine($"Name: {ev.Name}, Date: {ev.Date.ToShortDateString()}, Location: {ev.Location}, Description: {ev.Description}");
            }
        }

        static void SearchEvent()
        {
            try
            {
                Console.Write("Enter event name to search: ");
                string name = Console.ReadLine();
                var foundEvents = events.FindAll(e => e.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

                if (foundEvents.Count == 0)
                {
                    Console.WriteLine("No events found.");
                    return;
                }

                Console.WriteLine("\nSearch Results:");
                foreach (var ev in foundEvents)
                {
                    // Display the details of each found event
                    Console.WriteLine($"Name: {ev.Name}, Date: {ev.Date.ToShortDateString()}, Location: {ev.Location}, Description: {ev.Description}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    class Event
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
    }
}