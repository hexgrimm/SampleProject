using System.Threading.Tasks;

namespace Models
{
	public interface IMetaModel
	{
		int Coins { get; }
		int Crystals { get; }
		bool IsConnected { get; }
		
		IPromise RequestMoreCoins();
		IPromise ExchangeCoinsToCrystals(int coinsAmount);
	}
}