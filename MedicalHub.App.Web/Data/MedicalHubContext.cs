using MedicalHub.App.Web.Data.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System;

namespace MedicalHub.App.Web.Data
{
	public class MedicalHubContext
		: IdentityDbContext<User, Role, Guid>
	{
		public DbSet<Appointment> Appointments { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			var appointment = builder.Entity<Appointment>();
			appointment.Property(a => a.Id).HasDefaultValueSql("newid()");
		}
	}
}
