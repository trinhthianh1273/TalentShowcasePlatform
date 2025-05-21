export interface GroupModel {
    id: string,
    name: string,
    description: string,
    createBy : string,
    UserName : string,
    CategoryId: string,
    CategoryName: string,
    createdAt: Date,
    Members : []
}