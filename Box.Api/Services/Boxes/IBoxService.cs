using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Box.Api.Controllers;
using Box.Api.Services.Boxes.Exceptions;
using Box.Api.Services.Boxes.Models;
using Box.Api.Services.Trays;
using Box.Core.Data;
using Box.Core.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace Box.Api.Services.Boxes
{
    public interface IBoxService
    {
        /// <summary>
        ///     Adds a <see cref="BoxDto" /> to the database
        /// </summary>
        /// <param name="userId">User id who owns the box</param>
        /// <param name="data">Name of the <see cref="BoxDto" /> to create</param>
        /// <returns>The newly created <see cref="BoxDto" /></returns>
        Task<BoxDto> AddBox(Guid userId, BoxCreationData data);

        /// <summary>
        ///     Changes the name of a <see cref="BoxDto" />
        /// </summary>
        /// <param name="userId">User id who owns the box</param>
        /// <param name="data">Data which contains required information to change the name of the box</param>
        /// <returns>Changed <see cref="BoxDto" /></returns>
        Task<BoxDto> ChangeName(Guid userId, BoxChangeName data);

        /// <summary>
        /// Returns the requested <see cref="BoxDto"/>
        /// </summary>
        /// <param name="userId">User id who owns the box</param>
        /// <param name="boxId">Box id of the box</param>
        /// <exception cref="BoxNotFoundException">Thrown if the requested box does not exist</exception>
        /// <returns>Returns the requested <see cref="BoxDto"/></returns>
        Task<BoxDto> GetBox(Guid userId, long boxId);

        /// <summary>
        /// Returns all <see cref="BoxDto"/>es which belongs to the user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Returns an <see cref="IEnumerable{T}"/> of <see cref="BoxDto"/></returns>
        Task<IEnumerable<BoxDto>> GetBoxes(Guid userId);

        /// <summary>
        /// Returns all <see cref="BoxDto"/>es which are owned by the specified <paramref name="userId"/>
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>All deleted boxes</returns>
        Task<IEnumerable<BoxDto>> DeleteBoxes(Guid userId);

        Task<User> GetUser(Guid userIdGuid);

        Task<IEnumerable<TrayDto>> GetTrays(Guid userid, long boxId);
        
        Task<TrayDto> AddTray(Guid getId, long boxId, TrayCreationData creationData);
        
        Task<TrayDto> GetTray(Guid getId, long boxId, long trayId);

        Task<IEnumerable<CardDto>> GetCards(Guid userId, long boxId, long trayId);
        
        Task<CardDto> GetCard(Guid userId, long boxId, long trayId, long cardId);
        
        Task<CardDto> AddCard(Guid userId, long boxId, long trayId, CardCreationData data);
    }
}