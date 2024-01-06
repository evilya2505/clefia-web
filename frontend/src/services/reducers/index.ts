import { combineReducers } from "redux";
import encryptionSlice from "./encryption";

export const rootReducer = combineReducers({
  encryption: encryptionSlice,
});
