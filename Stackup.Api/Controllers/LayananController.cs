using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;

[Route("api/[controller]")]
[ApiController]
public class LayananController : ControllerBase
{
    private readonly DbLayanan _dbLayanan;
    Response response = new Response();
    public LayananController(IConfiguration configuration){
        _dbLayanan = new DbLayanan(configuration);
    }

    //GET: api/Layanan/get
    [HttpGet("get")] 
    public IActionResult GetLayanans(){
        try
        {
            response.status = 200;
            response.message = "Success";
            response.data = _dbLayanan.GetAllLayanans();
        }
        catch (Exception ex)
        {
            
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }
    //GET: api/Layanan/getById
    [HttpGet("getById")] 
    public IActionResult GetLayananById(int id_layanan){
        try
        {
            response.status = 200;
            response.message = "Success";
            response.data = _dbLayanan.GetLayananById(id_layanan);
        }
        catch (Exception ex)
        {
            
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }
    //POST: api/Layanan/create
    [HttpPost("create")]
    public IActionResult CreateLayanan([FromBody] Layanan lyn){
        try
        {
            response.status = 200;
            response.message = "Success";
            _dbLayanan.CreateLayanan(lyn);
        }
        catch (Exception ex)
        {
            
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }
    //PUT: api/Layanan/update
    [HttpPut("update")]
    public IActionResult UpdateLayanan(int id_layanan, [FromBody] Layanan lyn){
        try
        {
            response.status = 200;
            response.message = "Success";
            _dbLayanan.UpdateLayanan(id_layanan, lyn);
        }
        catch (Exception ex)
        {
            
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }
    //DELETE: api/Layanan/delete/id
    [HttpDelete("delete/{id_layanan}")]
    public IActionResult DeleteLayanan(int id_layanan){
        try
        {
            response.status = 200;
            response.message = "Success";
            _dbLayanan.DeleteLayanan(id_layanan);
        }
        catch (Exception ex)
        {
            
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }

}