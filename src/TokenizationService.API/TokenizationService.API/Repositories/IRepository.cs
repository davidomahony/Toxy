namespace TokenizationService.API.Repositories
{
    /// <summary>
    /// Generic repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>  where T : class
    {
        /// <summary>
        /// Create an object
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> CreateAsync(T entity);

        /// <summary>
        /// read an object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> ReadAsync(string id);

        /// <summary>
        /// Update an object
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> UpdateAsync(string id, T entity);

        /// <summary>
        /// Update an object
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> GetNextCount();
    }
}
