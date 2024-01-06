import React from "react";
import demo from "./demo.module.css";
import { ENCRYPTION_MODE, DECRYPTION_MODE } from "../../constants/constants";
import { EncryptionType } from "../../types/types";
import Checkbox from "../checkbox/checkbox";
import Textarea from "../textarea/textarea";
import { InputFormValues } from "../../types/types";
import { yupResolver } from "@hookform/resolvers/yup";
import { useForm } from "react-hook-form";
import { inputSchemaHex } from "../../validations/inputForm";
import Table from "../table/table";
import { useDispatch, useSelector } from "../../services/hooks";
import { encryptDecrypt } from "../../services/actions/ecryption";

function Demo() {
  const [mode, setMode] = React.useState<EncryptionType>(ENCRYPTION_MODE);
  const [isHexText, setIsHexText] = React.useState<boolean>(true);
  const [isHexKey, setIsHexKey] = React.useState<boolean>(true);
  const dispatch = useDispatch();
  const form: any = useForm<InputFormValues>({
    resolver: yupResolver(inputSchemaHex),
  });

  const { setValue, register, handleSubmit, formState } = form;
  const { errors } = formState;
  const result = useSelector((state) => state.encryption.result);

  React.useEffect(() => {
    setValue("type", mode);
  }, [mode, setValue]);

  const onSubmit = (data: InputFormValues) => {
    dispatch(encryptDecrypt(data));
  };

  const handleCheckboxesChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    const { name } = event.target;

    switch (name) {
      case ENCRYPTION_MODE:
        setMode(ENCRYPTION_MODE);
        break;
      case DECRYPTION_MODE:
        setMode(DECRYPTION_MODE);
        break;
      default:
        break;
    }
  };

  const handleHexCheckboxesChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    const { name } = event.target;

    switch (name) {
      case "text":
        setIsHexText(!isHexText);
        break;
      case "key":
        setIsHexKey(!isHexKey);
        break;
      default:
        break;
    }
  };

  return (
    <section className={demo.section}>
      <form className={demo.form} noValidate onSubmit={handleSubmit(onSubmit)}>
        <fieldset className={demo.fieldset}>
          <Checkbox
            label="Encryption"
            name={ENCRYPTION_MODE}
            isChecked={mode === ENCRYPTION_MODE}
            handleCheckboxesChange={handleCheckboxesChange}
          />

          <Checkbox
            label="Decryption"
            name={DECRYPTION_MODE}
            isChecked={mode === DECRYPTION_MODE}
            handleCheckboxesChange={handleCheckboxesChange}
          />
        </fieldset>
        <fieldset className={demo.fieldsetInputs}>
          <div className={demo.inputContainer}>
            <label className={`${demo.label} ${demo.inputLabel}`}>
              {mode === ENCRYPTION_MODE ? "Plain text" : "Cipher text"}
            </label>
            <Checkbox
              label="HEX"
              name="text"
              isChecked={isHexText}
              handleCheckboxesChange={handleHexCheckboxesChange}
            />
            <Textarea
              name="text"
              register={register}
              errorMessage={errors.text?.message}
            />
            <label className={demo.additionalLabel}>
              {isHexText === true ? "*128 bit only" : "8 symbols bit only"}
            </label>
          </div>

          <div className={demo.inputContainer}>
            <label className={`${demo.label} ${demo.inputLabel}`}>Key</label>
            <Checkbox
              label="HEX"
              name="key"
              isChecked={isHexKey}
              handleCheckboxesChange={handleHexCheckboxesChange}
            />
            <Textarea
              name="key"
              register={register}
              errorMessage={errors.key?.message}
            />
            <label className={demo.additionalLabel}>
              {isHexKey === true ? "*128 bit only" : "8 symbols bit only"}
            </label>
          </div>
          <button type="submit" className={demo.button}>
            {mode === ENCRYPTION_MODE ? "Encrypt" : "Decrypt"}
          </button>
        </fieldset>
      </form>
      {result !== null && (
        <div className={demo.resultContainer}>
          <div className={demo.resultText}>
            {" "}
            <h2 className={demo.text}>
              {mode === ENCRYPTION_MODE ? "Encrypted" : "Decrypted"}
              &nbsp;&nbsp;text:&nbsp;&nbsp;
            </h2>
            <p className={demo.result}>{result?.cipherText}</p>
          </div>

          <Table />
        </div>
      )}
    </section>
  );
}

export default Demo;
