using Common;

namespace Models.Meta
{
	public interface IMetaModel : IUpdateable
	{
		int Coins { get; }
		int Crystals { get; }
		bool IsConnected { get; }
		
		IPromise RequestMoreCoins();
		IPromise ExchangeCoinsToCrystals(int coinsAmount);
	}
}