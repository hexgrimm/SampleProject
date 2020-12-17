namespace Models.Meta
{
	public interface IMetaService : IUpdateable
	{
		bool IsConnected { get; }
	}
}