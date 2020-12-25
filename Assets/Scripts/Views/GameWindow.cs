using Common;
using UnityEngine;

namespace Views
{
	public class GameWindow : ViewBase<GameWindowPrefabLinks>, IGameWindow
	{
		private readonly Flag _quitGameButton = new Flag();
		
		public IFlag QuitGameButton => _quitGameButton;
		
		public GameWindow(Transform parent) : base(parent)
		{
			
		}

		protected override void ShowInternal()
		{
			PrefabLink.QuitGameButton.onClick.AddListener(_quitGameButton.Raise);
		}

		protected override void HideInternal()
		{
			PrefabLink.QuitGameButton.onClick.RemoveAllListeners();
		}
	}
}