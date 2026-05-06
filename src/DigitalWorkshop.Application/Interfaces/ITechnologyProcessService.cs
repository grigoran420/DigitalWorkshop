using System.Collections.Generic;
using System.Threading.Tasks;
using DigitalWorkshop.Domain.Entities;

namespace DigitalWorkshop.Application.Services
{
    public interface ITechnologyProcessService
    {
        Task<IEnumerable<TechnologyProcess>> GetAllAsync();
        Task<TechnologyProcess?> GetByIdAsync(int id);
        Task<TechnologyProcess> CreateAsync(TechnologyProcess tp);
        Task<TechnologyProcess> UpdateAsync(TechnologyProcess tp);
        Task DeleteAsync(int id);
        // src/DigitalWorkshop.Application/Services/ITechnologyProcessService.cs
        Task SubmitForApprovalAsync(int id, string userName);
        Task ApproveAsync(int id, string userName, string comment);
        Task RejectAsync(int id, string userName, string comment);
    }
}