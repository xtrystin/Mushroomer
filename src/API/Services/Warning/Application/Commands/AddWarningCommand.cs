using Application.Dto;
using MediatR;

namespace Application.Commands;

public class AddWarningCommand : IRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string? Province { get; set; }
    public string MushroomName { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? ThumbnailPhotoUrl { get; set; }
    public DateTime Date { get; set; }
    public bool AutoActivate { get; set; }
    public Guid AuthorId { get; set; }
}
