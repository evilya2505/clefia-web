public class EncryptionRequest
{
    public string Type { get; set; } // "ENC" или "DEC"
    public string Text { get; set; }
    public string Key { get; set; }
    public bool IsTextHex { get; set; }
    public bool IsKeyHex { get; set; }

}
