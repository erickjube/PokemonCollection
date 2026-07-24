namespace PokemonCollection.Application.Helpers;

public static class ValidatePagination
{
    public static void Validate(int pageNumber, int pageSize, int totalCount)
    {
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        if (pageNumber > totalPages && totalPages > 0) throw new ArgumentException("Página solicitada não existe.");
    }
}
