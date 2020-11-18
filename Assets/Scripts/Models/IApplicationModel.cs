namespace Models
{
	public interface IApplicationModel //endpoint to reach any model in application. Fully non-Unity
	{
		void Update(); //composite update for models
		TModel GetView<TModel>();
	}
}