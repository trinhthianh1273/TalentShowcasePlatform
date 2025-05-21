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
    groupPostComments: Array<any>
}