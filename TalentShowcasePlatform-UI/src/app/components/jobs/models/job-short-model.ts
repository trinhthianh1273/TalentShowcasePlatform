export interface JobShortModel {
  id: string;
  title: string;
  categoryId: string;
  categoryName: string;
  userName: string;
  userAvatarUrl: string;
  location: string;
  postedBy: string;
  createdAt: Date; // hoặc Date nếu bạn muốn xử lý ngày giờ với kiểu Date
}
