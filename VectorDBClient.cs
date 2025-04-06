namespace VectorSearch
{
    public class VectorDBClient
    {
        private readonly List<(string key, double[] Vector)> vectors = new();
        private readonly VectorComaprisonAlgorithm comparisonAlgorithm;

        /// <summary>
        /// Initializes a new instance of the <see cref="VectorDBClient"/> class.
        /// </summary>
        /// <param name="algorithm">The algorithm used to compare vectors.</param>
        public VectorDBClient(VectorComaprisonAlgorithm algorithm)
        {
            this.comparisonAlgorithm = algorithm;
        }

        public void AddVector(string key, double[] vector)
        {
            this.vectors.Add((key, vector));
        }

        public List<(string key, double similarity)> FindSimilarVectors(double[] queryVector, int topResults)
        {
            var results = this.vectors.Select(vec => (
                vec.key,
                similarity: this.comparisonAlgorithm.Compare(queryVector, vec.Vector))).OrderByDescending(r => r.similarity)
              .Take(topResults)
              .ToList();

            return results;
        }
    }
}
