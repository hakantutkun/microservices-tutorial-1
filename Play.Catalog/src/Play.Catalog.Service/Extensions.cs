using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service
{
    /// <summary>
    /// Extension Methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Converts an Item to ItemDto
        /// </summary>
        /// <param name="item">Received Item</param>
        /// <returns>Returns Item Dto</returns>
        public static ItemDto AsDto(this Item item)
        {
            // Create and return an ItemDto
            return new ItemDto(item.Id, item.Name, item.Description, item.Price, item.CreatedDate);
        }
    }
}