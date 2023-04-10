using System;
using System.Collections.Generic;
using System.IO;

namespace MarsRover
{
    class Program
    {
        private static bool x = true;

        static void Main(string[] args)
        {
            while (x == true)
            {
                try
                {
                    List<Rover> data = Rover.readRoverData();
                    Console.WriteLine($"There is currently {data.Count} Rover/s deployed");

                    for (int i = 0; i < data.Count; i++)
                    {
                        Console.WriteLine($"{data[i].Name} is currently positioned at {data[i].X} {data[i].Y} facing {data[i].Direction}");
                    }
                    Rover.adjustRoversQuestion(data);
                    Rover.additionalRoversQuestion(data);
                    x = Rover.repeatProgram(x);
                }

                catch (Exception e) when (e is FileNotFoundException || e is NullReferenceException)
                {
                    Console.WriteLine("No Previous Data being held");
                    List<Rover> data = new List<Rover>();
                    Rover.roverDeployment(data);
                    x = Rover.repeatProgram(x);
                }
            }
            Console.WriteLine("END OF PROGRAM");
        }
    }
}