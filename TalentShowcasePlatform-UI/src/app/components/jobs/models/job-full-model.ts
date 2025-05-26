export interface JobFullModel {
  id: string;
  title: string;
  companyName: string;
  location: string;
  addressDetail: string;
  description: string;
  requirements: string;
  benefits: string;
  jobType: string;
  salaryFrom: number;
  salaryTo: number;
  expiryDate: string; // Hoặc Date nếu bạn xử lý thời gian
  contactEmail: string;
  contactPhone: string;
  postedBy: string;
  categoryId: string;
  categoryName: string;
  createdAt: Date; // Hoặc Date nếu cần
  postedByUser: {
    id: string;
    userName: string;
    fullName: string | null;
    email: string;
    bio: string;
    avatarUrl: string;
    location: string | null;
    roleId: string;
    createdAt: Date;
    role: string | null;
  };
}
