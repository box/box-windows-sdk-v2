namespace Box.V2.Converter
{
    public interface IBoxConverter
    {
        /// <summary>
        /// Parses the string content into the provided type T
        /// </summary>
        /// <typeparam name="T">The type of the content</typeparam>
        /// <param name="content">The string content</param>
        /// <returns></returns>
        T Parse<T>(string content);

        /// <summary>
        /// Serializes the type into a string
        /// </summary>
        /// <typeparam name="T">The type of the entity</typeparam>
        /// <param name="entity">The entity to be serialized</param>
        /// <returns></returns>
        string Serialize<T>(T entity);
    }
}
