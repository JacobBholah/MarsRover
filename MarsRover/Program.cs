using System;
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
                    Rover[] data = Rover.readRoverData();
                    Console.WriteLine($"There is currently {data.Length} Rover/s deployed");

                    for (int i = 0; i < data.Length; i++)
                    {
                        Console.WriteLine($"{data[i].Name} is currently positioned at {data[i].X} {data[i].Y} facing {data[i].Direction}");
                    }

                    Rover.roverDeployment(data);
                    x = Rover.repeatProgram(x);
                }

                catch (Exception e) when (e is FileNotFoundException || e is NullReferenceException)
                {
                    Console.WriteLine("No Previous Data being held");
                    Rover[] data = new Rover[0];
                    Rover.roverDeployment(data);
                    x = Rover.repeatProgram(x);
                }
            }
            Console.WriteLine("END OF PROGRAM");
        }
    }
}