using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Tile.ReadModels.Service;

namespace Tile.ReadModels.EventHandler
{
    //handle the events here and route them to the read models service layer
    //this handler subscribes to the domain events using the implementation we choose
    public class ReadModelsEventHandler
    {
        //dependency injection
        public IReadModelsService ReadModelsService { get; set; }

        public void EventReceived(IEvent evt)
        {
            ReadModelsService.Handle(evt);
        }
        
    }
}
