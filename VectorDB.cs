using System;
namespace VectorSearch
{
	public class VectorDBClient
	{
        private List<(string key, double[] Vector)> vectors = new();
        private VectorComaprisonAlgorithm ComparisonAlgorithm;

        public VectorDBClient(VectorComaprisonAlgorithm algorithm)
		{
            this.ComparisonAlgorithm = algorithm;
		}

        public void AddVector(string key, double[] vector)
        {
            vectors.Add((key, vector));
        }

        public List<(string key, double similarity)> FindSimilarVectors(double[] queryVector, int topResults)
        {
            var results = vectors.Select(vec => (
                vec.key,
                similarity: this.ComparisonAlgorithm.Compare(queryVector, vec.Vector)
            )).OrderByDescending(r => r.similarity)
              .Take(topResults)
              .ToList();

            return results;
        }
    }
}

