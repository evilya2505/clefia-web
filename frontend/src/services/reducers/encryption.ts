import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import { ResultType, InputFormValues } from "../../types/types";

export interface TEncryptionListState {
  requestData: InputFormValues | null;
  result: ResultType | null;
  request: boolean;
  requestFailed: boolean;
}

export const initialState: TEncryptionListState = {
  requestData: null,
  result: null,
  request: false,
  requestFailed: false,
};

const encryptionSlice = createSlice({
  name: "encryption",
  initialState,
  reducers: {
    requestSuccess(
      state: TEncryptionListState,
      action: PayloadAction<ResultType>
    ) {
      state.result = action.payload;
      state.request = false;
      state.requestFailed = false;
    },
    request(state: TEncryptionListState) {
      state.request = true;
      state.requestFailed = false;
    },
    requestFailed(state: TEncryptionListState) {
      state.request = false;
      state.requestFailed = true;
    },
  },
});
export const { request, requestSuccess, requestFailed } =
  encryptionSlice.actions;

export default encryptionSlice.reducer;
