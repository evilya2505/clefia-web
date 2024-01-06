import React from "react";
import textareaStyles from "./textarea.module.css";
import { UseFormRegister } from "react-hook-form";

interface TextareaProps {
  name: string;
  register: UseFormRegister<any>;
  errorMessage: string | undefined;
}

function Textarea({ name, register, errorMessage }: TextareaProps) {
  return (
    <>
      <input
        {...register(name)}
        type="text"
        id={name}
        name={name}
        className={`${textareaStyles.textarea}`}
      />
      <label className={textareaStyles.error}>{errorMessage || " "}</label>
    </>
  );
}

export default Textarea;
