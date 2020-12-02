using System.Collections.Generic;

namespace Models
{
	public class ModelUpdaterInOrder : IRootModel
	{
		private readonly ICollection<IUpdateableModel> _models;


		public ModelUpdaterInOrder(ICollection<IUpdateableModel> models)
		{
			_models = models;
		}
		
		public void Update()
		{
			foreach (var model in _models)
			{
				model.Update();
			}
		}
	}
}