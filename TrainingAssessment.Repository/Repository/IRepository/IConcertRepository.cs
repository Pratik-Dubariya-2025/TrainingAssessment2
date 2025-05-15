using TrainingAssessment.Models.Models;
using TrainingAssessment.Models.ViewModels;

namespace TrainingAssessment.Repository.Repository.IRepository;

public interface IConcertRepository : IRepository<Concert>
{
    List<ConcertViewModel> GetFilteredConcerts(DateTime? filterDate, string? search);
}
