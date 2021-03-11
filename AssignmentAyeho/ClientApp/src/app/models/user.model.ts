export class User {
    member: Member;
    error: string;
    isUserAuth: boolean;
    //token: string;
}
export class Member{
    firstName: string;
    lastName: string;
    username: string;
    //id: number;
    // password: string;
}
export class Register{
    username: string;
    password: string;
    verificationPassword: string;
    firstName: string;
    lastName: string;
    phoneArea: string;
    phoneNumber: string;
}
export interface ExtendMember 
{
    firstName: string;
    lastName: string;
    username: string;
    phoneArea: string;
    phoneNumber: string;
    UserConnectinonId: string;
}
export interface ConnectedUsers{
    members: ExtendMember[];
}
