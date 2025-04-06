using System;

namespace VectorSearch
{
	public interface IDocumentStorage<T>
	{
		public void AddOrUpdateRecord(string key, T data);
		public T GetRecord(string key);
    }

	public class DocumentStorage<DocumentStorageType> : IDocumentStorage<DocumentStorageType>
		where DocumentStorageType : class
    {
		private IDictionary<string, DocumentStorageType> Records;

		public DocumentStorage()
		{
			Records = new Dictionary<string, DocumentStorageType>();
        }

		public void AddOrUpdateRecord(string key, DocumentStorageType data)
		{
			if(!this.Records.TryGetValue(key, out DocumentStorageType _data))
			{
				this.Records.Add(key, data);
			}
			else
			{
				this.Records[key] = data;
            }
		}

        public DocumentStorageType GetRecord(string key)
		{
            if (this.Records.TryGetValue(key, out DocumentStorageType _data))
            {
				return _data;
            }

			return null;
        }
    }
}

