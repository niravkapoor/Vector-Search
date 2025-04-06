using System;
namespace VectorSearch
{
	public class CosineSimilarity : VectorComaprisonAlgorithm
	{
		public CosineSimilarity()
		{
		}

        public override double Compare(double[] vec1, double[] vec2)
        {
            double dot = 0, normA = 0, normB = 0;
            for (int i = 0; i < vec1.Length; i++)
            {
                dot += vec1[i] * vec2[i];
                normA += vec1[i] * vec1[i];
                normB += vec2[i] * vec2[i];
            }
            return (normA == 0 || normB == 0) ? 0 : dot / (Math.Sqrt(normA) * Math.Sqrt(normB));
        }
    }
}

