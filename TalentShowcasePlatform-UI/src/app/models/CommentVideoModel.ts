export interface CommentVideoModel {
  id: string;
  videoId: string;
  userId: string;
  userName: string;
  userAvatarUrl: string;
  content: string;
  createdAt: string; // ISO format date string
}
