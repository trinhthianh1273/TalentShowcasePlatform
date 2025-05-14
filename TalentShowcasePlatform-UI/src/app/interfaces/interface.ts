export interface LoginResponse {
  messages?: string[];
  succeeded?: boolean;
  data?: {
    userId?: string;
    fullName?: string;
    userName?: string;
    email?: string;
    avatarUrl?: string;
    token?: string;
  };
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