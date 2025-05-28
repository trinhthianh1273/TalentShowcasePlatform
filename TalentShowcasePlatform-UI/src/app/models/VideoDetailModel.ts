export interface UserNavigationDto {
    id: string;
    userName: string;
    fullName: string | null;
    avatarUrl: string | null;
    email: string;
    bio: string;
    role: string | null;
}

export interface VideoDetailModel {
    id: string;
    title: string;
    description: string;
    url: string;
    userId: string;
    categoryId: string;
    isPrivate: boolean;
    uploadedAt: string; // ISO date string (e.g. "2025-04-27T16:11:31.422912")
    likeCount: number | 0;
    commentCount: number;
    viewCount: number;
    userNavigationDto: UserNavigationDto;
}
