using System;
using System.Linq;

namespace Tile.ReadModels
{
    //this read model can be used on UI it can have all the needed attributes of the regular view model
    //aka TileEntity(?)
    public class UserTileDeviceReadModel: IReadModel
    {
        public Guid UserGuid { get; set; }
        public Guid TileGuid { get; set; }
        public long? BluetoothAdderss { get; set; }
        public Guid? TileDeviceGuid { get; set; }
        public long TileId { get; set; }
        public string State { get; set; }
        public string RingState { get; set; }

        public void Handle(IEvent evt, IReadModelRepository repo)
        {
            if (evt is UserTileCreated userTileCreated)
            {
                //this is create read model case
                this.UserGuid = userTileCreated.UserGuid;
                this.TileGuid = userTileCreated.TileGuid;
                this.TileId = userTileCreated.TileId;
                repo.UserTileDeviceReadModels.Add(this);
            }
            if (evt is UserTileRemoved userTileRemoved)
            {
                var rm = repo.UserTileDeviceReadModels.FirstOrDefault(x => x.UserGuid == userTileRemoved.UserGuid
                                                                  && x.TileGuid == userTileRemoved.UserGuid
                
                                                                  && x.TileId == userTileRemoved.TileId);
                if (rm == null)
                {
                    return;
                }
                //tile deactivated - remove it from the list so it's not displayed on the UI anymore
                repo.UserTileDeviceReadModels.Remove(rm);
            }
            if (evt is DeviceConnected deviceConnected)
            {
                //this is update read model case
                var rm = repo.UserTileDeviceReadModels.
                    FirstOrDefault(x => x.TileId == deviceConnected.TileId);
                if (rm != null)
                {
                    rm.State = "Connected";
                }
            }
            if (evt is DeviceStartedRinging deviceStartedRinging)
            {
                //this is update read model case
                var rm = repo.UserTileDeviceReadModels.
                    FirstOrDefault(x => x.TileDeviceGuid == deviceStartedRinging.TileDeviceGuid);
                if (rm == null)
                {
                    return;
                }

                rm.RingState = "Ringing";
            }

            if (evt is DeviceDisconnected deviceDisconnected)
            {
                //this is update read model case
                var rm = repo.UserTileDeviceReadModels.
                    FirstOrDefault(x => x.TileDeviceGuid == deviceDisconnected.TileDeviceGuid);
                if (rm == null)
                    return;
                rm.TileDeviceGuid = null;
            }
        }
    }

    public interface IReadModel
    {
        void Handle(IEvent evt, IReadModelRepository repo);
    }

    public interface IEvent { }

    public class UserTileCreated : IEvent
    {
        public Guid UserGuid { get; set; }
        public Guid TileGuid { get; set; }
        public long TileId { get; set; }
    }

    public class DeviceConnected : IEvent
    {
        public Guid TileDeviceGuid { get; set; }
        public long TileId { get; set; }
    }

    public class DeviceStartedRinging : IEvent
    {
        public Guid TileDeviceGuid { get; set; }
        public long TileId { get; set; }
    }

    public class DeviceDisconnected : IEvent
    {
        public Guid TileDeviceGuid { get; set; }
        public long TileId { get; set; }
    }

    public class UserTileRemoved : IEvent
    {
        public Guid UserGuid { get; set; }
        public Guid TileDeviceGuid { get; set; }
        public long TileId { get; set; }
    }
}
