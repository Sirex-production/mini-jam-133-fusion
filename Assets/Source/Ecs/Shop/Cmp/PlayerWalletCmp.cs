using Secs;

namespace Ingame
{
	public struct PlayerWalletCmp : IEcsComponent
	{
		public int currentAmountOfCoins;
		
		public bool HasEnoughCoins(int amountOfCoins)
		{
			return amountOfCoins <= currentAmountOfCoins;
		}
	}
}