using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    //Generic репозиторий
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private PipeInspectionLogContext _context;
        private DbSet<TEntity> _table;

        public GenericRepository(PipeInspectionLogContext context)
        {
            _context = context;
            _table = context.Set<TEntity>();
        }

        public void Delete(object id)
        {
            TEntity entityToDelete = _table.Find(id);
            _table.Remove(entityToDelete);
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _table;
            //Фильтр
            if (filter != null)
            {
                query = query.Where(filter);
            }

            //Данные для связи с таблицами
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            //Сортировка
            if (orderBy != null)
                return orderBy(query).ToList();
            else
                return query.ToList();
        }

        public TEntity GetById(object id)
        {
            return _table.Find(id);
        }

        public void Insert(TEntity obj)
        {
            _table.Add(obj);
        }

        public void Update(TEntity obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
        }
    }
}
