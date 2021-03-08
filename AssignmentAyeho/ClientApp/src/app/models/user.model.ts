export class User {
    member: Member;
    error: string;
    isUserAuth: boolean;
    //token: string;
}
export class Member{
    firstName: string;
    lastName: string;
    //id: number;
    // username: string;
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
