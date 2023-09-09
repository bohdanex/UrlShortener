import storage from 'react-secure-storage'
import {UserAuth, User, AuthResponse} from '../../types/User'

export async function getUser(): Promise<User | null>{
    const jwt = storage.getItem('jwt');
    if(jwt == null){
        return null;
    }
    const repsonse = await fetch('api/accouts/get', {
        method: 'get',
        headers:{
            "Authorization": "Bearer " + jwt,
            "Content-Type": "application/json; encoding-utf8"
        }
    });
    
    return JSON.parse(await repsonse.json())
}  

export async function login(user: UserAuth): Promise<boolean> {
    const userJson = JSON.stringify(user);
    const response = await fetch('api/account/login',
    {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: userJson
    });
    const resultJson = await response.json();
    const resultObject: AuthResponse = JSON.parse(resultJson);
    if(resultObject.accessToken != null){
        storage.setItem('jwt', resultObject.accessToken);
    }
    
    return resultObject.accessToken != null;
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
        storage.setItem('jwt', resultObject.accessToken);
    }
    
    return resultObject.accessToken != null;
}