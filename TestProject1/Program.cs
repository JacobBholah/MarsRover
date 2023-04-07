using System;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("How many Rovers will you be deploying");
            string numOfRovers = Console.ReadLine();
            Rover[] data = new Rover[int.Parse(numOfRovers.ToString())];

            //////set dimensions of the plateau
            Plateau plateau = new Plateau();
            plateau=plateau.setPlateauDimensions(plateau);

            for(int i=0; i<data.Length; i++)
            {
                Rover rover = new Rover($"Rover{i+1}");            
                /////set the position of rovers
                rover=rover.setRoverPosition(rover);
                //push the data into correct array spot
                data[i] = rover;
                ////movement of rover
                rover = rover.validateRoverMovementCommand(rover,plateau);
                //push the rover data into the array
                data[i] = rover;

                Console.WriteLine($"Executing movement of {rover.getName()}");
                Console.WriteLine($"new position of {rover.getName()} is {rover.getX()} {rover.getY()} {rover.getDirection()}");
            }
            Console.WriteLine("END OF PROGRAM");
        }
    }
}