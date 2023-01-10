export interface Users
{
    userId?:number;
    username : string;
    password: string;
    account_age : Date ;
    eggTimer : Date ;
    goldCount : number;
    eggCount : number;
    showcaseCompanion_fk:number;
    showcaseCompanionFk?:number;
    aboutMe:string;
}