export interface GroupPostModel {
    id: string,
    title: string,
    content: string,
    createdAt: Date,
    lastActivityDate: Date,
    imgUrl?: string ,
    userId?: string,
    userName?: string,
    userImgUrl?: string,
    groupId: string,
    groupName: string,
    likeCount : number | 0,
    commentCount: number | 0,
    groupPostComments: Array<any>
}