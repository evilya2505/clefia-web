import React from "react";
import textareaStyles from "./customInput.module.css";
import { UseFormRegister } from "react-hook-form";

interface CustomInputProps {
  name: string;
  register: UseFormRegister<any>;
  errorMessage: string | undefined;
}

function CustomInput({ name, register, errorMessage }: CustomInputProps) {
  return (
    <>
      <input
        {...register(name)}
        autoComplete="off"
        type="text"
        id={name}
        name={name}
        className={`${textareaStyles.input}`}
      />
      <label className={textareaStyles.error}>{errorMessage || " "}</label>
    </>
  );
}

export default CustomInput;
