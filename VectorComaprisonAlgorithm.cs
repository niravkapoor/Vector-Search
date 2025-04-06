using System;
namespace VectorSearch
{
	public abstract class VectorComaprisonAlgorithm
	{
		public VectorComaprisonAlgorithm()
		{
		}

		public abstract double Compare(double[] vec1, double[] vec2);
	}
}

