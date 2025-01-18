using Microsoft.EntityFrameworkCore;
using Resume.Application.Dtos.Education;
using Resume.Application.Services.Interface.Education;
using Resume.Application.Utilities;
using Resume.Domain.Repository;

namespace Resume.Application.Services.Implementation.Education
{
    public class EducationService : IEducationService
    {
        #region Fields

        private readonly IGenericRepository<Domain.Entities.Education.Education> _educationRepository;



        #endregion

        #region Constructor

        public EducationService(IGenericRepository<Domain.Entities.Education.Education> educationRepository)
        {
            _educationRepository = educationRepository;
        }

        #endregion

        #region Methods

        public async Task<List<FilterEducationDto>> GetAllEducations()
        {
            var education = await _educationRepository
                .GetQuery()
                .AsQueryable()
                .Where(x => !x.IsDelete)
                .Select(x => new FilterEducationDto
                {
                    Id = x.Id,
                    College = x.College,
                    EducationYear = x.EducationYear,
                    Description = x.Description,
                    CreateDate = x.CreateDate.ToStringShamsiDate()

                }).OrderByDescending(x => x.Id)
                .ToListAsync();

            return education;
        }

        #endregion










        #region Dispose

        public async ValueTask DisposeAsync()
        {
            if (_educationRepository != null)
            {
                await _educationRepository.DisposeAsync();
            }
        }

        

        #endregion
    }
}
