public class Produk
{
    public int id_produk { get; set; }
    public string kode_produk { get; set; }
    public string nama_produk { get; set; }
    public int harga { get; set; }
    public int stok { get; set; }
    public int status { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
}