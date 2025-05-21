export interface GroupModel {
    id: string,
    name: string,
    description: string,
    groupAvatar: string,
    createdBy : string,
    UserName : string,
    CategoryId: string,
    CategoryName: string,
    createdAt: Date,
    Members : []
}