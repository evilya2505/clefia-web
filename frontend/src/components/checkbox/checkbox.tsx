import React from "react";
import checkbox from "./checkbox.module.css";
import { UseFormRegister } from "react-hook-form";

interface CheckboxProps {
  label: string;
  id: string;
  isChecked: boolean;
  register: UseFormRegister<any>;
}

function Checkbox({ label, id, isChecked, register }: CheckboxProps) {
  return (
    <div className={checkbox.checkboxContainer}>
      <input
        className={checkbox.checkbox}
        type="checkbox"
        id={id}
        {...register(id)}
      />
      <span
        className={`${checkbox.customCheckbox} ${
          isChecked ? `${checkbox.customCheckboxChecked}` : ""
        }`}
      ></span>
      <label className={checkbox.label}>{label}</label>
    </div>
  );
}

export default Checkbox;
