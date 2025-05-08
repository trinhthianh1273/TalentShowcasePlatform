export interface LoginResponse {
  messages?: string[];
  succeeded?: boolean;
  data?: {
    userId?: string;
    userName?: string;
    email?: string;
    avatarUrl?: string;
    token?: string;
  };
  exception?: any;
  code?: number;
}