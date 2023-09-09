export class UserAuth{
    email: string = '';
    password: string = '';
}

export type User = {
    email: string,
    role: Role
}

export class Role {
    User = 0;
    Admin = 1;
}

export type AuthResponse = {
    accessToken: string
}