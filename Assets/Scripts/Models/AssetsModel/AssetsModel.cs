namespace Models
{
	public class AssetsModel : IAssetsModel
	{
		public AssetLinks Links { get; }

		public AssetsModel(AssetLinks assetLinks)
		{
			Links = assetLinks;
		}
	}
}