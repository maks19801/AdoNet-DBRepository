using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DbExample
{
    public class Repository<T>: IRepository<T>, IDisposable where T :BaseEntity, new( )
    {

        private readonly SqlConnection _connection;
        private readonly string _tableName = $"[{typeof(T).Name}s]";

        public Repository()
        {
            var connectionString = @"Data Source = USER-PC; Initial Catalog = Library; Integrated Security = True;";
            _connection = new SqlConnection(connectionString);
            _connection.Open();
        }

        public void Add(T entity)
        {
            var properties = typeof(T).GetProperties().Where(p => p.Name != "Id");

            var propNames = properties.Select(p => p.Name);
            var columnNames = string.Join(',', propNames);
            var paramNames = string.Join(',', propNames.Select(_ => $"@{_}"));

            var propValues = properties.Select(p => p.GetValue(entity));
            var commandText = $"INSERT INTO {_tableName} ({columnNames}) VALUES ({paramNames})";
            var command = GetCommand(commandText);
            foreach(var property in properties)
            {
                command.Parameters.AddWithValue(property.Name, property.GetValue(entity));
            }
            command.ExecuteNonQuery();

        }

        public void Delete(int id)
        {
            var commandText = $"DELETE FROM {_tableName} WHERE id = @id";
            var command = GetCommand(commandText);
            command.Parameters.AddWithValue("id", id);
            command.ExecuteNonQuery();
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public IEnumerable<T> Get()
        {
            var results = new List<T>();
            var commandText = $"SELECT * FROM {_tableName}";
            var command = GetCommand(commandText);

            using var reader = command.ExecuteReader();

            var schema = reader.GetColumnSchema();
            while (reader.Read())
            {
                results.Add(Create(schema, reader));
            }
            return results;
        }

        private static T Create(ReadOnlyCollection<DbColumn> schema, SqlDataReader reader)
        {
            var result = new T();
            for (int colIdx = 0; colIdx < schema.Count; colIdx++)
            {
                var columnName = schema[colIdx].ColumnName;
                var columnType = schema[colIdx].DataType;
                var columnValue = reader[colIdx];
                var value = Convert.ChangeType(columnValue, columnType);

                var property = typeof(T).GetProperties().FirstOrDefault(p => p.Name == columnName);
                property?.SetValue(result, value);
            }

            return result;
        }

        public T Get(int id)
        {
            var commandText = $"SELECT * FROM {_tableName} WHERE Id = @id";
            var command = GetCommand(commandText);
            command.Parameters.AddWithValue("id", id);

            using var reader = command.ExecuteReader();

            var schema = reader.GetColumnSchema();

            if (!reader.Read()) throw new ArgumentOutOfRangeException();
            return Create(schema, reader);
        }

       
        public void Update(T entity)
        {
            var properties = typeof(T).GetProperties();
            var propNames = properties.Where(p => p.Name != "Id").Select(p =>p.Name);
            var setPropString = propNames.Select(n => $"{n} = @{n}");
            var setProps = string.Join(',', setPropString);

            var commandText = $"UPDATE {_tableName} SET {setProps} WHERE Id = @Id";
            var command = GetCommand(commandText);
            foreach(var property in properties)
            {
                command.Parameters.AddWithValue(property.Name, property.GetValue(entity));
            }
            command.ExecuteNonQuery();
        }

        private SqlCommand GetCommand(string commandText)
        {
            return new SqlCommand(commandText, _connection);
        }

        Book IRepository<T>.Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
