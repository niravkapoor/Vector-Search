namespace VectorSearch
{
    public class Document
    {
        public string DocumentId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string AreaPath { get; set; }

        public string GetEmbeddingData()
        {
            //var embeddingsData = new Dictionary<string, string> { { nameof(Title), Title } };

            //if (!string.IsNullOrWhiteSpace(Description))
            //{
            //    embeddingsData.Add(nameof(Description), Description);
            //}

            return $"{this.Title} {this.Description}";
            //return JsonConvert.SerializeObject(embeddingsData, Formatting.Indented);
        }
    }
}

