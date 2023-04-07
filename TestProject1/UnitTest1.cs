using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarsRover
{
    [TestClass]
    public class RoverTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Rover testrover = new Rover();
            testrover.setX(4);
            testrover.setY(3);
            testrover.setDirection('N');

            Assert.AreEqual(4, testrover.getX(), "Wrong x-coord");
            Assert.AreEqual(3, testrover.getY(), "Wrong y-coord");
            Assert.AreEqual('N', testrover.getDirection(), "Wrong direction");
        }
    }
}