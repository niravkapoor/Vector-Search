using Porter2Stemmer;

namespace VectorSearch
{
    public class SearchProvider
    {
        private readonly HashSet<string> TokenDataSet;
        private IDictionary<string, int> WordToIndexMapping;
        private readonly IDocumentStorage<Document> DocumentStorageService;
        private readonly VectorDBClient vectorcDBClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchProvider"/> class.
        /// </summary>
        /// <param name="documentStorageService">The service used to store and retrieve documents.</param>
        public SearchProvider(IDocumentStorage<Document> documentStorageService)
        {
            this.TokenDataSet = new ();
            this.WordToIndexMapping = new Dictionary<string, int>();
            this.DocumentStorageService = documentStorageService;
            this.vectorcDBClient = new VectorDBClient(new CosineSimilarity());
        }

        public void InitializeAndStoreData(List<Document> data)
        {
            Dictionary<string, string[]> _docToTokenMapping = new ();

            // Creating vocabulory, this is needed when we wanted to refresh our vocab
            foreach (Document item in data)
            {
                item.DocumentId = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
                var tokens = this.Tokenization(item.GetEmbeddingData());
                this.UpdateTokenDataSet(tokens);
                _docToTokenMapping[item.DocumentId] = tokens;
            }
            this.UpdateWordToIndexMapping();

            foreach (Document item in data)
            {
                var embedding = this.GenerateEmbedding(_docToTokenMapping[item.DocumentId]);
                this.DocumentStorageService.AddOrUpdateRecord(item.DocumentId, item);
                this.vectorcDBClient.AddVector(item.DocumentId, embedding);
            }
        }

        public IEnumerable<Document> GetSimilarDocument(Document data)
        {
            var tokens = this.Tokenization(data.GetEmbeddingData());
            var embedding = this.GenerateEmbedding(tokens);

            IEnumerable<(string key, double similarity)> result = this.vectorcDBClient.FindSimilarVectors(embedding, 2);

            List<VectorSearchResponse> output = new();

            foreach ((string key, double similarity) item in result)
            {
                Document dc = this.DocumentStorageService.GetRecord(item.key);
                if (dc != null)
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
            {
                this.TokenDataSet.Add(token);
            }
        }

        private void UpdateWordToIndexMapping()
        {
            this.WordToIndexMapping = this.TokenDataSet.Select((word, index) => new { word, index })
                                    .ToDictionary(x => x.word, x => x.index);
        }

        private double[] GenerateEmbedding(string[] tokens)
        {

            var vector = new double[this.TokenDataSet.Count];
            foreach (var token in tokens)
            {
                if (this.WordToIndexMapping.ContainsKey(token))
                {
                    vector[this.WordToIndexMapping[token]] += 1;
                }
            }
            return vector;
        }
    }
}

