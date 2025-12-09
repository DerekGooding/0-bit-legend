using BitLegend.MapEditor.Model;
using System.ComponentModel;

namespace BitLegend.Tests
{
    [TestClass]
    public class ModelTests
    {
        [TestMethod]
        public void TransitionData_PropertyChangedEvents()
        {
            var transition = new TransitionData();
            string? receivedPropertyName = null;

            transition.PropertyChanged += (object? sender, PropertyChangedEventArgs e) =>
            {
                receivedPropertyName = e.PropertyName;
            };

            transition.MapId = "NewMapId";
            Assert.AreEqual(nameof(transition.MapId), receivedPropertyName);

            transition.StartPositionX = 1;
            Assert.AreEqual(nameof(transition.StartPositionX), receivedPropertyName);

            transition.StartPositionY = 2;
            Assert.AreEqual(nameof(transition.StartPositionY), receivedPropertyName);

            transition.DirectionType = "Down";
            Assert.AreEqual(nameof(transition.DirectionType), receivedPropertyName);

            transition.SizeX = 3;
            Assert.AreEqual(nameof(transition.SizeX), receivedPropertyName);

            transition.SizeY = 4;
            Assert.AreEqual(nameof(transition.SizeY), receivedPropertyName);

            transition.PositionX = 5;
            Assert.AreEqual(nameof(transition.PositionX), receivedPropertyName);

            transition.PositionY = 6;
            Assert.AreEqual(nameof(transition.PositionY), receivedPropertyName);
        }
    }
}
