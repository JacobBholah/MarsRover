using System;
using System.IO;

namespace MarsRover
{
    public class Plateau
    {
        /////attributes////
        public int Length;
        public int Width;

        ///constructors/////
        public Plateau(int length, int width)
        {
            this.Length = length;
            this.Width = width;
        }

        public Plateau() { }
        ////methods////
        public int getLength()
        {
            return Length;
        }
        public void setLength(int length)
        {
            this.Length = length;
        }
        public int getWidth()
        {
            return Width;
        }
        public void setWidth(int width)
        {
            this.Width = width;
        }

        public Plateau setPlateauDimensions(Plateau plateau)
        {
            Console.WriteLine("Please provide the upper right coordinates of the plateau");
            string upperRightCoords = Console.ReadLine();
            if (upperRightCoords.Length > 3)
                throw new InvalidDataException("Plateau coordinate string is too long");
            try
            {
                //should I use setters and getter here instead????
                //testPlataeu.setLength(upperRightCoords[0]);
                //testPlataeu.setWidth(upperRightCoords[2]);

                plateau = new Plateau(upperRightCoords[0], upperRightCoords[2]);
            }

            catch (InvalidCastException)
            {
                Console.WriteLine("Invalid format of intial plateau coordinates");
                throw new InvalidCastException(@"only x and y coordinates for plateau should be provided");
            }
            return plateau;
        }
    }
}