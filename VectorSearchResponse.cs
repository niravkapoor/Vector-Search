using System;
namespace VectorSearch
{
	public class VectorSearchResponse : Document
	{
		public double Similarity { get; set; }

        public VectorSearchResponse() { }
		public VectorSearchResponse(Document dc, double similarity)
        {
            DocumentId = dc.DocumentId;
			AreaPath = dc.AreaPath;
			Description = dc.Description;
			Title = dc.Title;
			Similarity = similarity;
        }
    }
}

