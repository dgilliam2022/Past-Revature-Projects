export interface Posts
{
    postId : number;
    userIdFk : number;
    content : string;
    posterNickname?:string;
}