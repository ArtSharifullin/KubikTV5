using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Reflection;

namespace MyORMLibrary;

/// <summary>
/// Представляет контекст ORM для работы с базой данных.
/// </summary>
/// <typeparam name="T">Тип сущности.</typeparam>
public class ORMContext<T> where T : class, new()
{
    private readonly IDbConnection _dbConnection;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="ORMContext{T}"/> с указанным соединением с базой данных.
    /// </summary>
    /// <param name="dbConnection">Соединение с базой данных.</param>
    public ORMContext(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    /// <summary>
    /// Создает новую запись в базе данных.
    /// </summary>
    /// <param name="entity">Сущность для создания.</param>
    public void Create(T entity)
    {
        var properties = entity.GetType().GetProperties()
            .Where(p => p.Name != "Id");
        var columns = string.Join(", ", properties.Select(p => p.Name));
        var values = string.Join(", ", properties.Select(p => '@' + p.Name));
        string query = $"INSERT INTO {typeof(T).Name}s ({columns}) VALUES ({values})";

        using (var command = _dbConnection.CreateCommand())
        {
            command.CommandText = query;
            foreach (var property in properties)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = '@' + property.Name;
                parameter.Value = property.GetValue(entity) ?? DBNull.Value;
                command.Parameters.Add(parameter);
            }
            _dbConnection.Open();
            command.ExecuteNonQuery();
            _dbConnection.Close();
        }
    }

    /// <summary>
    /// Получает все записи из базы данных.
    /// </summary>
    /// <returns>Список всех записей.</returns>
    public List<T> GetAll()
    {
        string query = $"SELECT * FROM {typeof(T).Name}s";
        using (var command = _dbConnection.CreateCommand())
        {
            var result = new List<T>();
            command.CommandText = query;
            _dbConnection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(Map(reader));
                }
                _dbConnection.Close();
                return result;
            }
        }
    }

    /// <summary>
    /// Получает запись по идентификатору.
    /// </summary>
    /// <param name="Id">Идентификатор записи.</param>
    /// <returns>Запись с указанным идентификатором.</returns>
    public T GetById(int Id)
    {
        string query = $"SELECT * FROM {typeof(T).Name}s WHERE Id = @Id";

        using (var command = _dbConnection.CreateCommand())
        {
            var result = new T();
            command.CommandText = query;
            var parameter = command.CreateParameter();
            parameter.ParameterName = "@Id";
            parameter.Value = Id;
            command.Parameters.Add(parameter);

            _dbConnection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    result = Map(reader);
                }
            }
            _dbConnection.Close();
            return result;
        }
    }

    /// <summary>
    /// Обновляет существующую запись в базе данных.
    /// </summary>
    /// <param name="entity">Сущность для обновления.</param>
    public void Update(T entity)
    {
        var properties = entity.GetType().GetProperties()
            .Where(p => p.Name != "Id");
        var values = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));
        string query = $"UPDATE {typeof(T).Name}s SET {values} WHERE Id = @Id";
        using (var command = _dbConnection.CreateCommand())
        {
            command.CommandText = query;
            var idParameter = command.CreateParameter();
            idParameter.ParameterName = "@Id";
            idParameter.Value = entity.GetType().GetProperty("Id").GetValue(entity);
            command.Parameters.Add(idParameter);
            foreach (var property in properties)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = '@' + property.Name;
                parameter.Value = property.GetValue(entity) ?? DBNull.Value;
                command.Parameters.Add(parameter);
            }
            _dbConnection.Open();
            command.ExecuteNonQuery();
            _dbConnection.Close();
        }
    }

    public void UpdateMovie(T entity)
    {
        var properties = entity.GetType().GetProperties()
            .Where(p => p.Name != "id");
        var values = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));
        string query = $"UPDATE {typeof(T).Name}s SET {values} WHERE id = @id";
        using (var command = _dbConnection.CreateCommand())
        {
            command.CommandText = query;
            var idParameter = command.CreateParameter();
            idParameter.ParameterName = "@id";
            idParameter.Value = entity.GetType().GetProperty("id").GetValue(entity);
            command.Parameters.Add(idParameter);
            foreach (var property in properties)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = '@' + property.Name;
                parameter.Value = property.GetValue(entity) ?? DBNull.Value;
                command.Parameters.Add(parameter);
            }
            _dbConnection.Open();
            command.ExecuteNonQuery();
            _dbConnection.Close();
        }
    }


    /*public void Update(T entity)
    {
        var properties = entity.GetType().GetProperties()
            .Where(p => p.Name != "Id" && p.Name != "MovieId");
        var values = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));
        string query = $"UPDATE {typeof(T).Name}s SET {values} WHERE ";

        if (entity.GetType().GetProperty("Id") != null)
        {
            query += "Id = @Id";
        }
        else if (entity.GetType().GetProperty("MovieId") != null)
        {
            query += "MovieId = @MovieId";
        }

        using (var command = _dbConnection.CreateCommand())
        {
            command.CommandText = query;

            if (entity.GetType().GetProperty("Id") != null)
            {
                var idParameter = command.CreateParameter();
                idParameter.ParameterName = "@Id";
                idParameter.Value = entity.GetType().GetProperty("Id").GetValue(entity);
                command.Parameters.Add(idParameter);
            }
            else if (entity.GetType().GetProperty("MovieId") != null)
            {
                var movieIdParameter = command.CreateParameter();
                movieIdParameter.ParameterName = "@MovieId";
                movieIdParameter.Value = entity.GetType().GetProperty("MovieId").GetValue(entity);
                command.Parameters.Add(movieIdParameter);
            }

            foreach (var property in properties)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = '@' + property.Name;
                parameter.Value = property.GetValue(entity) ?? DBNull.Value;
                command.Parameters.Add(parameter);
            }

            _dbConnection.Open();
            command.ExecuteNonQuery();
            _dbConnection.Close();
        }
    }*/


    /// <summary>
    /// Удаляет запись по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор записи.</param>
    /// <param name="tableName">Имя таблицы.</param>
    public void Delete(string id, string tableName)
    {
        string query = $"DELETE FROM {tableName} WHERE Id = @Id";
        using (var command = _dbConnection.CreateCommand())
        {
            command.CommandText = query;
            var parameter = command.CreateParameter();
            parameter.ParameterName = "@Id";
            parameter.Value = id;
            command.Parameters.Add(parameter);
            _dbConnection.Open();
            command.ExecuteNonQuery();
            _dbConnection.Close();
        }
    }

    /// <summary>
    /// Удаляет запись на основе сущности.
    /// </summary>
    /// <param name="entity">Сущность для удаления.</param>
    public void Delete(T entity)
    {
        var properties = entity.GetType().GetProperties();
        var condition = string.Join(" AND ", properties.Select(p => $"{p.Name} = @{p.Name}"));
        string query = $"DELETE FROM {typeof(T).Name}s WHERE {condition}";
        using (var command = _dbConnection.CreateCommand())
        {
            command.CommandText = query;
            foreach (var property in properties)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = '@' + property.Name;
                parameter.Value = property.GetValue(entity);
                command.Parameters.Add(parameter);
            }
            _dbConnection.Open();
            command.ExecuteNonQuery();
            _dbConnection.Close();
        }
    }

    /// <summary>
    /// Метод заглушка для фильтрации записей.
    /// </summary>
    /// <returns>Запись, соответствующая условию.</returns>
    public T Where()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Получает первую запись, соответствующую предикату, или значение по умолчанию.
    /// </summary>
    /// <param name="predicate">Предикат для фильтрации.</param>
    /// <returns>Первая запись, соответствующая предикату, или значение по умолчанию.</returns>
    public T FirstOrDefault(Predicate<T> predicate)
    {
        var query = $"SELECT * FROM {typeof(T).Name}s";
        using (var command = _dbConnection.CreateCommand())
        {
            command.CommandText = query;
            _dbConnection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var entity = Map(reader);
                    if (predicate(entity))
                    {
                        return entity;
                    }
                }
            }
            _dbConnection.Close();
        }
        return null;
    }

    /// <summary>
    /// Получает запись по имени.
    /// </summary>
    /// <param name="Name">Имя записи.</param>
    /// <returns>Запись с указанным именем.</returns>
    public T GetByName(string Name)
    {
        string query = $"SELECT * FROM {typeof(T).Name}s WHERE Name = @Name";

        using (var command = _dbConnection.CreateCommand())
        {
            command.CommandText = query;
            var parameter = command.CreateParameter();
            parameter.ParameterName = "@Name";
            parameter.Value = Name;
            command.Parameters.Add(parameter);

            _dbConnection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return Map(reader);
                }
            }
            _dbConnection.Close();
        }

        return null;
    }

    /// <summary>
    /// Получает пользователя по логину.
    /// </summary>
    /// <param name="Login">Логин пользователя.</param>
    /// <returns>Пользователь с указанным логином.</returns>
    public T GetUserByLogin(string Login)
    {
        string query = $"SELECT * FROM {typeof(T).Name}s WHERE Login = @Login";

        using (var command = _dbConnection.CreateCommand())
        {
            command.CommandText = query;
            var parameter = command.CreateParameter();
            parameter.ParameterName = "@Login";
            parameter.Value = Login;
            command.Parameters.Add(parameter);

            _dbConnection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return Map(reader);
                }
            }
            _dbConnection.Close();
        }

        return null;
    }

    /// <summary>
    /// Отображает данные из <see cref="IDataReader"/> на объект типа <typeparamref name="T"/>.
    /// </summary>
    /// <param name="reader">Читатель данных.</param>
    /// <returns>Объект типа <typeparamref name="T"/>.</returns>
    private T Map(IDataReader reader)
    {
        var obj = new T();
        var properties = typeof(T).GetProperties();

        foreach (var property in properties)
        {
            if (reader[property.Name] != DBNull.Value)
            {
                var value = reader[property.Name];
                var propertyType = property.PropertyType;

                // Преобразование значения в правильный тип
                if (propertyType == typeof(int))
                {
                    property.SetValue(obj, Convert.ToInt32(value));
                }
                else if (propertyType == typeof(string))
                {
                    property.SetValue(obj, Convert.ToString(value));
                }
                else if (propertyType == typeof(DateTime))
                {
                    property.SetValue(obj, Convert.ToDateTime(value));
                }
                else if (propertyType == typeof(bool))
                {
                    property.SetValue(obj, Convert.ToBoolean(value));
                }
                else if (propertyType == typeof(double))
                {
                    property.SetValue(obj, Convert.ToDouble(value));
                }
                else if (propertyType == typeof(decimal))
                {
                    property.SetValue(obj, Convert.ToDecimal(value));
                }
                else if (propertyType == typeof(float))
                {
                    property.SetValue(obj, Convert.ToSingle(value));
                }
                else if (propertyType == typeof(long))
                {
                    property.SetValue(obj, Convert.ToInt64(value));
                }
                else if (propertyType == typeof(short))
                {
                    property.SetValue(obj, Convert.ToInt16(value));
                }
                else if (propertyType == typeof(byte))
                {
                    property.SetValue(obj, Convert.ToByte(value));
                }
                else if (propertyType == typeof(char))
                {
                    property.SetValue(obj, Convert.ToChar(value));
                }
                else if (propertyType == typeof(Guid))
                {
                    property.SetValue(obj, Guid.Parse(value.ToString()));
                }
                else
                {
                    property.SetValue(obj, value);
                }
            }
        }

        return obj;
    }

    /// <summary>
    /// Выполняет SQL-запрос и возвращает множество результатов.
    /// </summary>
    /// <param name="predicate">Предикат для фильтрации.</param>
    /// <returns>Множество результатов, соответствующих предикату.</returns>
    public IEnumerable<T> Where(Expression<Func<T, bool>> predicate)
    {
        var sqlQuery = ExpressionParser<T>.BuildSqlQuery(predicate, singleResult: false);
        return ExecuteQueryMultiple(sqlQuery);
    }

    /// <summary>
    /// Выполняет SQL-запрос и возвращает единственный результат.
    /// </summary>
    /// <param name="query">SQL-запрос.</param>
    /// <returns>Единственный результат, соответствующий запросу.</returns>
    private T ExecuteQuerySingle(string query)
    {
        using (var command = _dbConnection.CreateCommand())
        {
            command.CommandText = query;
            _dbConnection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return Map(reader);
                }
            }
            _dbConnection.Close();
        }

        return null;
    }

    /// <summary>
    /// Выполняет SQL-запрос и возвращает множество результатов.
    /// </summary>
    /// <param name="query">SQL-запрос.</param>
    /// <returns>Множество результатов, соответствующих запросу.</returns>
    private IEnumerable<T> ExecuteQueryMultiple(string query)
    {
        var results = new List<T>();
        using (var command = _dbConnection.CreateCommand())
        {
            command.CommandText = query;
            _dbConnection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    results.Add(Map(reader));
                }
            }
            _dbConnection.Close();
        }
        return results;
    }
}
