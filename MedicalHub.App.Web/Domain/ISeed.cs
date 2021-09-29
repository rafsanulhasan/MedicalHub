
using System.Threading;
using System.Threading.Tasks;

namespace MedicalHub.App.Web.Domain
{
	public interface ISeed
	{
		Task MigrateAsync(CancellationToken cancellationToken = default);
		Task SeedDataAsync();
	}
}
