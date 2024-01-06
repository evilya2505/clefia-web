import * as yup from "yup";
import { EncryptionType } from "../types/types";

export const inputSchemaHex = yup.object().shape({
  type: yup.mixed<EncryptionType>(),
  text: yup
    .string()
    .required("Поле обязательное. text")
    .test("exactLength", "Длина текста должна быть ровно 32 символа", (value) =>
      value ? value.length === 32 : true
    )
    .matches(
      /^[0-9a-fA-F]+$/,
      "Строка должна быть в 16-ричной системе счисления"
    ),
  key: yup
    .string()
    .required("Поле обязательное. ключ")
    .test("exactLength", "Длина текста должна быть ровно 32 символа", (value) =>
      value ? value.length === 32 : true
    )
    .matches(
      /^[0-9a-fA-F]+$/,
      "Строка должна быть в 16-ричной системе счисления"
    ),
});

export const inputSchema = yup.object().shape({
  type: yup.mixed<EncryptionType>().required("Поле обязательное."),
  text: yup
    .string()
    .required("Поле обязательное.")
    .test("exactLength", "Длина текста должна быть ровно 8 символов", (value) =>
      value ? value.length === 8 : true
    ),
  key: yup
    .string()
    .required("Поле обязательное.")
    .test("exactLength", "Длина текста должна быть ровно 8 символов", (value) =>
      value ? value.length === 8 : true
    ),
});
