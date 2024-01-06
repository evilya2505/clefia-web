import { AppDispatch } from "../store";
import { request, requestFailed, requestSuccess } from "../reducers/encryption";
import mainApi from "../../utils/api";
import { InputFormValues } from "../../types/types";

export const encryptDecrypt = (requestData: InputFormValues) => {
  return function (dispatch: AppDispatch) {
    dispatch(request());

    mainApi
      .request(requestData)
      .then((data) => {
        console.log(data);
        dispatch(requestSuccess(data));
      })
      .catch((err) => {
        console.log(err);
        dispatch(requestFailed());
      });
  };
};
