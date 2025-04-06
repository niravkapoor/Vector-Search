namespace VectorSearch
{
    public class VectorSearchResponse : Document
    {
        public double Similarity { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VectorSearchResponse"/> class.
        /// </summary>
        public VectorSearchResponse() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="VectorSearchResponse"/> class.
        /// </summary>
        /// <param name="dc">The document object containing details to initialize the response.</param>
        /// <param name="similarity">The similarity score between the query and the document.</param>
        public VectorSearchResponse(Document dc, double similarity)
        {
            this.DocumentId = dc.DocumentId;
            this.AreaPath = dc.AreaPath;
            this.Description = dc.Description;
            this.Title = dc.Title;
            this.Similarity = similarity;
        }
    }
}

