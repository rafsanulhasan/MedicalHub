using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalHub.App.Web.Data.Entities
{
	public class Appointment
		: IEntity<Guid>
	{
		[Key]
		public Guid Id { get; set; }

		public DateTime AppointmentDate { get; set; }

		public Guid DoctorId { get; set; }
		public virtual User Doctor { get; set; }

		public Guid PatientId { get; set; }
		public virtual User Patient { get; set; }
	}
}
