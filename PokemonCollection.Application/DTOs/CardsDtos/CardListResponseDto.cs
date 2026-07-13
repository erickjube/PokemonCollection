namespace PokemonCollection.Application.DTOs.CardsDtos;

public class CardListResponseDto
{
    public List<CardResponseDto> Data { get; set; } = [];
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int Count { get; set; }
    public int TotalCount { get; set; }
}
