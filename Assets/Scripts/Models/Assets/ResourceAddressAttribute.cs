using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Models.Assets
{
	[AttributeUsage(AttributeTargets.Field)]
	public class ResourceAddressAttribute : PropertyAttribute
	{
		public Type Type;

		public ResourceAddressAttribute(Type type)
		{
			Type = type;
		}

		public ResourceAddressAttribute()
		{
			Type = typeof(Object);
		}
	}
}