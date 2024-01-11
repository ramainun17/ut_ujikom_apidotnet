using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;

[Route("api/[controller]")]
[ApiController]
public class ProdukController : ControllerBase
{
    private readonly DbProduk _dbProduk;
    Response response = new Response();
    public ProdukController(IConfiguration configuration){
        _dbProduk = new DbProduk(configuration);
    }

    //GET: api/Produk/get
    [HttpGet("get")] 
    public IActionResult GetProduks(){
        try
        {
            response.status = 200;
            response.message = "Success";
            response.data = _dbProduk.GetAllProduks();
        }
        catch (Exception ex)
        {
            
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }
    //GET: api/Produk/getById
    [HttpGet("getById")] 
    public IActionResult GetProdukById(int id_produk){
        try
        {
            response.status = 200;
            response.message = "Success";
            response.data = _dbProduk.GetProdukById(id_produk);
        }
        catch (Exception ex)
        {
            
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }
    //POST: api/Produk/create
    [HttpPost("create")]
    public IActionResult CreateProduk([FromBody] Produk pro){
        try
        {
            response.status = 200;
            response.message = "Success";
            _dbProduk.CreateProduk(pro);
        }
        catch (Exception ex)
        {
            
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }
    //PUT: api/Produk/update
    [HttpPut("update")]
    public IActionResult UpdateProduk(int id_produk, [FromBody] Produk pro){
        try
        {
            response.status = 200;
            response.message = "Success";
            _dbProduk.UpdateProduk(id_produk, pro);
        }
        catch (Exception ex)
        {
            
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }
    //DELETE: api/Produk/delete/id
    [HttpDelete("delete/{id_produk}")]
    public IActionResult DeleteProduk(int id_produk){
        try
        {
            response.status = 200;
            response.message = "Success";
            _dbProduk.DeleteProduk(id_produk);
        }
        catch (Exception ex)
        {
            
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }

}