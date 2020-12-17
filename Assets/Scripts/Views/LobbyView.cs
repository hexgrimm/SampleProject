using EventUtils;
using UnityEngine;

namespace Views
{
	public class LobbyView : ViewBase<LobbyWindowPrefabLinks>, ILobbyView
	{
		private readonly Signal _requestCoinsButton = new Signal();
		private readonly Signal _startGameButton = new Signal();

		public ISignal RequestCoinsButton => _requestCoinsButton;

		public ISignal StartGameButton => _startGameButton;

		public LobbyView(GameObject prefab, Transform parent) : base(prefab, parent)
		{
			
		}

		public void SetCoinsValue(int value)
		{
			PrefabLink.Coins.text = value.ToString();
		}
		
		public void SetDeltaTimeValue(float value)
		{
			PrefabLink.DeltaTime.text = value.ToString("0.00000");
		}


		protected override void ShowInternal()
		{
			PrefabLink.RequestButton.onClick.AddListener(_requestCoinsButton.Raise);
			PrefabLink.StartGameButton.onClick.AddListener(_startGameButton.Raise);
		}

		protected override void HideInternal()
		{
			PrefabLink.RequestButton.onClick.RemoveAllListeners();
			PrefabLink.StartGameButton.onClick.RemoveAllListeners();
		}
	}
}