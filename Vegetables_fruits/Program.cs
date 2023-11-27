using System.Data;
using System.Data.SQLite;

internal class Program
{
    private static async Task Main(string[] args)
    {
        string ConnectionString = "Data Source=Vegetebls_fruits.db;Version=3;";

        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            string CreateTable = @"CREATE TABLE IF NOT EXISTS Vegetebls_fruits (
                                        ID INTEGER PRIMARY KEY AUTOINCREMENT,
                                        Name TEXT,
                                        Type INTEGER,
                                        Color TEXT,
                                        Calories REAL);";

            using (var command = new SQLiteCommand(CreateTable, connection))
            {
                command.ExecuteNonQuery();
            }
            Console.WriteLine("Table created successfully.");

        }
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            try
            {
                await connection.OpenAsync();
                Console.WriteLine("Сonnection is successful\n");

                Console.WriteLine("ALL INFOMATIN:\n\n");
                string code = "SELECT * FROM Vegetebls_fruits";
                using (var command = new SQLiteCommand(code, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        Console.WriteLine("ID | Name | Type | Color | Calories ");
                        Console.WriteLine("------------------------------------");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["ID"]} | {reader["Name"]} | {reader["Type"]} | {reader["Color"]} | {reader["Calories"]}");
                        }
                    }
                    Console.WriteLine("\n\nName:");
                    using (var reader = command.ExecuteReader())
                    {
                        Console.WriteLine("ID | Name");
                        Console.WriteLine("----------");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["ID"]} | {reader["Name"]}");
                        }
                    }
                    Console.WriteLine("\n\nColor:");
                    using (var reader = command.ExecuteReader())
                    {
                        Console.WriteLine("ID | Color");
                        Console.WriteLine("----------");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["ID"]} | {reader["Color"]}");
                        }
                    }
                }

                code = "SELECT MAX(Calories) AS M_Calories FROM Vegetebls_fruits";
                using (var command = new SQLiteCommand(code, connection))
                {
                    var result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        Console.WriteLine($"\nMaximum Calories: {result}");
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }
                }

                code = "SELECT MIN(Calories) AS M_Calories FROM Vegetebls_fruits";
                using (var command = new SQLiteCommand(code, connection))
                {
                    var result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        Console.WriteLine($"\nMinimum Calories: {result}");
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }
                }

                code = "SELECT AVG(Calories) AS AverageCalories FROM Vegetebls_fruits;";
                using (var command = new SQLiteCommand(code, connection))
                {
                    var result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        Console.WriteLine($"\nAverage Calories: {result}");
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }
                }

                code = "SELECT COUNT(*) AS VegetableCount FROM Vegetebls_fruits WHERE Type = 0;";
                using (var command = new SQLiteCommand(code, connection))
                {
                    var result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        Console.WriteLine($"\nCount of vegetables: {result}");
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }
                }

                code = "SELECT COUNT(*) AS FruitCount FROM Vegetebls_fruits WHERE Type = 1;";
                using (var command = new SQLiteCommand(code, connection))
                {
                    var result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        Console.WriteLine($"\nCount of fruits: {result}");
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }
                }
                Console.WriteLine("\n");
                code = @"SELECT Color, COUNT(*) AS Count FROM Vegetebls_fruits WHERE Color = 'Red';";
                using (var command = new SQLiteCommand(code, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Count of red fruits and vegetables{reader["Count"]}");
                        }
                    }
                }
                Console.WriteLine("\n");
                code = @"SELECT Color, COUNT(*) AS Count FROM Vegetebls_fruits GROUP BY Color;";
                using (var command = new SQLiteCommand(code, connection))
                {
                    Console.WriteLine("Color | Count");
                    Console.WriteLine("--------------------");
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["Color"]} | {reader["Count"]}");
                        }
                    }
                }
                Console.WriteLine("\nCalories < 30");
                code = @"SELECT * FROM Vegetebls_fruits WHERE Calories < 30;";
                using (var command = new SQLiteCommand(code, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        Console.WriteLine("ID | Name | Type | Color | Calories ");
                        Console.WriteLine("------------------------------------");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["ID"]} | {reader["Name"]} | {reader["Type"]} | {reader["Color"]} | {reader["Calories"]}");
                        }
                    }
                }
                Console.WriteLine("\nCalories > 70");
                code = @"SELECT * FROM Vegetebls_fruits WHERE Calories > 70;";
                using (var command = new SQLiteCommand(code, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        Console.WriteLine("ID | Name | Type | Color | Calories ");
                        Console.WriteLine("------------------------------------");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["ID"]} | {reader["Name"]} | {reader["Type"]} | {reader["Color"]} | {reader["Calories"]}");
                        }
                    }
                }

                Console.WriteLine("\nCalories in diapasone (30; 100)");
                code = @"SELECT * FROM Vegetebls_fruits WHERE Calories BETWEEN 30 AND 100;";
                using (var command = new SQLiteCommand(code, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        Console.WriteLine("ID | Name | Type | Color | Calories ");
                        Console.WriteLine("------------------------------------");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["ID"]} | {reader["Name"]} | {reader["Type"]} | {reader["Color"]} | {reader["Calories"]}");
                        }
                    }
                }

                Console.WriteLine("\nObjects where color in (Yellow, Red)");
                code = @"SELECT * FROM Vegetebls_fruits WHERE Color IN ('Yellow', 'Red');";
                using (var command = new SQLiteCommand(code, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        Console.WriteLine("ID | Name | Type | Color | Calories ");
                        Console.WriteLine("------------------------------------");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["ID"]} | {reader["Name"]} | {reader["Type"]} | {reader["Color"]} | {reader["Calories"]}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await connection.CloseAsync();
                Console.WriteLine("Connection is failed");
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    await connection.CloseAsync();
                    Console.WriteLine("Connection is disconnected");
                }
            }
        }
    }
}