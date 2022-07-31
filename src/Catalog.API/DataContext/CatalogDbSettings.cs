namespace Catalog.API.DataContext
{
    public class CatalogDbSettings : ICatalogDbSettings
    {
#nullable disable
        public string ConnectionString { get; set;}
        public string DatabaseName { get; set;}
        public string CollectionName { get; set;}
    }
}
