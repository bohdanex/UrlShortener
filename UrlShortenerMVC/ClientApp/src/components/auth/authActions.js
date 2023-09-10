import storage from 'react-secure-storage'
import {UserAuth, User, AuthResponse} from '../../types/Users'
import { ErrorResponse } from '../../types/Responses';
import {JWT_STORAGE_KEY} from '../../constants/'

export async function getUser(): Promise<User | null>{
    const jwt = storage.getItem(JWT_STORAGE_KEY);
    if(jwt == null){
        return null;
    }
    const repsonse = await fetch('api/accouts/get', {
        method: 'get',
        headers:{
            "Authorization": "Bearer " + jwt,
            "Accept": "application/json; encoding-utf8"
        }
    });
    
    return JSON.parse(await repsonse.json())
}  

export async function login(user: UserAuth): Promise<AuthResponse | ErrorResponse> {
    const userJson = JSON.stringify(user);
    const response = await fetch('api/account/login',
    {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: userJson
    });

    const resultObject: AuthResponse = await response.json();
    if(resultObject.accessToken != null){
        storage.setItem(JWT_STORAGE_KEY, resultObject.accessToken);
    }
    
    return resultObject;
}

export async function register(user: UserAuth): Promise<boolean> {
    const userJson = JSON.stringify(user);
    const response = await fetch('api/account/register',
    {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: userJson
    });
    const resultJson = await response.json();
    const resultObject: AuthResponse = resultJson;
    if(resultObject.accessToken != null){
        storage.setItem(JWT_STORAGE_KEY, resultObject.accessToken);
    }
    
    return resultObject.accessToken != null;
}

export async function getUserInfo(accessToken: string): Promise<User>{
    const url = 'api/account/get-user-info';
    try{
        console.log(accessToken)
        const response = await fetch(url, {
            method: 'POST',
            headers:{
                "Authorization": "Bearer " + accessToken,
                "Content-Type": "application/json"
            }
        })
        
        if(!response.ok){
            throw new Error('Unauthorized');
        }
        return await response.json();
    }
    catch(error){
        throw error;
    }
}