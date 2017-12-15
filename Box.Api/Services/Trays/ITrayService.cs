using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Box.Core.Data;
using Box.Core.DataTransferObjects;

namespace Box.Api.Services.Trays
{
    public interface ITrayService
    {
        Task<TrayDto> GetTray(Guid userId, long trayId);

        Task<IEnumerable<TrayDto>> GetTrays(Guid userId, long boxId);

        Task<TrayDto> AddTray(Guid userId, long boxId, TrayCreationData data);

        Task<TrayDto> DeleteTray(Guid userId, long boxId, long trayId);

        Task<TrayDto> RenameTray(Guid userId, long boxId, long trayId, string newName);
    }
}