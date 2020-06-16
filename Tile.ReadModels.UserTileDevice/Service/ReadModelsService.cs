using System;
using System.Collections.Generic;
using System.Text;

namespace Tile.ReadModels.Service
{
    public class ReadModelsService: IReadModelsService
    {
        public IReadModelRepository ReadModelsRepository { get; set; }

        public void Handle(IEvent evt)
        {
            //optimize here and make a generic code to scan all the read models in the assembly
            //and deliver event to each of th e models
            //each model can choose which event to process
            var utd = new UserTileDeviceReadModel();
            utd.Handle(evt, ReadModelsRepository);

            var am = new DcsReadModel();
            am.Handle(evt, ReadModelsRepository);
        }
    }
}
