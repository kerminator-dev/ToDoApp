using Microsoft.Data.Sqlite;
using ToDoList.Models;
using ToDoList.Models.ViewModels;

namespace ToDoList.DBContexts
{
    public class DBManager
    {
        private const string ConnectionString = "Data Source=db.sqlite";
        private readonly SqliteConnection _connection;
        private static DBManager? _instance;

        public static DBManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DBManager();

                return _instance;
            }
        }

        private DBManager()
        {
            _connection = new SqliteConnection(ConnectionString);
        }

        public void InsertToDo(ToDoItem toDoItem)
        {
            if (string.IsNullOrEmpty(toDoItem.Name))
                return;

            using (var command = _connection.CreateCommand())
            {
                _connection.Open();
                command.CommandText = $"INSERT INTO todo (name) VALUES ('{toDoItem.Name}')";
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    _connection.Close();
                }
            }
        }

		internal ToDoItem GetToDoById(int id)
		{
            ToDoItem toDoItem = new ToDoItem();

            using (var command = _connection.CreateCommand())
            {
                _connection.Open();

                try
                {
                    command.CommandText = $"SELECT * FROM todo WHERE id = '{id}'";
                    
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            toDoItem.Id = reader.GetInt32(0);
                            toDoItem.Name = reader.GetString(1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
                finally
                {
                    _connection.Close();
                }
            }

            return toDoItem;
        }

		public void DeleteToDo(int id)
		{
            using (var command = _connection.CreateCommand())
			{
                _connection.Open();

                try
                {
                    command.CommandText = $"DELETE FROM todo WHERE id = '{id}'";
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
				finally
				{
                    _connection.Close();
				}
			}
		}

        public void Update(ToDoItem toDoItem)
		{
            using (var command = _connection.CreateCommand())
            {
                _connection.Open();

                try
                {
                    command.CommandText = $"UPDATE todo SET name = '{toDoItem.Name}' WHERE id = '{toDoItem.Id}'";
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
                finally
                {
                    _connection.Close();
                }
            }
        }

        public ToDoViewModel GetAllToDos()
        {
            List<ToDoItem> todoItems = new List<ToDoItem>();

            using (var command = _connection.CreateCommand())
            {
                _connection.Open();
                command.CommandText = $"SELECT * FROM todo";
                try
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                todoItems.Add
                                (
                                    new ToDoItem()
                                    {
                                        Id = reader.GetInt32(0),
                                        Name = reader.GetString(1)
                                    }
                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    _connection.Close();
                }

                return new ToDoViewModel(todoItems);
            }
        }
    }
}
