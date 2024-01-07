import { AppDispatch } from "../store";
import {
  request,
  requestFailed,
  requestSuccess,
  setMode,
} from "../reducers/encryption";
import mainApi from "../../utils/api";
import { RequestValues } from "../../types/types";

export const encryptDecrypt = (requestData: RequestValues) => {
  return function (dispatch: AppDispatch) {
    dispatch(request());

    mainApi
      .request(requestData)
      .then((data) => {
        console.log(requestData.type);
        if (requestData.type !== undefined) dispatch(setMode(requestData.type));
        dispatch(requestSuccess(data));
      })
      .catch((err) => {
        console.log(err);
        dispatch(requestFailed());
      });
  };
};
