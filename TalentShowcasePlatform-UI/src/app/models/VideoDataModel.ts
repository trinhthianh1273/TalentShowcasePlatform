export interface VideoDataModel {
  id: string;
  title: string;
  description: string;
  url: string;
  categoryId: string;
  IsPrivate: boolean; // để hiển thị checkbox,
  uploadedAt: Date;
}