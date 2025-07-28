namespace CodeCharm.Lexicon.Web;

public class ApiClient(HttpClient httpClient)
{
    public async Task<IEnumerable<Lexica>> GetLexicon(int maxItems = 10, CancellationToken cancellationToken = default)
    {
        var lexicon = new List<Lexica>();

        await foreach (var lexica in httpClient.GetFromJsonAsAsyncEnumerable<Lexica>("/lexica", cancellationToken))
        {
            if (lexicon.Count >= maxItems)
            {
                break;
            }
            if (lexica is not null)
            {
                lexicon.Add(lexica);
            }
        }

        return lexicon;
    }
}

public record Lexica(string Key, string Title);
