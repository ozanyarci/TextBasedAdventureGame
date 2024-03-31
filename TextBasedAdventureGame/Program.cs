using System;
using System.Collections.Generic;

namespace TextAdventureGame
{
    class Program
    {
        static void Main(string[] args)
        {
            bool playAgain = true;

            while (playAgain)
            {
                Console.WriteLine("Welcome to the Text Adventure Game!");
                Console.WriteLine("Your objective is to escape the house by finding the key and unlocking the study drawer.");
                Console.WriteLine("You wake up in a dark room with no memory of how you got there.");

                Dictionary<string, string> rooms = new Dictionary<string, string>
                {
                    { "dark room", "You are in a dark room with a door to the north. There is a small table with a candle on it." },
                    { "hallway", "You are in a dimly lit hallway with doors to the east, west, and south. The door to the south is locked." },
                    { "kitchen", "You are in a kitchen. There is a knife on the counter and a locked cabinet." },
                    { "bedroom", "You are in a bedroom. There is a bed and a wardrobe. The wardrobe is locked." },
                    { "study", "You are in a study. There is a desk with a locked drawer and a bookshelf." }
                };

                string currentRoom = "dark room";
                bool hasKnife = false;
                bool hasKey = false;
                bool candleLit = false;
                bool gameWon = false;

                while (true)
                {
                    string availableCommands = "";
                    if (currentRoom == "dark room" && !candleLit)
                    {
                        availableCommands = "\nAvailable commands: light candle, restart, exit";
                    }
                    else if (currentRoom == "dark room" && candleLit)
                    {
                        availableCommands = "\nAvailable commands: go north, restart, exit";
                    }
                    else if (currentRoom == "hallway")
                    {
                        availableCommands = "\nAvailable commands: go east, go west, restart, exit";
                        if (hasKey)
                        {
                            availableCommands += ", go south";
                        }
                    }
                    else if (currentRoom == "kitchen" && !hasKnife)
                    {
                        availableCommands = "\nAvailable commands: take knife, go west, restart, exit";
                    }
                    else if (currentRoom == "kitchen" && hasKnife)
                    {
                        availableCommands = "\nAvailable commands: unlock cabinet (if you have the key), go west, restart, exit";
                    }
                    else if (currentRoom == "bedroom")
                    {
                        availableCommands = "\nAvailable commands: use knife (if you have the knife), unlock wardrobe (if you have the key), go east, restart, exit";
                    }
                    else if (currentRoom == "study")
                    {
                        availableCommands = "\nAvailable commands: search bookshelf, unlock drawer (if you have the key), restart, exit";
                    }

                    Console.WriteLine("\n" + rooms[currentRoom] + availableCommands);
                    Console.Write("> ");
                    string input = Console.ReadLine().ToLower();

                    if (input == "restart")
                    {
                        break;
                    }
                    else if (input == "exit")
                    {
                        playAgain = false;
                        break;
                    }
                    else if (input == "light candle" && currentRoom == "dark room" && !candleLit)
                    {
                        candleLit = true;
                        Console.WriteLine("You light the candle, illuminating the room.");
                    }
                    else if (input == "go north" && currentRoom == "dark room" && candleLit)
                    {
                        currentRoom = "hallway";
                    }
                    else if (input == "go east" && currentRoom == "hallway")
                    {
                        currentRoom = "kitchen";
                    }
                    else if (input == "go west" && currentRoom == "hallway")
                    {
                        currentRoom = "bedroom";
                    }
                    else if (input == "go south" && currentRoom == "hallway" && hasKey)
                    {
                        currentRoom = "study";
                    }
                    else if (input == "take knife" && currentRoom == "kitchen" && !hasKnife)
                    {
                        hasKnife = true;
                        Console.WriteLine("You take the knife.");
                    }
                    else if (input == "unlock cabinet" && currentRoom == "kitchen" && hasKey)
                    {
                        Console.WriteLine("You unlock the cabinet and find a note inside: 'The key to the wardrobe is hidden in the study.'");
                    }
                    else if (input == "use knife" && hasKnife && currentRoom == "bedroom")
                    {
                        Console.WriteLine("You use the knife to break open the wardrobe.");
                        hasKey = true;
                        Console.WriteLine("You find a key inside the wardrobe.");
                    }
                    else if (input == "unlock wardrobe" && currentRoom == "bedroom" && hasKey)
                    {
                        Console.WriteLine("You unlock the wardrobe, but it's empty.");
                    }
                    else if (input == "go east" && currentRoom == "bedroom")
                    {
                        currentRoom = "hallway";
                    }
                    else if (input == "go west" && currentRoom == "kitchen")
                    {
                        currentRoom = "hallway";
                    }
                    else if (input == "search bookshelf" && currentRoom == "study")
                    {
                        Console.WriteLine("You search the bookshelf and find a hidden compartment behind one of the books.");
                        Console.WriteLine("Inside the compartment, you find the key to the locked drawer.");
                        hasKey = true;
                    }
                    else if (input == "unlock drawer" && currentRoom == "study" && hasKey)
                    {
                        Console.WriteLine("You unlock the drawer and find a message inside: 'Congratulations! You've escaped the house.'");
                        Console.WriteLine("Thanks for playing!");
                        gameWon = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid command.");
                    }
                }

                if (gameWon)
                {
                    Console.Write("Would you like to play again? (y/n) ");
                    string playAgainInput = Console.ReadLine().ToLower();
                    playAgain = playAgainInput == "y";
                }
                else if (!playAgain)
                {
                    break;
                }
                else
                {
                    Console.Write("Would you like to play again? (y/n) ");
                    string playAgainInput = Console.ReadLine().ToLower();
                    playAgain = playAgainInput == "y";
                }
            }

            Console.WriteLine("Goodbye!");
            Console.ReadLine();
        }
    }
}