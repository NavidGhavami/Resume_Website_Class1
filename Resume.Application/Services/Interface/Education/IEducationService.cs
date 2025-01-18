using Resume.Application.Dtos.Education;

namespace Resume.Application.Services.Interface.Education;

public interface IEducationService : IAsyncDisposable
{
    Task<List<FilterEducationDto>> GetAllEducations();
}