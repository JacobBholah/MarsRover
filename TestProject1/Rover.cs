using System;
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
            switch(direction)
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
                    setY(getY()+1);
                    break;

                case 'E':
                    setX(getX()+1);
                    break;

                case 'S':
                    setY(getY()-1);
                    break;

                case 'W':
                    setX(getX()-1);
                    break;
            }
        }
        public Rover setRoverPosition(Rover rover)
        {
            Console.WriteLine($"Please provide the starting point for {rover.getName()}");
            string roverStartingPos = Console.ReadLine();

            if (roverStartingPos.Length > 5)
                throw new InvalidDataException("Rover1 starting position string is too long");
            try
            {
                //should I use setters and getter here instead????
                rover.setX(int.Parse(roverStartingPos[0].ToString()));
                rover.setY(int.Parse(roverStartingPos[2].ToString()));
                rover.setDirection(roverStartingPos[4]);
                //rover1 = new Rover(rover1StartingPos[0], rover1StartingPos[2], rover1StartingPos[4]);
            }

            catch (InvalidCastException)
            {
                Console.WriteLine("Invalid format of Rover1 starting position");
                throw new InvalidCastException(@"only x and y coordinates and direction(N,S,E,W) for the Rover must be provided");
            }

            return rover;
        }
        public Rover validateRoverMovementCommand(Rover rover,Plateau plateau)
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
                Console.WriteLine($"Invalid format of {rover.getName()} movement command");
                throw new InvalidDataException(@"Only the movement commands L,R and M are accepted");
            }
            return rover;
        }
    }
}