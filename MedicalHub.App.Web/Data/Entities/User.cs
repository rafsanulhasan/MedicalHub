using System;

using Microsoft.AspNetCore.Identity;

namespace MedicalHub.App.Web.Data.Entities
{
	public class User
		: IdentityUser<Guid>, IEntity<Guid>
	{
	}
}
