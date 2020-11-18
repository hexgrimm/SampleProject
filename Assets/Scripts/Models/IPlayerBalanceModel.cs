using System;

namespace Models
{
	public interface IPlayerBalanceModel
	{
		int Coins { get; }
		event Action CoinsValueChanged;
		void RequestMoreCoins();
	}
}