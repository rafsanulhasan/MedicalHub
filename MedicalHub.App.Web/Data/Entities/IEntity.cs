using System;

namespace MedicalHub.App.Web.Data.Entities
{
	public interface IEntity<TKey>
		where TKey:IEquatable<TKey>
	{
		TKey Id { get; set; }
	}
}
