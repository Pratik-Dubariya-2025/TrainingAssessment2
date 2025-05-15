using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TrainingAssessment.Models.Data;
using TrainingAssessment.Repository.Repository.IRepository;

namespace TrainingAssessment.Repository.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    public readonly TrainingAssessmentDbContext dbContext;
    internal DbSet<T> DbSet;

    public Repository(TrainingAssessmentDbContext dbContext)
    {
        this.dbContext = dbContext;
        DbSet = dbContext.Set<T>();
    }

    public T? GetFirstOrDefault(Expression<Func<T, bool>> where) =>
        DbSet.Where(where).FirstOrDefault();

    public TObj? GetFirstOrDefault<TObj>(Expression<Func<T, TObj>> select, Expression<Func<T, bool>> where) 
    {
        IQueryable<T> query = DbSet.Where(where);
        return query.Select(select).FirstOrDefault();
    }

    public T? GetFirstOrDefault<TObj>(Expression<Func<T, bool>> filter, Expression<Func<T, TObj>>? include = null)
    {
        IQueryable<T> query = DbSet;
        if (include != null)
        {
            query = query.Where(filter).Include(include);
        }
        return query.FirstOrDefault();
    }

    public List<TObj> GetAll<TObj>(Expression<Func<T, TObj>> select, Expression<Func<T, bool>> where)
    {
        IQueryable<T> query = DbSet.Where(where);
        return query.Select(select).ToList();
    }

    public List<TObj> GetAll<TObj, TKey>(Expression<Func<T, TObj>> select, Expression<Func<T, bool>> where, Expression<Func<T, TKey>> orderBy, bool ascending = true)
    {
        IQueryable<T> query = DbSet.Where(where);
        query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
        return query.Select(select).ToList();
    }
}
