import { CurrentUserModel } from "./CurrentUserModel";

export interface LoginResponseModel {
  messages?: string[];
  succeeded?: boolean;
  data?: CurrentUserModel;
  exception?: any;
  code?: number;
}