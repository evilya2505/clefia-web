import React from "react";
import * as yup from "yup";
import demo from "./demo.module.css";
import { ENCRYPTION_MODE, DECRYPTION_MODE } from "../../constants/constants";
import Checkbox from "../checkbox/checkbox";
import { InputFormValues } from "../../types/types";
import { yupResolver } from "@hookform/resolvers/yup";
import { useForm } from "react-hook-form";
import Table from "../table/table";
import { useDispatch, useSelector } from "../../services/hooks";
import { encryptDecrypt } from "../../services/actions/ecryption";
import CustomInput from "../textarea/customInput";
function Demo() {
  const [isTextHex, setIsTextHex] = React.useState(false);
  const [isKeyHex, setIsKeyHex] = React.useState(false);
  const dispatch = useDispatch();

  const inputSchema = yup.object().shape({
    ENC: yup.bool().required("The field is required."),
    DEC: yup.bool().required("The field is required."),
    isTextHex: yup.bool().required("The field is required."),
    isKeyHex: yup.bool().required("The field is required."),
    text: isTextHex
      ? yup
          .string()
          .required("The field is required.")
          .test(
            "exactLength",
            "The text must be exactly 8 characters long.",
            (value) => value?.replaceAll(" ", "").length === 8
          )
      : yup
          .string()
          .required("Поле обязательное.")
          .test(
            "exactLength",
            "The text must be exactly 32 characters long.",
            (value) => value?.replaceAll(" ", "").length === 32
          )
          .test(
            "isHexadecimal",
            "The string must be in hexadecimal format.",
            (value) => /^[0-9a-fA-F]+$/.test(value?.replaceAll(" ", ""))
          ),
    key: isKeyHex
      ? yup
          .string()
          .required("The field is required.")
          .test(
            "exactLength",
            "The text must be exactly 8 characters long.",
            (value) => value?.replaceAll(" ", "").length === 8
          )
      : yup
          .string()
          .required("Поле обязательное.")
          .test(
            "exactLength",
            "The text must be exactly 32 characters long.",
            (value) => value?.replaceAll(" ", "").length === 32
          )
          .test(
            "isHexadecimal",
            "The string must be in hexadecimal format.",
            (value) => /^[0-9a-fA-F]+$/.test(value?.replaceAll(" ", ""))
          ),
  });

  const form: any = useForm<InputFormValues>({
    resolver: yupResolver(inputSchema),
    mode: "onChange",
    defaultValues: {
      ENC: true,
      DEC: false,
      text: "",
      key: "",
      isKeyHex: true,
      isTextHex: true,
    },
  });

  const { setValue, watch, register, handleSubmit, formState } = form;
  const { errors, isValid } = formState;
  const result = useSelector((state) => state.encryption.result);

  const isTextHexValue = watch("isTextHex");
  const isKeyHexValue = watch("isKeyHex");
  const isDec = watch("DEC");
  const isEnc = watch("ENC");

  React.useEffect(() => {
    if (isDec) setValue("ENC", false);
  }, [isDec, setValue]);

  React.useEffect(() => {
    if (isEnc) setValue("DEC", false);
  }, [isEnc, setValue]);

  React.useEffect(() => {
    setIsTextHex((prev) => !prev);
    setValue("text", watch("text"));
  }, [isTextHexValue, setValue, watch]);

  React.useEffect(() => {
    setIsKeyHex((prev) => !prev);
    setValue("key", watch("key"));
  }, [isKeyHexValue, setValue, watch]);

  const onSubmit = (data: InputFormValues) => {
    const modifiedKey = data.key.replaceAll(" ", "").toLocaleLowerCase();
    const modifiedText = data.text.replaceAll(" ", "").toLocaleLowerCase();
    dispatch(
      encryptDecrypt({
        type: data.ENC ? ENCRYPTION_MODE : DECRYPTION_MODE,
        isKeyHex: data.isKeyHex,
        isTextHex: data.isTextHex,
        key: modifiedKey,
        text: modifiedText,
      })
    );

    setValue("key", modifiedKey);
    setValue("text", modifiedText);
  };

  return (
    <section className={demo.section}>
      <form className={demo.form} noValidate onSubmit={handleSubmit(onSubmit)}>
        <fieldset className={demo.fieldset}>
          <Checkbox
            label="Encryption"
            id="ENC"
            register={register}
            isChecked={isEnc}
          />

          <Checkbox
            label="Decryption"
            id="DEC"
            register={register}
            isChecked={isDec}
          />
        </fieldset>
        <fieldset className={demo.fieldsetInputs}>
          <div className={demo.inputContainer}>
            <label className={`${demo.label} ${demo.inputLabel}`}>
              {isEnc ? "Plain text" : "Cipher text"}
            </label>
            <Checkbox
              label="HEX"
              id="isTextHex"
              register={register}
              isChecked={isTextHexValue}
            />
            <CustomInput
              name="text"
              register={register}
              errorMessage={errors.text?.message}
            />
            <label className={demo.additionalLabel}>
              {isTextHexValue === true
                ? "*128 bit only"
                : "*8 symbols bit only"}
            </label>
          </div>

          <div className={demo.inputContainer}>
            <label className={`${demo.label} ${demo.inputLabel}`}>Key</label>
            <Checkbox
              label="HEX"
              id="isKeyHex"
              register={register}
              isChecked={isKeyHexValue}
            />
            <CustomInput
              name="key"
              register={register}
              errorMessage={errors.key?.message}
            />
            <label className={demo.additionalLabel}>
              {isKeyHexValue === true ? "*128 bit only" : "*8 symbols bit only"}
            </label>
          </div>
          <button
            type="submit"
            disabled={!isValid}
            className={`${demo.button} ${
              (errors.key?.message || errors.text?.message) && demo.buttonError
            }`}
          >
            {isEnc ? "Encrypt" : "Decrypt"}
          </button>
        </fieldset>
      </form>
      {result !== null && (
        <div className={demo.resultContainer}>
          <div className={demo.resultText}>
            <h2 className={demo.text}>
              {isEnc ? "Encrypted" : "Decrypted"}
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
