using System;
using System.ComponentModel.DataAnnotations;

namespace Play.Catalog.Service.Dtos
{
    /// <summary>
    /// Item Data Transfer Object
    /// <summary>
    public record ItemDto(Guid Id, string Name, string Description, decimal Price, DateTimeOffset CreatedDate);

    /// <summary>
    /// Create Item Data Transfer Object
    /// <summary>
    public record CreateItemDto([Required] string Name, string Description, [Range(0, 1000)] decimal Price);

    /// <summary>
    /// Update Item Data Transfer Object
    /// <summary>
    public record UpdateItemDto([Required] string Name, string Description, [Range(0, 1000)] decimal Price);
}