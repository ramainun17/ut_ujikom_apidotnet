using System.Data;
using MySql.Data.MySqlClient;

public class DbLayanan
{
    private readonly string connectionString;
    private readonly MySqlConnection _connection;
    public DbLayanan(IConfiguration configuration){
        connectionString = configuration.GetConnectionString("DefaultConnection");
        _connection = new MySqlConnection(connectionString);
    }

    //CRUD LAYANAN
    public List<Layanan> GetAllLayanans(){
        List<Layanan> lynlist = new List<Layanan>();
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM layanans";
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Layanan lyn = new Layanan{
                            id_layanan = Convert.ToInt32(reader["Id_layanan"]),
                            kode_layanan = reader["Kode_layanan"].ToString(),
                            nama_layanan = reader["Nama_layanan"].ToString(),
                            keterangan = reader["Keterangan"].ToString(),
                            harga = Convert.ToInt32(reader["Harga"]),
                            status = Convert.ToInt32(reader["Status"]),
                            created_at = Convert.ToDateTime(reader["Created_at"]),
                            updated_at = Convert.ToDateTime(reader["Updated_at"]),
                        };
                        lynlist.Add(lyn);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return lynlist;
    }
    public Layanan GetLayananById(int id_layanan){
        Layanan lyn = null;

        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM layanans WHERE id_layanan = @Id_layanan";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Menggunakan parameterized query untuk mencegah SQL injection
                    command.Parameters.AddWithValue("@Id_layanan", id_layanan);

                    connection.Open();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lyn = new Layanan
                            {
                                id_layanan = Convert.ToInt32(reader["Id_layanan"]),
                                kode_layanan = reader["Kode_layanan"].ToString(),
                                nama_layanan = reader["Nama_layanan"].ToString(),
                                keterangan = reader["Keterangan"].ToString(),
                                harga = Convert.ToInt32(reader["Harga"]),
                                status = Convert.ToInt32(reader["Status"]),
                                created_at = Convert.ToDateTime(reader["Created_at"]),
                            updated_at = Convert.ToDateTime(reader["Updated_at"]),
                            };
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Handle the exception (e.g., log or return to caller)
            Console.WriteLine($"Error: {ex.Message}");
        }

        return lyn;
    }
    public int CreateLayanan(Layanan lyn){
        using (MySqlConnection connection = _connection)
        {
            lyn.created_at = DateTime.Now;
            lyn.updated_at = DateTime.Now;
            string query = "INSERT INTO layanans (kode_layanan, nama_layanan, keterangan, harga, status, created_at, updated_at ) VALUES (@Kode_layanan, @Nama_layanan, @Keterangan, @Harga, @Status, @Created_at, @Updated_at)";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Kode_layanan", lyn.kode_layanan);
                command.Parameters.AddWithValue("@Nama_layanan", lyn.nama_layanan);
                command.Parameters.AddWithValue("@Keterangan", lyn.keterangan);
                command.Parameters.AddWithValue("@Harga", lyn.harga);
                command.Parameters.AddWithValue("@Status", lyn.status);
                command.Parameters.AddWithValue("@Created_at", lyn.created_at);
                command.Parameters.AddWithValue("@Updated_at", lyn.updated_at);
                connection.Open();
                return command.ExecuteNonQuery();
            }
        }
    }
    public int UpdateLayanan(int id_layanan, Layanan lyn){
        using (MySqlConnection connection = _connection)
        {
            lyn.updated_at = DateTime.Now;
            string query = "UPDATE layanans SET kode_layanan = @Kode_layanan, nama_layanan = @Nama_layanan, keterangan = @Keterangan, harga = @Harga, status = @Status, updated_at = @Updated_at WHERE id_layanan = @Id_layanan";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Kode_layanan", lyn.kode_layanan);
                command.Parameters.AddWithValue("@Nama_layanan", lyn.nama_layanan);
                command.Parameters.AddWithValue("@Keterangan", lyn.keterangan);
                command.Parameters.AddWithValue("@Harga", lyn.harga);
                command.Parameters.AddWithValue("@Status", lyn.status);
                command.Parameters.AddWithValue("@Updated_at", lyn.updated_at);
                command.Parameters.AddWithValue("@Id_layanan", id_layanan);

                connection.Open();
                return command.ExecuteNonQuery();
            }
        }
    }
    public int DeleteLayanan(int id_layanan){
        using (MySqlConnection connection = _connection)
        {
            string query = "DELETE FROM layanans WHERE id_layanan = @Id_layanan";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id_layanan", id_layanan);

                connection.Open();
                return command.ExecuteNonQuery();
            }
        }
    }

}