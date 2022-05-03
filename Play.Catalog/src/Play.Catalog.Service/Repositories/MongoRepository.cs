using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Repositories
{
    /// <summary>
    /// Items Repository Class
    /// </summary>
    public class MongoRepository<T> : IRepository<T> where T : IEntity
    {
        #region Members

        /// <summary>
        /// Database Collection
        /// </summary>
        private readonly IMongoCollection<T> dbCollection;

        /// <summary>
        /// Filter Builder for MongoDB
        /// </summary>
        private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            // Set collection
            dbCollection = database.GetCollection<T>(collectionName);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all items asynchronously.
        /// </summary>
        /// <returns>Collection of items.</returns>
        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        /// <summary>
        /// Returns requested specified item.
        /// </summary>
        /// <param name="id">The id of the requested item.</param>
        /// <returns>Returns requested item.</returns>
        public async Task<T> GetAsync(Guid id)
        {
            // Create filter for id matching.
            FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.Id, id);

            // Return matched item.
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Adds New Item to the collection.
        /// </summary>
        /// <param name="entity">Received Item that will be added to collection.</param>
        public async Task CreateAsync(T entity)
        {
            // Check if received item is null
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            // Add the received item to the collection.
            await dbCollection.InsertOneAsync(entity);
        }

        /// <summary>
        /// Updates specific existing item.
        /// </summary>
        /// <param name="entity">Item that will be updated</param>
        public async Task UpdateAsync(T entity)
        {
            // Check if received item is null
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            // Create filter for update process.
            FilterDefinition<T> filter = filterBuilder.Eq(existingEntity => existingEntity.Id, entity.Id);

            // Update existing item.
            await dbCollection.ReplaceOneAsync(filter, entity);
        }

        /// <summary>
        /// Removes specified item.
        /// </summary>
        /// <param name="id">The id of the item that will be deleted.</param>
        public async Task RemoveAsync(Guid id)
        {
            // Create filter for delete process.
            FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.Id, id);

            // Remove item from collection.
            await dbCollection.DeleteOneAsync(filter);
        }

        #endregion
    }
}