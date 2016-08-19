using Sitecore.Data.Items;
using Sitecore.MediaFramework.Synchronize;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Synchronize.EntityCreators
{
    public class TagEntityCreator : IMediaServiceEntityCreator
    {
        public virtual object CreateEntity(Item item)
        {
            return new Tag()
            {
                Name = item[Templates.Tag.Name]
            };
        }
    }
}