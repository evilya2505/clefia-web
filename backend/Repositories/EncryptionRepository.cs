public class EncryptionRepository
{
    public Response Encrypt(bool isTextHex, bool isKeyHex, string input, string key)
    {
        return CLEFIACipherTest.ReturnEnctyptionResponse(isTextHex, isKeyHex, key, input);
    }

    public Response Decrypt(bool isTextHex, bool isKeyHex, string input, string key)
    {
        return CLEFIACipherTest.ReturnDecryptedResult(isTextHex, isKeyHex, key, input);
    }
}
