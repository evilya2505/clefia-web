    public class Round
    {
        public int RoundNumber { get; set; }
        public string Input { get; set; }
        public string RoundKey { get; set; }
        public string AfterKeyAdd { get; set; }
        public string AfterS { get; set; }
        public string AfterM { get; set; }
    }    
    public class Response
    {
        public string PlainText { get; set; }
        public string InitialWhiteningKey { get; set; }
        public string AfterInitialWhitening { get; set; }
        public List<Round> Rounds { get; set; }
        public string Output { get; set; }
        public string FinalWhiteningKey { get; set; }
        public string AfterFinalWhitening { get; set; }
        public string CipherText { get; set; }

        public Response()
        {
            Rounds = new List<Round>(); // Инициализация списка
        }
    }