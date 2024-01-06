export type EncryptionType = "ENC" | "DEC";
export type DirectionType = "FORWARD" | "BACKWARD";

export type InputFormValues = {
  type?: EncryptionType;
  text: string;
  key: string;
};

export type RoundType = {
  roundNumber: number;
  input: string;
  roundKey: string;
  afterKeyAdd: string;
  afterS: string;
  afterM: string;
};

export type ResultType = {
  plainText: string;
  initialWhiteningKey: string;
  afterInitialWhitening: string;
  rounds: Array<RoundType>;
  output: string;
  finalWhiteningKey: string;
  afterFinalWhitening: string;
  cipherText: string;
};
