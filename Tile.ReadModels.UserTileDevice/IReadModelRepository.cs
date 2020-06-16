using System.Collections.Generic;

namespace Tile.ReadModels
{
    public interface IReadModelRepository
    {
        //each list of read models is created for particular UI page
        //can be an Observable collection
        List<UserTileDeviceReadModel> UserTileDeviceReadModels { get; set; }
    }
}