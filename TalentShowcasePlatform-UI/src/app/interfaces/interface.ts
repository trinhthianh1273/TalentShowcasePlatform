import { CurrentUserModel } from "../models/CurrentUserModel";

export interface LoginResponse {
  messages?: string[];
  succeeded?: boolean;
  data?: CurrentUserModel;
  exception?: any;
  code?: number;
}

export interface UserData {
  messages?: string[];
  succeeded?: boolean;
  data?: {
    userId?: string;
    fullName?: string;
    userName?: string;
    email?: string;
    avatarUrl?: string;
    bio?: string;
    location?: string;
    token?: string;
  };
  exception?: any;
  code?: number;
}

export interface VideoData {
  messages?: string[];
  succeeded?: boolean;
  data?: {
    id?: string;
    title: string;
    description?: string;
    url?: string;
    categoryId?: string;
    IsPrivate?: boolean // để hiển thị checkbox
  }
}