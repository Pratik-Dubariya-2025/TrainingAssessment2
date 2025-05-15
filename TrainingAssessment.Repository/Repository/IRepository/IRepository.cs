using System.Linq.Expressions;

namespace TrainingAssessment.Repository.Repository.IRepository;

public interface IRepository<T> where T : class
{
    T? GetFirstOrDefault(Expression<Func<T, bool>> filter);
    TObj? GetFirstOrDefault<TObj>(Expression<Func<T, TObj>> select, Expression<Func<T, bool>> where);
    T? GetFirstOrDefault<TObj>(Expression<Func<T, bool>> filter, Expression<Func<T, TObj>>? include = null);
    List<TObj> GetAll<TObj>(Expression<Func<T, TObj>> select, Expression<Func<T, bool>> where);
    List<TObj> GetAll<TObj, TKey>(Expression<Func<T, TObj>> select, Expression<Func<T, bool>> where, Expression<Func<T, TKey>> orderBy, bool ascending = true);
}
