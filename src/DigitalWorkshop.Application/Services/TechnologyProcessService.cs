using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DigitalWorkshop.Application.Services;
using DigitalWorkshop.Domain.Entities;
using DigitalWorkshop.Infrastructure.Data;

namespace DigitalWorkshop.Infrastructure.Services
{
    public class TechnologyProcessService : ITechnologyProcessService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TechnologyProcessService> _logger;

        public TechnologyProcessService(ApplicationDbContext context, ILogger<TechnologyProcessService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<TechnologyProcess>> GetAllAsync()
        {
            return await _context.TechnologyProcesses
                .Include(tp => tp.Operations)
                .ToListAsync();
        }

        public async Task<TechnologyProcess?> GetByIdAsync(int id)
        {
            return await _context.TechnologyProcesses
                .Include(tp => tp.Operations)
                    .ThenInclude(o => o.Transitions)
                        .ThenInclude(t => t.BomItems)
                .Include(tp => tp.Operations)
                    .ThenInclude(o => o.Transitions)
                        .ThenInclude(t => t.Tools)
                .Include(tp => tp.History)
                .FirstOrDefaultAsync(tp => tp.Id == id);
        }

        public async Task<TechnologyProcess> CreateAsync(TechnologyProcess tp)
        {
            tp.Status = TpStatus.Draft;
            tp.IsLocked = false;
            tp.AddHistoryEntry("Создан", "System");

            _context.TechnologyProcesses.Add(tp);
            await _context.SaveChangesAsync();
            return tp;
        }

        public async Task<TechnologyProcess> UpdateAsync(TechnologyProcess tp)
        {
            if (tp.IsLocked && tp.Status != TpStatus.Draft && tp.Status != TpStatus.Rejected)
                throw new InvalidOperationException("Редактирование утвержденного ТП запрещено.");

            var existing = await _context.TechnologyProcesses.FindAsync(tp.Id);
            if (existing == null) throw new KeyNotFoundException();

            // Простое обновление полей (в реальности нужно аккуратнее)
            existing.Name = tp.Name;
            // Здесь нужна логика обновления вложенных коллекций Operations/Transitions

            existing.AddHistoryEntry("Обновлен", "User");
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task DeleteAsync(int id)
        {
            var tp = await _context.TechnologyProcesses.FindAsync(id);
            if (tp != null)
            {
                if (tp.Status == TpStatus.Approved)
                    throw new InvalidOperationException("Нельзя удалить утвержденный ТП.");

                _context.TechnologyProcesses.Remove(tp);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SubmitForApprovalAsync(int id, string userName)
        {
            var tp = await _context.TechnologyProcesses.FindAsync(id);
            if (tp == null || tp.IsLocked) throw new Exception("ТП не найден или заблокирован");

            tp.Status = TpStatus.PendingApproval;
            tp.AddHistoryEntry($"Отправлен на согласование пользователем {userName}", userName);

            await _context.SaveChangesAsync();
        }

        public async Task ApproveAsync(int id, string userName, string comment)
        {
            var tp = await _context.TechnologyProcesses.FindAsync(id);
            if (tp == null) throw new Exception("ТП не найден");

            // Логика многоуровневого согласования (упрощенно)
            if (tp.Status == TpStatus.PendingApproval)
            {
                tp.Status = TpStatus.Approved; // Или промежуточный статус
                tp.IsLocked = true;

                // Генерация хеша версии (заглушка)
                tp.VersionHash = Guid.NewGuid().ToString("N").Substring(0, 8);

                tp.AddHistoryEntry($"Утверждено пользователем {userName}. Комментарий: {comment}", userName);
            }

            await _context.SaveChangesAsync();
        }

        public async Task RejectAsync(int id, string userName, string comment)
        {
            var tp = await _context.TechnologyProcesses.FindAsync(id);
            if (tp == null) throw new Exception("ТП не найден");

            tp.Status = TpStatus.Draft;
            tp.AddHistoryEntry($"Отклонено пользователем {userName}. Причина: {comment}", userName);

            await _context.SaveChangesAsync();
        }

        private string CalculateHash(TechnologyProcess tp)
        {
            // Упрощенная реализация хеширования
            var data = $"{tp.Id}{tp.Version}{tp.Name}{tp.CreatedAt}";
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }


    }
}