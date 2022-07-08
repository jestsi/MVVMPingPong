using System;
using NUnit.Framework;
using ApplicationModels;

namespace ApplicationsTests
{
    [TestFixture]
    public class RocketModelTests
    {
        [Test]
        public void EqualsTest()
        {
            var model = new RocketModel();
            var model1 = new RocketModel(){Width = 100, Height = 200};
            var modelEmpty = new RocketModel();
            var modelReverse = new RocketModel(){Width = 200, Height = 100};

            Assert.IsFalse(model.Equals(model1));
            Assert.IsTrue(model.Equals(modelEmpty));
            Assert.IsTrue(modelEmpty.Equals(model));
            Assert.IsFalse(model1.Equals(modelReverse));
        }

        [TestCase(Direction.Up)]
        [TestCase(Direction.Left)]
        [TestCase(Direction.Right)]
        [TestCase(Direction.Down)]
        public void MoveTest(Direction dir)
        {
            var model = new RocketModel();
            int expectedVal;
            
            switch (dir)
            {
                case Direction.Up:
                    expectedVal = model.Y - model.StepSize;
                    break;
                case Direction.Down:
                    expectedVal = model.Y + model.StepSize;
                    break;
                case Direction.Left:
                    expectedVal = model.X - model.StepSize;
                    break;
                case Direction.Right:
                    expectedVal = model.X + model.StepSize;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
            }
            model.Move(dir);

            Assert.AreEqual(expectedVal, (int)dir > 1 ? model.X : model.Y);
        }
        
    }
}