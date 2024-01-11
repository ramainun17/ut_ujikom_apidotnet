using System.Data;
using MySql.Data.MySqlClient;

public class DbProduk
{
    private readonly string connectionString;
    private readonly MySqlConnection _connection;
    public DbProduk(IConfiguration configuration){
        connectionString = configuration.GetConnectionString("DefaultConnection");
        _connection = new MySqlConnection(connectionString);
    }

    //CRUD PRODUK
    public List<Produk> GetAllProduks(){
        List<Produk> prolist = new List<Produk>();
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM produks";
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Produk pro = new Produk{
                            id_produk = Convert.ToInt32(reader["Id_produk"]),
                            kode_produk = reader["Kode_produk"].ToString(),
                            nama_produk = reader["Nama_produk"].ToString(),
                            harga = Convert.ToInt32(reader["Harga"]),
                            stok = Convert.ToInt32(reader["Stok"]),
                            status = Convert.ToInt32(reader["Status"]),
                            created_at = Convert.ToDateTime(reader["Created_at"]),
                            updated_at = Convert.ToDateTime(reader["Updated_at"]),
                        };
                        prolist.Add(pro);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return prolist;
    }
    public Produk GetProdukById(int id_produk){
        Produk pro = null;

        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM produks WHERE id_produk = @Id_produk";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Menggunakan parameterized query untuk mencegah SQL injection
                    command.Parameters.AddWithValue("@Id_produk", id_produk);

                    connection.Open();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pro = new Produk
                            {
                                id_produk = Convert.ToInt32(reader["Id_produk"]),
                                kode_produk = reader["Kode_produk"].ToString(),
                                nama_produk = reader["Nama_produk"].ToString(),
                                harga = Convert.ToInt32(reader["Harga"]),
                                stok = Convert.ToInt32(reader["Stok"]),
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

        return pro;
    }
    public int CreateProduk(Produk pro){
        using (MySqlConnection connection = _connection)
        {
            pro.created_at = DateTime.Now;
            pro.updated_at = DateTime.Now;
            string query = "INSERT INTO produks (kode_produk, nama_produk, harga, stok, status, created_at, updated_at ) VALUES (@Kode_produk, @Nama_produk, @Harga, @Stok, @Status, @Created_at, @Updated_at)";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Kode_produk", pro.kode_produk);
                command.Parameters.AddWithValue("@Nama_produk", pro.nama_produk);
                command.Parameters.AddWithValue("@Harga", pro.harga);
                command.Parameters.AddWithValue("@Stok", pro.stok);
                command.Parameters.AddWithValue("@Status", pro.status);
                command.Parameters.AddWithValue("@Created_at", pro.created_at);
                command.Parameters.AddWithValue("@Updated_at", pro.updated_at);
                connection.Open();
                return command.ExecuteNonQuery();
            }
        }
    }
    public int UpdateProduk(int id_produk, Produk pro){
        using (MySqlConnection connection = _connection)
        {
            pro.updated_at = DateTime.Now;
            string query = "UPDATE produks SET kode_produk = @Kode_produk, nama_produk = @Nama_produk, harga = @Harga, stok = @Stok, status = @Status, updated_at = @Updated_at WHERE id_produk = @Id_produk";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Kode_produk", pro.kode_produk);
                command.Parameters.AddWithValue("@Nama_produk", pro.nama_produk);
                command.Parameters.AddWithValue("@Harga", pro.harga);
                command.Parameters.AddWithValue("@Stok", pro.stok);
                command.Parameters.AddWithValue("@Status", pro.status);
                command.Parameters.AddWithValue("@Updated_at", pro.updated_at);
                command.Parameters.AddWithValue("@Id_produk", id_produk);

                connection.Open();
                return command.ExecuteNonQuery();
            }
        }
    }
    public int DeleteProduk(int id_produk){
        using (MySqlConnection connection = _connection)
        {
            string query = "DELETE FROM produks WHERE id_produk = @Id_produk";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id_produk", id_produk);

                connection.Open();
                return command.ExecuteNonQuery();
            }
        }
    }

}