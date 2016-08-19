using System.Collections.Generic;
using System.Linq;
using Sitecore.Data.Items;
using Sitecore.MediaFramework.Cleanup;
using Sitecore.MediaFramework.Entities;
using Sitecore.MediaFramework.Import;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Cleanup
{
    public abstract class ExtendedCleanupExecuterBase<TEntity, TSearchResult> : CleanupExecuterBase<TEntity, TSearchResult> where TSearchResult : MediaServiceSearchResult, new()
    {
        public List<string> ImportNames { get; protected set; }

        public void AddImportName(string importName)
        {
            if (string.IsNullOrEmpty(importName))
                return;

            this.ImportNames.Add(importName);
        }

        protected ExtendedCleanupExecuterBase()
        {
            this.ImportNames = new List<string>();
        }

        protected override List<TEntity> GetServiceData(Item accountItem)
        {
            if (this.ImportNames.Any())
            {
                var serviceData = new List<TEntity>();

                // revisit and convert to linq
                foreach (var importName in this.ImportNames)
                {
                    var data = ImportManager.ImportList<TEntity>(importName, accountItem);

                    if (data != null)
                        serviceData.AddRange(data);
                }

                return serviceData;
            }

            return base.GetServiceData(accountItem);
        }
    }
}
