using MedicalHub.App.Web.Data.Entities;

using System;

namespace MedicalHub.App.Web.Data.Repositories
{
	internal class AppointmentRepository
		: RepositoryBase<Appointment, Guid>
	{
		public AppointmentRepository(MedicalHubContext hubContext) 
			: base(hubContext)
		{
		}
	}
}
