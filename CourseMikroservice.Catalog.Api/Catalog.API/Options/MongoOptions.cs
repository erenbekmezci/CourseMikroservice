using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Options
{
    public class MongoOptions
    {

        [Required] public string ConnectionString { get; set; } = default!;
        [Required] public string Database { get; set; } = default!;    
    }
}
