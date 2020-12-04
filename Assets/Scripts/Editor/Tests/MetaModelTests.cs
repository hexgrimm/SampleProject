using Models;
using Models.Meta;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests
{
	[TestFixture]
	public class MetaModelTests
	{
		private ITimeModel _timeModel;
		private IMetaService _service;
		private MetaModel _testModel;
		
		[SetUp]
		public void SetUp()
		{
			_timeModel = Substitute.For<ITimeModel>();
			_timeModel.RealTimeSinceStartup.ReturnsForAnyArgs(0);
			
			_service = Substitute.For<IMetaService>();
			_service.IsConnected.ReturnsForAnyArgs(true);
			
			_testModel = new MetaModel(_timeModel, _service);
		}
		
		[Test]
		public void UpdateTest()
		{
			_testModel.Update();
		}

		[Test]
		public void RequestMoreCoins()
		{
			var initialCoins = _testModel.Coins;
			
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
			
			var promise = _testModel.ExchangeCoinsToCrystals(10);

			Assert.IsTrue(!promise.IsCompleted);

			_timeModel.RealTimeSinceStartup.ReturnsForAnyArgs(4);
			_testModel.Update();
			
			Assert.IsTrue(promise.IsCompleted);
			Assert.IsTrue(!promise.IsFaulted);
			Assert.IsTrue(_testModel.Coins == initialCoins - 10);
			Assert.IsTrue(_testModel.Crystals == initialCrystals + 10);
		}

		[Test]
		public void PromiseFailIfNoConnection()
		{
			var initialCoins = _testModel.Coins;

			_service.IsConnected.ReturnsForAnyArgs(false);
			
			var promise = _testModel.ExchangeCoinsToCrystals(10);
			
			_timeModel.RealTimeSinceStartup.ReturnsForAnyArgs(4);
			_testModel.Update();
			
			Assert.IsTrue(promise.IsCompleted);
			Assert.IsTrue(promise.IsFaulted);
			Assert.IsTrue(_testModel.Coins == initialCoins);
		}
	}
}