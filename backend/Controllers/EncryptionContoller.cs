using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/clefia")]
public class CipherController : ControllerBase
{
    private readonly EncryptionRepository repository;

    public CipherController()
    {
        repository = new EncryptionRepository();
    }

    [HttpGet]
    public string Test()
    {
        return "test";
    }

    [HttpPost]
    public ActionResult<string> ProcessEncryptionRequest(EncryptionRequest request)
    {
        if (request.Type == "ENC")
        {
            return Ok(repository.Encrypt(request.IsTextHex, request.IsKeyHex, request.Text, request.Key));
        }
        else if (request.Type == "DEC")
        {
            return Ok(repository.Decrypt(request.IsTextHex, request.IsKeyHex,request.Text, request.Key));
        }
        else
        {
            return BadRequest("Invalid type specified.");
        }
    }
}
