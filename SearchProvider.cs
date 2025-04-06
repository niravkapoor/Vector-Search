using System;
using Newtonsoft.Json.Linq;
using Porter2Stemmer;

namespace VectorSearch
{
	public class SearchProvider
	{
		private HashSet<string> TokenDataSet;
		private IDictionary<string, int> WordToIndexMapping;
		private IDocumentStorage<Document> DocumentStorageService;
		private VectorDBClient vectorcDBClient;
        public SearchProvider(IDocumentStorage<Document> documentStorageService)
		{
			TokenDataSet = new();
			WordToIndexMapping = new Dictionary<string, int>();
			DocumentStorageService = documentStorageService;
            vectorcDBClient = new VectorDBClient(new CosineSimilarity());
        }

		public void InitializeAndStoreData(List<Document> data)
		{
            Dictionary<string, string[]> _docToTokenMapping = new();
			// Creating vocabulory, this is needed when we wanted to refresh our vocab
            foreach (Document item in data)
            {
                item.DocumentId = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
                var tokens = Tokenization(item.GetEmbeddingData());
                UpdateTokenDataSet(tokens);
                _docToTokenMapping[item.DocumentId] = tokens;
            }
			UpdateWordToIndexMapping();

            foreach (Document item in data)
            {
                var embedding = GenerateEmbedding(_docToTokenMapping[item.DocumentId]);
                this.DocumentStorageService.AddOrUpdateRecord(item.DocumentId, item);
                vectorcDBClient.AddVector(item.DocumentId, embedding);
            }
        }

		public IEnumerable<Document> GetSimilarDocument(Document data)
		{
            var tokens = Tokenization(data.GetEmbeddingData());
            var embedding = GenerateEmbedding(tokens);

			IEnumerable<(string key, double similarity)> result = this.vectorcDBClient.FindSimilarVectors(embedding, 2);

			List<VectorSearchResponse> output = new();

            foreach ((string key, double similarity) item in result)
			{
				Document dc = this.DocumentStorageService.GetRecord(item.key);
				if(dc != null)
				{
					output.Add(new VectorSearchResponse(dc, item.similarity));
				}
            }

			return output;
        }

		private string[] Tokenization(string embeddingData)
		{
            var stemmer = new EnglishPorter2Stemmer();

            var lemmatizedTokens = embeddingData.ToLower().Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries).Select(tk => stemmer.Stem(tk).Value);

			return lemmatizedTokens.ToArray();
        }

		private void UpdateTokenDataSet(string[] tokens)
		{
            foreach (var token in tokens)
                TokenDataSet.Add(token);
        }

		private void UpdateWordToIndexMapping()
		{
            WordToIndexMapping = TokenDataSet.Select((word, index) => new { word, index })
                                    .ToDictionary(x => x.word, x => x.index);
        }

		private double[] GenerateEmbedding(string[] tokens)
		{
            
            var vector = new double[TokenDataSet.Count];
            foreach (var token in tokens)
            {
                if (WordToIndexMapping.ContainsKey(token))
                    vector[WordToIndexMapping[token]] += 1;
            }
			return vector;
        }
	}
}

