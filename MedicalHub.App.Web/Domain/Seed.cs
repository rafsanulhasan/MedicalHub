using MedicalHub.App.Web.Data;
using MedicalHub.App.Web.Data.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System.Threading;
using System.Threading.Tasks;

namespace MedicalHub.App.Web.Domain
{
	internal class Seed
		: ISeed
	{
		private readonly MedicalHubContext _context;
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<Role> _roleManager;

		private async Task<User[]> CreateUsersAsync()
		{
			User doctorUser = new()
			{
				UserName = "doctor1",
				Email = "doctor1@mymedicalhub.com",
				PhoneNumber = "+8801772000000"
			};
			User patientUser = new()
			{
				UserName = "patient1",
				Email = "patient1@mymedicalhub.com",
				PhoneNumber = "+8801773000000"
			};
			await _userManager.CreateAsync(doctorUser, "doctor1");
			await _userManager.CreateAsync(patientUser, "patient1");
			doctorUser = await _userManager.FindByNameAsync(doctorUser.UserName);
			patientUser = await _userManager.FindByNameAsync(patientUser.UserName);
			return new[] { doctorUser, patientUser };
		}

		private async Task<Role[]> CreateRolesAsync()
		{
			var doctorRole = new Role() { Name = "Doctor" };
			var patientRole = new Role() { Name = "Patient" };
			await _roleManager.CreateAsync(doctorRole);
			await _roleManager.CreateAsync(patientRole);
			return new[] { doctorRole, patientRole };
		}

		private async Task MapUsersToRoles(User[] users, Role[] roles) 
		{
			await _userManager.AddToRoleAsync(
				users[0],
				roles[0].Name
			);

			await _userManager.AddToRoleAsync(
				users[1],
				roles[1].Name
			);
		}

		public Seed(
			MedicalHubContext context,
			UserManager<User> userManager,
			RoleManager<Role> roleManager
		)
		{
			_context = context;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public async Task MigrateAsync(CancellationToken cancellationToken = default)
		{
			var isCreated = await _context.Database.EnsureCreatedAsync(cancellationToken);
			if (isCreated)
				_context.Database.Migrate();
		}
		public async Task SeedDataAsync()
		{
			var users = await CreateUsersAsync();

			var roles = await CreateRolesAsync();

			await MapUsersToRoles(users, roles);
		}
	}
}
