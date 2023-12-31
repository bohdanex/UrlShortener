export class UserAuth{
    email: string = '';
    password: string = '';
}

export class User{
    id: string;
    email: string;
    role: Role;
}

export const Role = {
    User: 0,
    Admin: 1
}

export type AuthResponse = {
    accessToken: string
}