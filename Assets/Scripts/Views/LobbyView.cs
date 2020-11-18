using System;
using UnityEngine;

namespace Views
{
	public class LobbyView : ViewBase<LobbyWindowPrefabLinks>, ILobbyView
	{
		private readonly IUpdater _updater;
		
		public LobbyView(GameObject prefab, Transform parent) : base(prefab, parent)
		{
			
		}

		public override void Show()
		{
			base.Show();
		}

		public void SubscribeToRequestButton(Action action)
		{
			PrefabLink.RequestButton.onClick.AddListener(action.Invoke);
		}

		public void SetCoinsValue(int value)
		{
			PrefabLink.Coins.text = value.ToString();
		}

		public override void Hide()
		{
			PrefabLink.RequestButton.onClick.RemoveAllListeners();
			base.Hide();
		}
	}
}