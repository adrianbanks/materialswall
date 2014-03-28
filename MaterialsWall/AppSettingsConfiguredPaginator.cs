using System;
using System.Collections.Generic;

namespace Granta.MaterialsWall
{
    public interface IAppSettingsConfiguredPaginator<T>
    {
        IEnumerable<T> GetPage(int pageNumber, IEnumerable<T> items);
    }

    public sealed class AppSettingsConfiguredPaginator<T> : IAppSettingsConfiguredPaginator<T>
    {
        private const string PageSizeAppSettingName = "PageSize";

        private readonly IAppSettingsProvider appSettingsProvider;
        private readonly IPaginator<T> paginator;

        public AppSettingsConfiguredPaginator(IAppSettingsProvider appSettingsProvider, IPaginator<T> paginator)
        {
            if (appSettingsProvider == null)
            {
                throw new ArgumentNullException("appSettingsProvider");
            }
            
            if (paginator == null)
            {
                throw new ArgumentNullException("paginator");
            }
            
            this.appSettingsProvider = appSettingsProvider;
            this.paginator = paginator;
        }

        public IEnumerable<T> GetPage(int pageNumber, IEnumerable<T> items)
        {
            int pageSize = appSettingsProvider.GetIntegerSetting(PageSizeAppSettingName, 10);
            return paginator.GetPage(pageSize, pageNumber, items);
        }
    }
}