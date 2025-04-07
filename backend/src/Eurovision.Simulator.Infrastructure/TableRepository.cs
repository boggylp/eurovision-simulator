namespace Eurovision.Simulator.Infrastructure;

public interface ITableRepository<T> where T : class
{
}

public class CsvRepository<T> : ITableRepository<T>
{
}
