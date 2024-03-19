namespace ScreenSound.Modelos; 
using OpenAI_API;

internal class Banda : IAvaliavel
{
    private List<Album> albuns = new List<Album>();
    private List<Avaliacao> notas = new List<Avaliacao>();

    public Banda(string nome)
    {
        Nome = nome;
    }

    public string Nome { get; }
    public double Media
    {
        get
        {
            if (notas.Count == 0) return 0;
            else return Math.Round(notas.Average(a => a.Nota), 2);
        }
    }
    public string? Resumo { get; set; }
    public List<Album> Albuns => albuns;

    public void AdicionarAlbum(Album album) 
    { 
        albuns.Add(album);
    }

    public void AdicionarNota(Avaliacao nota)
    {
        notas.Add(nota);
    }

    public void AdicionarResumo()
    {
        var client = new OpenAIAPI("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");
        var chat = client.Chat.CreateConversation();
        chat.AppendSystemMessage($"Resuma a banda {Nome} em 1 parágrafo.");
        var resposta = chat.GetResponseFromChatbotAsync().GetAwaiter().GetResult();
        Resumo = resposta;
    }
    public void ExibirDiscografia()
    {
        Console.WriteLine($"Discografia da banda {Nome}");
        foreach (Album album in albuns)
        {
            Console.WriteLine($"Álbum: {album.Nome} ({album.DuracaoTotal})");
        }
    }
}