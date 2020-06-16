namespace Tile.ReadModels.Service
{
    public interface IReadModelsService
    {
        void Handle(IEvent evt);
    }
}