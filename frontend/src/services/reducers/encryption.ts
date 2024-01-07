import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import { ResultType, InputFormValues, EncryptionType } from "../../types/types";

export interface TEncryptionListState {
  mode: EncryptionType;
  requestData: InputFormValues | null;
  result: ResultType | null;
  request: boolean;
  requestFailed: boolean;
}

export const initialState: TEncryptionListState = {
  mode: "ENC",
  requestData: null,
  result: null,
  request: false,
  requestFailed: false,
};

const encryptionSlice = createSlice({
  name: "encryption",
  initialState,
  reducers: {
    setMode(
      state: TEncryptionListState,
      action: PayloadAction<EncryptionType>
    ) {
      state.mode = action.payload;
    },
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
export const { setMode, request, requestSuccess, requestFailed } =
  encryptionSlice.actions;

export default encryptionSlice.reducer;
