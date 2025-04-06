using System;

namespace VectorSearch
{
    public interface IDocumentStorage<T>
    {
        public void AddOrUpdateRecord(string key, T data);

        public T? GetRecord(string key);
    }

    /// <summary>
    /// Represents a storage mechanism for documents of type <typeparamref name="TDocumentStorageType"/>.
    /// </summary>
    /// <typeparam name="TDocumentStorageType">The type of the documents to be stored.</typeparam>
    public class DocumentStorage<TDocumentStorageType> : IDocumentStorage<TDocumentStorageType>
        where TDocumentStorageType : class
    {
        private readonly IDictionary<string, TDocumentStorageType> Records;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentStorage{DocumentStorageType}"/> class.
        /// </summary>
        public DocumentStorage()
        {
            this.Records = new Dictionary<string, TDocumentStorageType>();
        }

        /// <inheritdoc/>
        public void AddOrUpdateRecord(string key, TDocumentStorageType data)
        {
            if (!this.Records.TryGetValue(key, out _))
            {
                this.Records.Add(key, data);
            }
            else
            {
                this.Records[key] = data;
            }
        }

        /// <inheritdoc/>
        public TDocumentStorageType? GetRecord(string key)
        {
            if (this.Records.TryGetValue(key, out TDocumentStorageType _data))
            {
                return _data;
            }

            return null;
        }
    }
}

