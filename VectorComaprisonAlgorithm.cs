namespace VectorSearch
{
    public abstract class VectorComaprisonAlgorithm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VectorComaprisonAlgorithm"/> class.
        /// </summary>
        public VectorComaprisonAlgorithm()
        {
        }

        public abstract double Compare(double[] vec1, double[] vec2);
    }
}
