import React from "react";
import checkbox from "./checkbox.module.css";

interface CheckboxProps {
  label: string;
  name: string;
  isChecked: boolean;
  handleCheckboxesChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
}

function Checkbox({
  label,
  name,
  isChecked,
  handleCheckboxesChange,
}: CheckboxProps) {
  return (
    <div className={checkbox.checkboxContainer}>
      <input
        className={checkbox.checkbox}
        type="checkbox"
        id={name}
        name={name}
        checked={isChecked}
        onChange={handleCheckboxesChange}
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
