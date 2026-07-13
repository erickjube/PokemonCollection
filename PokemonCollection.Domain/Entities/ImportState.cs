namespace PokemonCollection.Domain.Entities;

public class ImportState
{
    public int Id { get; private set; }
    public int LastImportedPage { get; private set; }
    public DateTime LastExecution { get; private set; } = DateTime.UtcNow;

    private ImportState() { }
    public ImportState(int lastImportedPage)
    {
        LastImportedPage = lastImportedPage;
        LastExecution = DateTime.UtcNow;
    }

    public void Update(int page)
    {
        LastImportedPage = page;
        LastExecution = DateTime.UtcNow;
    }

}
