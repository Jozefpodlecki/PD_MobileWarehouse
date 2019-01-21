namespace Common.Mappers
{
    public interface IMapper<T,V>
    {
        V Map(T entity);
    }
}
