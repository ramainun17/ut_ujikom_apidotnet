public class Layanan
{
    public int id_layanan { get; set; }
    public string kode_layanan { get; set; }
    public string nama_layanan { get; set; }
    public string keterangan { get; set; }
    public int harga { get; set; }
    public int status { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
}