public class EncryptionRepository
{
    public Response Encrypt(string input, string key)
    {
        return CLEFIACipherTest.ReturnEnctyptionResponse(key, input);
    }

    public Response Decrypt(string input, string key)
    {
        return CLEFIACipherTest.ReturnDecryptedResult(key, input);
    }
}
