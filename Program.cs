using VectorSearch;

SearchProvider provider = new SearchProvider(new DocumentStorage<Document>());

var documents = new List<Document>
{
    new Document
    {
        Title = "Teams freezes during screen sharing",
        Description = "When a user starts sharing their screen during a meeting, the app becomes unresponsive. Other participants can still see the shared screen, but the person sharing cannot interact with Teams or stop the sharing without force-quitting the app.",
        AreaPath = "Teams/Meetings/ScreenSharing",
    },
    new Document
    {
        Title = "Meeting audio cuts off randomly",
        Description = "Users report that during video meetings, audio randomly cuts out for a few seconds and then returns. This happens inconsistently but affects communication significantly. Issue occurs across different networks and devices.",
        AreaPath = "Teams/Meetings/Audio",
    },
    new Document
    {
        Title = "Messages not syncing between desktop and mobile",
        Description = "Messages sent from the desktop app are not showing up on the mobile app until it is manually refreshed. Notifications also arrive late or not at all. This causes confusion and missed conversations.",
        AreaPath = "Teams/Messaging/Sync",
    },
    new Document
    {
        Title = "Chat search returns no results",
        Description = "The search bar in the chat section returns 'No results found' even for messages that are visibly present in the chat. Restarting Teams or reindexing does not fix the issue.",
        AreaPath = "Teams/Messaging/Search",
    },
    new Document
    {
        Title = "Status stuck on 'Away'",
        Description = "User status shows as 'Away' even when actively using Teams. The status does not update to 'Available' automatically, causing confusion with team members who think the user is inactive.",
        AreaPath = "Teams/Presence",
    },
    new Document
    {
        Title = "File upload fails in channel chat",
        Description = "Attempting to upload a file to a Teams channel results in a 'Something went wrong' error. The issue persists with different file types and sizes, and even with stable internet connections.",
        AreaPath = "Teams/Files/Upload",
    },
    new Document
    {
        Title = "Meeting recording not saving to OneDrive",
        Description = "After recording a meeting, the recording does not appear in OneDrive or SharePoint as expected. Teams shows that the meeting was recorded, but there’s no way to access the video afterward.",
        AreaPath = "Teams/Meetings/Recording",
    },
    new Document
    {
        Title = "Repeated prompts to sign in",
        Description = "Users are frequently prompted to sign in again even after checking 'Keep me signed in.' This happens several times a day and disrupts workflow, especially during active calls or meetings.",
        AreaPath = "Teams/Auth",
    },
    new Document
    {
        Title = "Background blur not working on Mac",
        Description = "The background blur feature is not available or doesn’t function properly on certain Mac devices. Instead of blurring, the entire video feed becomes pixelated or freezes.",
        AreaPath = "Teams/Video/Effects",
    },
    new Document
    {
        Title = "Emoji reactions not displaying correctly",
        Description = "When a user reacts to a message with an emoji, it either doesn’t show up or displays the wrong icon. This issue is inconsistent but has been reported across multiple platforms.",
        AreaPath = "Teams/Messaging/Reactions",
    },
};


provider.InitializeAndStoreData(documents);

var queryDocument = new Document
{
    Title = "Audio intermittently drops during Teams call",
    Description = "In ongoing Teams calls, some participants report that their audio randomly cuts out for a few seconds and then comes back. It’s affecting communication and seems to happen without a pattern.",
};

var result = provider.GetSimilarDocument(queryDocument);
foreach (var item in result)
{
    Console.WriteLine(item.Title);
}
