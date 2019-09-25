﻿using System;
using System.Collections.Generic;

namespace Zork
{
    class Program
    {

        private static (int Row, int Column) Location = (1,1);
        private static string CurrentRoom
        {
            get
            {
                return Rooms[Location.Row, Location.Column];
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            Commands command = Commands.UNKNOWN;
            while (command != Commands.QUIT)
            {
                Console.Write("\n> ");
                command = ToCommand(Console.ReadLine().Trim());

                string outputString = "";
                switch (command)
                {
                    case Commands.LOOK:
                        //outputString = "This is an open field west of a white house, with a boarded front door. \n A rubber mat saying 'Welcome to Zork!' lies by the door.\n";
                        break;
                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        if(Move(command) == false)
                        {
                            Console.WriteLine("The way is shut!");
                        }
                        else
                        {
                            Console.WriteLine($"You moved {command}");
                        }
                            
                        break;
                    case Commands.QUIT:
                        outputString = "Thank you for playing";
                        break;

                    default:
                        outputString = "Unknown Command\n";
                        break;
                }

                Console.WriteLine(outputString);
                Console.Write(CurrentRoom);
            }
        }

        private static Commands ToCommand(string commandString) => Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN;
        private static bool IsDirection(Commands command) => Directions.Contains(command);
        private static readonly string[,] Rooms = {
            { "Rock Trail", "South of House", "Canyon View" },
            { "Forest", "West of House", "Behind House" },
            { "Dense Woods", "North of House", "Clearing" }
        };

        private static Boolean Move(Commands command)
        {
            Assert.IsTrue(IsDirection(command), "Invalid direction");
            bool isValidMove = true;
            switch (command)
            {
                case Commands.NORTH when Location.Row > 0:
                Location.Row--;
                break;

                case Commands.SOUTH when Location.Row < Rooms.GetLength(0) - 1:
                Location.Row++;
                break;

                case Commands.EAST when Location.Column < Rooms.GetLength(1) - 1:
                Location.Column++;
                break;

                case Commands.WEST when Location.Column > 0:
                Location.Column--;
                break;

                default:
                    isValidMove = false;
                    break;
            }

            return isValidMove;
        }

        private static readonly List<Commands> Directions = new List<Commands>
        {
                Commands.NORTH,
                Commands.SOUTH,
                Commands.EAST,
                Commands.WEST
        };
    }
}
