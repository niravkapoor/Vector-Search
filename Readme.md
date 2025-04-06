# VectorSearch

VectorSearch is a .NET 7.0-based application that implements a vector similarity search engine. It allows users to store documents, tokenize their content, generate embeddings, and find similar documents based on cosine similarity.

## Project Description

The project is designed to demonstrate how vector-based similarity search can be implemented using a combination of tokenization, embedding generation, and vector comparison algorithms. It uses the following key components:

- **Document Storage**: Stores documents and their metadata.
- **VectorDB**: A simple in-memory vector database that supports adding vectors and finding similar vectors.
- **Cosine Similarity**: A vector comparison algorithm to calculate the similarity between two vectors.
- **Porter2 Stemmer**: Used for tokenization and stemming of document text.
- **Newtonsoft.Json**: Used for JSON serialization (though currently commented out in the code).

## Features

- Tokenization and stemming of document text.
- Vocabulary generation and embedding creation.
- Cosine similarity-based vector comparison.
- In-memory storage of documents and vectors.
- Querying for similar documents based on input text.

## Project Structure

VectorSearch/ ├── CosineSimilarity.cs # Implements cosine similarity algorithm ├── Document.cs # Represents a document with metadata ├── DocumentStorage.cs # Provides in-memory storage for documents ├── Program.cs # Entry point of the application ├── SearchProvider.cs # Handles document processing and similarity search ├── VectorComaprisonAlgorithm.cs # Abstract class for vector comparison algorithms ├── VectorDB.cs # In-memory vector database ├── VectorSearch.csproj # Project file ├── VectorSearch.sln # Solution file ├── VectorSearchResponse.cs # Represents the response for a similarity search ├── bin/ # Build output directory └── obj/ # Intermediate build directory

## How It Works

1. **Document Initialization**: Documents are initialized with titles, descriptions, and area paths.
2. **Tokenization and Stemming**: Text is tokenized and stemmed using the Porter2 Stemmer.
3. **Embedding Generation**: A vector embedding is generated for each document based on the vocabulary.
4. **Vector Storage**: The embeddings are stored in an in-memory vector database.
5. **Similarity Search**: Given a query document, the system generates its embedding and finds the most similar documents using cosine similarity.

## Prerequisites

- .NET 7.0 SDK
- NuGet packages:
  - `Newtonsoft.Json` (v13.0.3)
  - `Porter2Stemmer` (v1.0.0)

## Getting Started

1. Clone the repository:

   ```bash
   git clone <repository-url>
   cd VectorSearch

   ```

2. Restore dependencies:

   ```bash
   dotnet restore
   ```

3. Build the project:

   ```bash
   dotnet build
   ```

4. Run the application:
   ```bash
   dotnet run
   ```

```c#
using VectorSearch;

SearchProvider provider = new SearchProvider(new DocumentStorage<Document>());

var documents = new List<Document>
{
    new Document
    {
        Title = "Teams freezes during screen sharing",
        Description = "When a user starts sharing their screen during a meeting, the app becomes unresponsive.",
        AreaPath = "Teams/Meetings/ScreenSharing"
    },
    new Document
    {
        Title = "Meeting audio cuts off randomly",
        Description = "Users report that during video meetings, audio randomly cuts out for a few seconds.",
        AreaPath = "Teams/Meetings/Audio"
    }
};

provider.InitializeAndStoreData(documents);

var queryDocument = new Document
{
    Title = "Audio intermittently drops during Teams call",
    Description = "In ongoing Teams calls, some participants report that their audio randomly cuts out.",
    AreaPath = "Teams/Meetings/Audio"
};

var result = provider.GetSimilarDocument(queryDocument);
foreach (var item in result)
{
    Console.WriteLine(item.Title);
}
```

### Key Classes

SearchProvider: Handles tokenization, embedding generation, and similarity search. <br>
VectorDBClient: Stores and retrieves vectors for similarity comparison. <br>
CosineSimilarity: Implements the cosine similarity algorithm. <br>
DocumentStorage: Provides in-memory storage for documents. <br>
