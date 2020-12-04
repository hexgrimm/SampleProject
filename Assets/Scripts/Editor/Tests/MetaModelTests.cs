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
		private Meta _test;
		
		[SetUp]
		public void SetUp()
		{
			_timeModel = Substitute.For<ITimeModel>();
			_timeModel.RealTimeSinceStartup.ReturnsForAnyArgs(0);
			
			_service = Substitute.For<IMetaService>();
			_service.IsConnected.ReturnsForAnyArgs(true);
			
			_test = new Meta(_timeModel, _service);
		}
		
		[Test]
		public void UpdateTest()
		{
			_test.Update();
		}

		[Test]
		public void RequestMoreCoins()
		{
			var initialCoins = _test.Coins;
			
			var promise = _test.RequestMoreCoins();

			Assert.IsTrue(!promise.IsCompleted);

			_timeModel.RealTimeSinceStartup.ReturnsForAnyArgs(4);
			_test.Update();
			
			Assert.IsTrue(promise.IsCompleted);
			Assert.IsTrue(!promise.IsFaulted);
			Assert.IsTrue(_test.Coins != initialCoins);
		}
		
		[Test]
		public void RequestExchangeCoins()
		{
			var initialCoins = _test.Coins;
			var initialCrystals = _test.Crystals;
			
			var promise = _test.ExchangeCoinsToCrystals(10);

			Assert.IsTrue(!promise.IsCompleted);

			_timeModel.RealTimeSinceStartup.ReturnsForAnyArgs(4);
			_test.Update();
			
			Assert.IsTrue(promise.IsCompleted);
			Assert.IsTrue(!promise.IsFaulted);
			Assert.IsTrue(_test.Coins == initialCoins - 10);
			Assert.IsTrue(_test.Crystals == initialCrystals + 10);
		}

		[Test]
		public void PromiseFailIfNoConnection()
		{
			var initialCoins = _test.Coins;

			_service.IsConnected.ReturnsForAnyArgs(false);
			
			var promise = _test.ExchangeCoinsToCrystals(10);
			
			_timeModel.RealTimeSinceStartup.ReturnsForAnyArgs(4);
			_test.Update();
			
			Assert.IsTrue(promise.IsCompleted);
			Assert.IsTrue(promise.IsFaulted);
			Assert.IsTrue(_test.Coins == initialCoins);
		}
	}
}