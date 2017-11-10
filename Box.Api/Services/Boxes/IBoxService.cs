using System;
using System.Threading.Tasks;
using Box.Api.Controllers;
using Box.Api.Services.Boxes.Exceptions;
using Box.Core.DataTransferObjects;

namespace Box.Api.Services.Boxes
{
    public interface IBoxService
    {
        /// <summary>
        ///     Adds a <see cref="Box" /> to the database
        /// </summary>
        /// <param name="userId">User id who owns the box</param>
        /// <param name="data">Name of the <see cref="Box" /> to create</param>
        /// <returns>The newly created <see cref="Box" /></returns>
        Task<Core.DataTransferObjects.Box> AddBox( Guid userId, BoxCreationData data );

        /// <summary>
        ///     Changes the name of a <see cref="Box" />
        /// </summary>
        /// <param name="userId">User id who owns the box</param>
        /// <param name="data">Data which contains required information to change the name of the box</param>
        /// <returns>Changed <see cref="Box" /></returns>
        Task<Core.DataTransferObjects.Box> ChangeName( Guid userId, BoxChangeName data );

        /// <summary>
        /// Returns the requested <see cref="Core.DataTransferObjects.Box"/>
        /// </summary>
        /// <param name="userId">User id who owns the box</param>
        /// <param name="boxId">Box id of the box</param>
        /// <exception cref="BoxNotFoundException">Thrown if the requested box does not exist</exception>
        /// <returns>Returns the requested <see cref="Box"/></returns>
        Task<Core.DataTransferObjects.Box> GetBox(Guid userId, long boxId);
    }
}