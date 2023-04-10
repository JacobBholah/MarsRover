using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;


namespace MarsRover
{
    public class Rover
    {
        /////attributes////
        public string Name;
        public int X;
        public int Y;
        public char Direction;

        ///constructors/////
        public Rover(int x, int y, char direction)
        {
            this.X = x;
            this.Y = y;
            this.Direction = direction;
        }

        public Rover(string name)
        {
            this.Name = name;
        }

        public Rover() { }
        ////methods////
        public string getName()
        {
            return Name;
        }
        public void setName(string name)
        {
            this.Name = name;
        }
        public int getX()
        {
            return X;
        }
        public void setX(int x)
        {
            this.X = x;
        }

        public int getY()
        {
            return Y;
        }
        public void setY(int y)
        {
            this.Y = y;
        }
        public char getDirection()
        {
            return Direction;
        }
        public void setDirection(char direction)
        {
            this.Direction = direction;
        }

        public void rotateL(char direction)
        {
            switch (direction)
            {
                case 'N':
                    setDirection('W');
                    break;

                case 'E':
                    setDirection('N');
                    break;

                case 'S':
                    setDirection('E');
                    break;

                case 'W':
                    setDirection('S');
                    break;
            }
        }
        public void rotateR(char direction)
        {
            switch (direction)
            {
                case 'N':
                    setDirection('E');
                    break;

                case 'E':
                    setDirection('S');
                    break;

                case 'S':
                    setDirection('W');
                    break;

                case 'W':
                    setDirection('N');
                    break;
            }

        }
        public void moveForward(char direction)
        {
            switch (direction)
            {
                case 'N':
                    setY(getY() + 1);
                    break;

                case 'E':
                    setX(getX() + 1);
                    break;

                case 'S':
                    setY(getY() - 1);
                    break;

                case 'W':
                    setX(getX() - 1);
                    break;
            }
        }
        public Rover setRoverPosition(Rover rover)
        {
            Console.WriteLine($"Please provide the starting point for {rover.getName()}");
            string roverStartingPos = Console.ReadLine();

            if (roverStartingPos.Length != 5)
            {
                Console.WriteLine("Rover1 starting position provided is invalid, it should be in the format of x y Direction i.e. 1 2 N ");
                rover.setRoverPosition(rover);
            }
            else
            {
                try
            {
                //should I use setters and getter here instead????
                rover.setX(int.Parse(roverStartingPos[0].ToString()));
                rover.setY(int.Parse(roverStartingPos[2].ToString()));
                rover.setDirection(roverStartingPos[4]);
                //rover1 = new Rover(rover1StartingPos[0], rover1StartingPos[2], rover1StartingPos[4]);
            }

            catch (FormatException)
            {
                Console.WriteLine("Invalid format of Rover1 starting position. Only x and y coordinates and direction(N,S,E,W) for the Rover must be provided");
                rover.setRoverPosition(rover);
            }
            }
            return rover;
        }
        public Rover validateRoverMovementCommand(Rover rover, Plateau plateau)
        {
            Console.WriteLine($"Please provide the movement command for {rover.getName()}");
            string rover1Movementcommand = Console.ReadLine();
            try
            {
                foreach (char i in rover1Movementcommand)
                {
                    switch (i)
                    {
                        case 'L':
                            rover.rotateL(rover.getDirection());
                            break;

                        case 'R':
                            rover.rotateR(rover.getDirection());
                            break;

                        case 'M':
                            rover.moveForward(rover.getDirection());
                            break;

                        case ' ':
                            break;
                        default:
                            //WHY DOESNT THIS ERROR MESSAGE APPEAR? DOES IT NOT GO TO CONSOLE?
                            throw new InvalidDataException($"Invalid format of {rover.getName()} movement command. Only the movement commands L,R and M are accepted");
                    }
                    if (rover.getX() > plateau.getLength() || rover.getX() < 0 || rover.getY() > plateau.getWidth() || rover.getY() < 0)
                    {
                        Console.WriteLine($"{rover.getName()} movement command exceeds the bounds of the Plateau");
                        throw new InvalidOperationException($"Rover must not exceed the bounds of the set {plateau.getLength()}by{plateau.getWidth()} Plateau");
                    }
                }
            }
            catch (InvalidDataException)
            {
                Console.WriteLine($"Invalid format of { rover.getName()} movement command. Only the movement commands L,R and M are accepted");
                rover.validateRoverMovementCommand(rover,plateau);
            }
            return rover;
        }

        public static void roverDeployment(List<Rover> data)
        {
            Console.WriteLine("How many Rovers will you be deploying");
            string numOfRovers = Console.ReadLine();
            if (numOfRovers == "0")
            {
                Console.WriteLine("No Rovers to be deployed");
            }
            else
            {
                //data = new Rover[int.Parse(numOfRovers.ToString())];

                //////set dimensions of the plateau
                Plateau plateau = new Plateau();
                plateau = plateau.setPlateauDimensions(plateau);

                for (int i = 0; i < int.Parse(numOfRovers.ToString()); i++)

                {
                    Rover rover = new Rover($"Rover{i + 1}");
                    /////set the position of rovers
                    rover = rover.setRoverPosition(rover);
                    //push the data into correct array spot
                    //data[i] = rover;
                    data.Insert(i, rover);
                    Rover.saveRoverData(data);
                    ////movement of rover
                    rover = rover.validateRoverMovementCommand(rover, plateau);
                    //push the rover data into the array
                    data[i] = rover;
                    Rover.saveRoverData(data);

                    Console.WriteLine($"Executing movement of {rover.getName()}");
                    Console.WriteLine($"new position of {rover.getName()} is {rover.getX()} {rover.getY()} {rover.getDirection()}");
                }
            }
        }

        //rename this method to something better
        public static void adjustRoversQuestion(List<Rover> data)
        {
            Console.WriteLine($"Would you like to adjust any of these Rovers? Y or N or type ! to see list of Rovers");
            string roverAdjustment = Console.ReadLine();
            switch (roverAdjustment)
            {
                case "Y":
                    Rover.roverAdjustment(data);
                    break;

                case "N":
                    Console.WriteLine($"No adjustments to be made to currently deployed Rovers");
                    break;

                case "!":
                    for (int i = 0; i < data.Count; i++)
                    {
                        Console.WriteLine($"{data[i].Name} is currently positioned at {data[i].X} {data[i].Y} facing {data[i].Direction}");
                    }
                    Rover.adjustRoversQuestion(data);
                    break;
                default:
                    Console.WriteLine($"Please type Y for yes, N for no or ! to see available list of Rovers");
                    Rover.adjustRoversQuestion(data);
                    break;
            }
        }

        //rename this method to something better
        public static void additionalRoversQuestion(List<Rover> data)
        {
            Console.WriteLine($"Would you like to deploy some additional Rovers?");
            string deployMoreRovers = Console.ReadLine();
            switch (deployMoreRovers)
            {
                case "Y":
                    Rover.additionalRoverDeployment(data);
                    break;

                case "N":
                    Console.WriteLine($"No additional Rovers to be deployed");
                    break;

                default:
                    Console.WriteLine($"Please type Y for yes or N for no");
                    Rover.additionalRoversQuestion(data);
                    break;
            }
        }

        public static void currentPositionOrNewQuestion(List<Rover> data, int roverPosition, Plateau plateau)
        {
            Console.WriteLine($"Use current position of {data[roverPosition].Name} at {data[roverPosition].X} {data[roverPosition].Y} facing {data[roverPosition].Direction} or provied a new starting position? Please Type C for current position or N for new position");
            string newOrOldPosition = Console.ReadLine();
            switch (newOrOldPosition)
            {
                case "C":
                    Rover existingPosRover = data[roverPosition];
                    existingPosRover = existingPosRover.validateRoverMovementCommand(existingPosRover, plateau);
                    //push the rover data into the array
                    data[roverPosition] = existingPosRover;
                    Rover.saveRoverData(data);
                    Console.WriteLine($"Executing movement of {existingPosRover.getName()}");
                    Console.WriteLine($"new position of {existingPosRover.getName()} is {existingPosRover.getX()} {existingPosRover.getY()} {existingPosRover.getDirection()}");
                    break;

                case "N":
                    Rover newPosRover = data[roverPosition];
                    /////set the position of rovers
                    newPosRover = newPosRover.setRoverPosition(newPosRover);
                    //push the data into correct array spot
                    data[roverPosition] = newPosRover;
                    Rover.saveRoverData(data);
                    newPosRover = newPosRover.validateRoverMovementCommand(newPosRover, plateau);
                    //push the rover data into the array
                    data[roverPosition] = newPosRover;
                    Rover.saveRoverData(data);
                    Console.WriteLine($"Executing movement of {newPosRover.getName()}");
                    Console.WriteLine($"new position of {newPosRover.getName()} is {newPosRover.getX()} {newPosRover.getY()} {newPosRover.getDirection()}");
                    break;

                default:
                    Console.WriteLine("Unaccepted input! Please type either C to use current position or N to provide a new position");
                    Rover.currentPositionOrNewQuestion(data, roverPosition, plateau);
                    break;
            }
        }

        public static void roverAdjustment(List<Rover> data)
        {
            Console.WriteLine($"Which Rover would you like to adjust? Type ! to see list of available Rovers");
            string roverName = Console.ReadLine();
            if (roverName == "!")
            {
                for (int i = 0; i < data.Count; i++)
                {
                    Console.WriteLine($"{data[i].Name} is currently positioned at {data[i].X} {data[i].Y} facing {data[i].Direction}");
                }
                Rover.roverAdjustment(data);
            }
            else
            {
                int roverPosition = data.FindIndex(x => x.Name == roverName);
                if (roverPosition == -1)
                {
                    Console.WriteLine("The provided Rover name does not exist. Current available Rovers are as follows");
                    for (int i = 0; i < data.Count; i++)
                    {
                        Console.WriteLine($"{data[i].Name} is currently positioned at {data[i].X} {data[i].Y} facing {data[i].Direction}");
                    }
                    Rover.roverAdjustment(data);
                }
                else
                {
                    //////set dimensions of the plateau
                    ///// set to load plateasu data
                    Plateau plateau = new Plateau();
                    plateau = plateau.setPlateauDimensions(plateau);

                    Rover.currentPositionOrNewQuestion(data, roverPosition, plateau);
                }
            }
        }

        public static void additionalRoverDeployment(List<Rover> data)
        {
            Console.WriteLine("How many Rovers will you be deploying");
            string numOfRovers = Console.ReadLine();
            if (numOfRovers == "0")
            {
                Console.WriteLine("No Additional Rovers to be deployed");
            }
            else
            {
                //////set dimensions of the plateau
                /////change to load previous plateau data
                Plateau plateau = new Plateau();
                plateau = plateau.setPlateauDimensions(plateau);

                for (int i = 0; i < int.Parse(numOfRovers.ToString()); i++)
                {
                    int j = data.Count;
                    Rover rover = new Rover($"Rover{1 + j}");
                    /////set the position of rovers
                    rover = rover.setRoverPosition(rover);
                    //push the data into correct array spot
                    data.Insert(j, rover);
                    Rover.saveRoverData(data);
                    ////movement of rover
                    rover = rover.validateRoverMovementCommand(rover, plateau);
                    //push the rover data into the array
                    data[j] = rover;
                    Rover.saveRoverData(data);

                    Console.WriteLine($"Executing movement of {rover.getName()}");
                    Console.WriteLine($"new position of {rover.getName()} is {rover.getX()} {rover.getY()} {rover.getDirection()}");
                }
            }
        }

        public static bool repeatProgram(bool x)
        {
            Console.WriteLine("Would you like to repeat the program? Y or N");
            string repeatProgram = Console.ReadLine();
            switch (repeatProgram)
            {
                case "Y":
                    x = true;
                    break;

                case "N":
                    x = false;
                    break;
                default:
                    Console.WriteLine("Accept answers are Y for yes and N for no");
                    Rover.repeatProgram(true);
                    break;
            }
            return x;
        }

        public static void saveRoverData(List<Rover> data)
        {
            var json = JsonConvert.SerializeObject(data);
            File.WriteAllText("./RoverData.json", json);
        }

        public static List<Rover> readRoverData()
        {
            string currentData = File.ReadAllText("./RoverData.json");
            List<Rover> data = JsonConvert.DeserializeObject<List<Rover>>(currentData);
            return data;
        }
    }
}