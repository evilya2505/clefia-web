export type EncryptionType = "ENC" | "DEC";
export type DirectionType = "FORWARD" | "BACKWARD";

export type InputFormValues = {
  ENC: boolean;
  DEC: boolean;
  isTextHex: boolean;
  isKeyHex: boolean;
  text: string;
  key: string;
};

export type RequestValues = {
  type: EncryptionType;
  isTextHex: boolean;
  isKeyHex: boolean;
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
