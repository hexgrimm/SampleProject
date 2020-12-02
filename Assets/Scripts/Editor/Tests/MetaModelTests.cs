using Models;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests
{
	[TestFixture]
	public class MetaModelTests
	{
		private ITimeModel _timeModel;
		private MetaModel _testModel;
		
		[SetUp]
		public void SetUp()
		{
			_timeModel = Substitute.For<ITimeModel>();
			_testModel = new MetaModel(_timeModel);
		}
		
		[Test]
		public void UpdateTest()
		{
			_timeModel.RealTimeSinceStartup.ReturnsForAnyArgs(0);
			_testModel.Update();
		}

		[Test]
		public void RequestMoreCoins()
		{
			var initialCoins = _testModel.Coins;
			
			_timeModel.RealTimeSinceStartup.ReturnsForAnyArgs(0);
			var promise = _testModel.RequestMoreCoins();

			Assert.IsTrue(!promise.IsCompleted);

			_timeModel.RealTimeSinceStartup.ReturnsForAnyArgs(4);
			_testModel.Update();
			
			Assert.IsTrue(promise.IsCompleted);
			Assert.IsTrue(!promise.IsFaulted);
			Assert.IsTrue(_testModel.Coins != initialCoins);
		}
		
		[Test]
		public void RequestExchangeCoins()
		{
			var initialCoins = _testModel.Coins;
			var initialCrystals = _testModel.Crystals;
			
			
			_timeModel.RealTimeSinceStartup.ReturnsForAnyArgs(0);
			var promise = _testModel.ExchangeCoinsToCrystals(10);

			Assert.IsTrue(!promise.IsCompleted);

			_timeModel.RealTimeSinceStartup.ReturnsForAnyArgs(4);
			_testModel.Update();
			
			Assert.IsTrue(promise.IsCompleted);
			Assert.IsTrue(!promise.IsFaulted);
			Assert.IsTrue(_testModel.Coins == initialCoins - 10);
			Assert.IsTrue(_testModel.Crystals == initialCrystals + 10);
		}
	}
}