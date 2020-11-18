namespace Root
{
	public class CompositionRoot
	{
		private readonly IUpdater _updater;

		public CompositionRoot(IUpdater updater)
		{
			_updater = updater;
			_updater.Update += UpdaterOnUpdate;
			BuildCodeTree();
		}

		private void BuildCodeTree()
		{
			
		}

		private void UpdaterOnUpdate()
		{
			
		}
	}
}