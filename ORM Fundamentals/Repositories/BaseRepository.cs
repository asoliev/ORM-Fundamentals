namespace ORM_Fundamentals.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected ApplicationDbContext Context { get; }

        protected BaseRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public void Create(T entity)
        {
            Context.Add(entity);
            Context.SaveChanges();
        }

        public T Read(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            Context.Update(entity);
            Context.SaveChanges();
        }

        public void Delete(T entity)
        {
            Context.Remove(entity);
            Context.SaveChanges();
        }
    }
}