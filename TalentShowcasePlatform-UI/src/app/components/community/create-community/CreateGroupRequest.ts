export interface CreateGroupRequest {
  name: string;
  description: string;
  categoryId: string;
  createdBy : string;
  groupAvatar: File | null;
}
