using F1.Common.DataContext.DataModel;

namespace F1TeamManager.WebApi.Repositories
{
    public interface ILicenseFeeRepository
    {
        Task<LicenseFee?> CreateAsync(LicenseFee fee);
        Task<IEnumerable<LicenseFee>> RetrieveAllAsync();
        Task<LicenseFee?> RetrieveAsync(string id);
        Task<LicenseFee?> UpdateAsync(string id, LicenseFee fee);
        Task<bool?> DeleteAsync(string id);
    }
}
